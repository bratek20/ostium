package com.github.bratek20.ostium.singlegame.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.gamesmanagement.api.CreatedGame
import com.github.bratek20.ostium.gamesmanagement.api.GameId
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.singlegame.api.*
import com.github.bratek20.ostium.user.api.Username

class SideLogic(
    private val logger: Logger,
    private var user: Username?
) {
    fun update(user: Username?) {
        this.user = user
    }

    private val hand: MutableList<CreatureCard> = mutableListOf(
        CreatureCard.create(
            id = CreatureCardId("Mouse1"),
        ),
        CreatureCard.create(
            id = CreatureCardId("Mouse2"),
        ),
    )
    private var attackRowCard: CreatureCard? = null
    private var defenseRowCard: CreatureCard? = null

    fun playCard(cardId: CreatureCardId, row: RowType) {
        val card = hand.first { it.getId() == cardId }
        hand.remove(card)

        when (row) {
            RowType.ATTACK -> attackRowCard = CreatureCard.create(id = cardId)
            RowType.DEFENSE -> defenseRowCard = CreatureCard.create(id = cardId)
        }

        logger.info("User `$user` played card `$cardId` in $row row")
    }

    fun moveCard(cardId: CreatureCardId, from: RowType, to: RowType) {
        val card = when (from) {
            RowType.ATTACK -> attackRowCard
            RowType.DEFENSE -> defenseRowCard
        } ?: throw IllegalStateException("No card in $from row")

        var toCurrentCard: CreatureCard? = null
        when (to) {
            RowType.ATTACK -> {
                toCurrentCard = attackRowCard
                attackRowCard = card
            }
            RowType.DEFENSE -> {
                toCurrentCard = defenseRowCard
                defenseRowCard = card
            }
        }

        when (from) {
            RowType.ATTACK -> attackRowCard = toCurrentCard
            RowType.DEFENSE -> defenseRowCard = toCurrentCard
        }

        logger.info("User `$user` moved card `$cardId` from $from to $to row")
    }

    fun getApiHand(): Hand {
        return Hand.create(
            cards = hand.toList()
        )
    }

    fun getTablePart(): PlayerSide {
        return PlayerSide.create(
            attackRow = Row.create(
                type = RowType.ATTACK,
                card = attackRowCard
            ),
            defenseRow = Row.create(
                type = RowType.DEFENSE,
                card = defenseRowCard
            ),
        )
    }

    fun getName(): Username? {
        return user
    }
}

class GameStateLogic(
    private val logger: Logger,
    private var createdGame: CreatedGame
) {
    private val creatorSide = SideLogic(logger, createdGame.getCreator())
    private val joinerSide = SideLogic(logger, createdGame.getJoiner())

    fun update(createdGame: CreatedGame) {
        this.createdGame = createdGame
        joinerSide.update(createdGame.getJoiner())
    }

    private fun getMySide(user: Username): SideLogic {
        return if (user == createdGame.getCreator()) creatorSide else joinerSide
    }

    private fun getOpponentSide(user: Username): SideLogic {
        return if (user == createdGame.getCreator()) joinerSide else creatorSide
    }

    fun playCard(user: Username, cardId: CreatureCardId, row: RowType): GameState {
        getMySide(user).playCard(cardId, row)
        return toApi(user)
    }

    fun moveCard(user: Username, cardId: CreatureCardId, from: RowType, to: RowType): GameState {
        getMySide(user).moveCard(cardId, from, to)
        return toApi(user)
    }

    fun toApi(user: Username): GameState {
        val mySide = getMySide(user)
        val opponentSide = getOpponentSide(user)

        return GameState.create(
            myHand = mySide.getApiHand(),
            opponentHand = opponentSide.getApiHand(),
            table = Table.create(
                mySide = mySide.getTablePart(),
                opponentSide = opponentSide.getTablePart()
            ),
            myName = mySide.getName()!!,
            opponentName = opponentSide.getName()
        )
    }
}

class SingleGameApiLogic(
    private val managementApi: GamesManagementApi,
    private val logger: Logger
): SingleGameApi {
    private val games: MutableMap<GameId, GameStateLogic> = mutableMapOf()

    private fun getCreatedGameOrThrow(gameId: GameId, user: Username): CreatedGame {
        val game = managementApi.getAllCreated().firstOrNull { it.getId() == gameId }
        if (game == null) {
            val message = "Game ${gameId.value} for user `${user.value}` not found"
            throw GameNotFoundException(message)
        }
        return game
    }

    private fun getGameOrThrow(gameId: GameId, user: Username): GameStateLogic {
        val createdGame = getCreatedGameOrThrow(gameId, user)
        if (!games.containsKey(gameId)) {
            games[gameId] = GameStateLogic(logger, createdGame)
        }
        val game = games[gameId]!!
        game.update(createdGame)
        return game
    }

    override fun getState(gameId: GameId, user: Username): GameState {
        return getGameOrThrow(gameId, user).toApi(user)
    }

    override fun playCard(gameId: GameId, user: Username, cardId: CreatureCardId, row: RowType): GameState {
        return getGameOrThrow(gameId, user).playCard(user, cardId, row)
    }

    override fun moveCard(gameId: GameId, user: Username, cardId: CreatureCardId, from: RowType, to: RowType): GameState {
        return getGameOrThrow(gameId, user).moveCard(user, cardId, from, to)
    }
}
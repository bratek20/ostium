package com.github.bratek20.ostium.singlegame.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.gamesmanagement.api.CreatedGame
import com.github.bratek20.ostium.gamesmanagement.api.GameId
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.singlegame.api.*
import com.github.bratek20.ostium.user.api.Username

class GameStateLogic(
    private val logger: Logger,
    private val createdGame: CreatedGame
) {
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

    fun playCard(user: Username, cardId: CreatureCardId, row: RowType): GameState {
        val card = hand.first { it.getId() == cardId }
        hand.remove(card)

        when (row) {
            RowType.ATTACK -> attackRowCard = CreatureCard.create(id = cardId)
            RowType.DEFENSE -> defenseRowCard = CreatureCard.create(id = cardId)
        }

        logger.info("Card $cardId played in $row row")
        return toApi()
    }

    fun moveCard(user: Username, cardId: CreatureCardId, from: RowType, to: RowType): GameState {
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

        logger.info("Card $cardId moved from $from to $to row")
        return toApi()
    }


    fun toApi(): GameState {
        return GameState.create(
            myHand = Hand.create(
                cards = hand.toList()
            ),
            opponentHand = Hand.create(
                cards = hand.toList()
            ),
            table = Table.create(
                mySide = PlayerSide.create(
                    attackRow = Row.create(
                        type = RowType.ATTACK,
                        card = attackRowCard
                    ),
                    defenseRow = Row.create(
                        type = RowType.DEFENSE,
                        card = defenseRowCard
                    ),
                ),
                opponentSide = PlayerSide.create(
                    attackRow = Row.create(
                        type = RowType.ATTACK,
                        card = null
                    ),
                    defenseRow = Row.create(
                        type = RowType.DEFENSE,
                        card = null
                    ),
                ),
            ),
            myName = createdGame.getCreator(),
            opponentName = null,
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
        val game = games[gameId]
        if (game == null) {
            val createdGame = getCreatedGameOrThrow(gameId, user)
            games[gameId] = GameStateLogic(logger, createdGame)
        }
        return games[gameId]!!
    }

    override fun getState(gameId: GameId, user: Username): GameState {
        return getGameOrThrow(gameId, user).toApi()
    }

    override fun playCard(gameId: GameId, user: Username, cardId: CreatureCardId, row: RowType): GameState {
        return getGameOrThrow(gameId, user).playCard(user, cardId, row)
    }

    override fun moveCard(gameId: GameId, user: Username, cardId: CreatureCardId, from: RowType, to: RowType): GameState {
        return getGameOrThrow(gameId, user).moveCard(user, cardId, from, to)
    }
}
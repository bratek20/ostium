package com.github.bratek20.ostium.kajiugame.impl

import com.github.bratek20.ostium.carddrawer.api.CardDrawerApi
import com.github.bratek20.ostium.kajiugame.api.*
import com.github.bratek20.ostium.gamesmanagement.api.*
import com.github.bratek20.ostium.user.api.Username

private class HandLogic(
    private val drawer: CardDrawerApi
) {
    private val cards = mutableListOf(
        drawer.draw(),
        drawer.draw(),
        drawer.draw(),
        drawer.draw()
    )

    fun getState(): Hand {
        return Hand.create(cards)
    }

    fun removeCardAndGet(idx: Int): Card {
        return cards.removeAt(idx)
    }
}

private class AttackGiverLogic(
    val type: DamageType,
    private var damageValue: Int
) {
    fun getState(): AttackGiver {
        return AttackGiver.create(
            type = type,
            damageValue = damageValue
        )
    }

    fun addDamage(value: Int) {
        damageValue += value
    }
}

private class PlayerSideLogic {
    private val playedCards = mutableListOf<Card>()
    private val lightGiver = AttackGiverLogic(DamageType.Light, 0)
    private val mediumGiver = AttackGiverLogic(DamageType.Medium, 0)
    private val heavyGiver = AttackGiverLogic(DamageType.Heavy, 0)
    private var focusLeft = 2

    fun getState(): PlayerSide {
        return PlayerSide.create(
            pool = AttackPool.create(
                lightGiver = lightGiver.getState(),
                mediumGiver = mediumGiver.getState(),
                heavyGiver = heavyGiver.getState(),
                focusLeft = focusLeft
            ),
            playedCards = playedCards.toList()
        )
    }

    fun handleCardPlayed(card: Card) {
        playedCards.add(card)
        allGivers().first { it.type == card.getType() }.addDamage(card.getValue())
        focusLeft -= card.getFocusCost()
    }

    private fun allGivers(): List<AttackGiverLogic> {
        return listOf(lightGiver, mediumGiver, heavyGiver)
    }
}

private class PlayerStateLogic(
    drawer: CardDrawerApi
) {
    private val hand = HandLogic(drawer)
    private val side = PlayerSideLogic()
    private var ready = false

    val initSideState = side.getState()

    fun isReady(): Boolean {
        return ready
    }

    fun getSideState(): PlayerSide {
        return side.getState()
    }

    fun getHandState(): Hand {
        return hand.getState()
    }

    fun markReady() {
        ready = true
    }

    fun playCard(handCardIdx: Int) {
        val handCard = hand.removeCardAndGet(handCardIdx)
        side.handleCardPlayed(handCard)
    }
}

private class HitZoneLogic(
    private val position: HitZonePosition
) {
    fun getState(): HitZone {
        return HitZone.create(
            position = position,
            lightReceiver = AttackReceiver.create(
                type = DamageType.Light,
                myDamage = 0,
                opponentDamage = 0
            ),
            mediumReceiver = AttackReceiver.create(
                type = DamageType.Medium,
                myDamage = 0,
                opponentDamage = 0
            ),
            heavyReceiver = AttackReceiver.create(
                type = DamageType.Heavy,
                myDamage = 0,
                opponentDamage = 0
            )
        )
    }
}

private class GameStateLogic(
    private val creator: Username,
    drawer: CardDrawerApi
) {
    private var phase = TurnPhase.PlayCard

    private val leftZone = HitZoneLogic(HitZonePosition.Left)
    private val centerZone = HitZoneLogic(HitZonePosition.Center)
    private val rightZone = HitZoneLogic(HitZonePosition.Right)

    private val creatorState = PlayerStateLogic(drawer)
    private val joinerState = PlayerStateLogic(drawer)

    fun getState(user: Username): GameState {
        return GameState.create(
            turn = 1,
            phase = phase,
            table = Table.create(
                leftZone = leftZone.getState(),
                centerZone = centerZone.getState(),
                rightZone = rightZone.getState(),
                mySide = getMyState(user).getSideState(),
                opponentSide = getOpponentState(user).initSideState
            ),
            hand = getMyState(user).getHandState(),
            myReady = getMyState(user).isReady(),
            opponentReady = getOpponentState(user).isReady()
        )
    }

    fun endPhase(user: Username): GameState {
        getMyState(user).markReady()

        if (getMyState(user).isReady() && getOpponentState(user).isReady()) {
            phase = TurnPhase.AssignDamage
        }

        return getState(user)
    }

    fun playCard(user: Username, handCardIdx: Int): GameState {
        getMyState(user).playCard(handCardIdx)
        return getState(user)
    }

    fun assignDamage(user: Username, zone: HitZonePosition, damageType: DamageType): GameState {
        return getState(user)
    }

    private fun getMyState(user: Username): PlayerStateLogic {
        return if (user == creator) creatorState else joinerState
    }

    private fun getOpponentState(user: Username): PlayerStateLogic {
        return if (user == creator) joinerState else creatorState
    }
}

class GameApiLogic(
    private val managementApi: GamesManagementApi,
    private val drawer: CardDrawerApi
) : GameApi {

    override fun getState(token: GameToken): GameState {
        return getGameStateLogic(token).getState(token.getUsername())
    }

    override fun endPhase(token: GameToken): GameState {
        return getGameStateLogic(token).endPhase(token.getUsername())
    }

    override fun playCard(token: GameToken, handCardIdx: Int): GameState {
        return getGameStateLogic(token).playCard(token.getUsername(), handCardIdx)
    }

    override fun assignDamage(token: GameToken, zone: HitZonePosition, damageType: DamageType): GameState {
        return getGameStateLogic(token).assignDamage(token.getUsername(), zone, damageType)
    }

    private fun getCreatedGameOrThrow(token: GameToken): CreatedGame {
        val game = managementApi.getAllCreated().firstOrNull { it.getId() == token.getGameId() }
        if (game == null) {
            throw GameNotFoundException("Game for id ${token.getGameId()} not found");
        }
        return game
    }

    private val gameStates: MutableMap<GameId, GameStateLogic> = mutableMapOf()
    private fun getGameStateLogic(token: GameToken): GameStateLogic {
        if (!gameStates.containsKey(token.getGameId())) {
            val createdGame = getCreatedGameOrThrow(token)
            gameStates[token.getGameId()] = GameStateLogic(createdGame.getCreator(), drawer)
        }
        return gameStates[token.getGameId()]!!
    }
}

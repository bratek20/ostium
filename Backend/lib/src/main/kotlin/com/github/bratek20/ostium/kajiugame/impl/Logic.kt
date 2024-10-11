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

private class PlayerSideLogic {
    private val playedCards = mutableListOf<Card>()
    private var focusLeft = 2

    fun getState(): PlayerSide {
        return PlayerSide.create(
            pool = AttackPool.create(
                attackGivers = listOf(
                    AttackGiver.create(
                        type = DamageType.Light,
                        damageValue = 0,
                    ),
                    AttackGiver.create(
                        type = DamageType.Medium,
                        damageValue = 0,
                    ),
                    AttackGiver.create(
                        type = DamageType.Heavy,
                        damageValue = 0,
                    ),
                ),
                focusLeft = focusLeft
            ),
            playedCards = playedCards
        )
    }

    fun putCardAndPayCost(card: Card) {
        playedCards.add(card)
        focusLeft -= card.getFocusCost()
    }
}

private class GameStateLogic(
    drawer: CardDrawerApi
) {
    private val hand = HandLogic(drawer)
    private val mySide = PlayerSideLogic()
    private val opponentSide = PlayerSideLogic()

    fun getState(): GameState {
        return GameState.create(
            turn = 1,
            phase = TurnPhase.PlayCard,
            table = Table.create(
                leftZone = createHitZone(),
                centerZone = createHitZone(),
                rightZone = createHitZone(),
                mySide = mySide.getState(),
                opponentSide = opponentSide.getState()
            ),
            hand = hand.getState(),
            myReady = false,
            opponentReady = false
        )
    }

    fun playCard(handCardIdx: Int): GameState {
        val handCard = hand.removeCardAndGet(handCardIdx)
        mySide.putCardAndPayCost(handCard)

        return getState()
    }

    private fun createHitZone(): HitZone {
        return HitZone.create(
            leftReceiver = AttackReceiver.create(
                type = DamageType.Light,
                myDamage = 0,
                opponentDamage = 0
            ),
            centerReceiver = AttackReceiver.create(
                type = DamageType.Medium,
                myDamage = 0,
                opponentDamage = 0
            ),
            rightReceiver = AttackReceiver.create(
                type = DamageType.Heavy,
                myDamage = 0,
                opponentDamage = 0
            )
        )
    }
}

class GameApiLogic(
    private val managementApi: GamesManagementApi,
    private val drawer: CardDrawerApi
) : GameApi {

    override fun getState(token: GameToken): GameState {
        return getGameStateLogic(token).getState()
    }

    override fun playCard(token: GameToken, handCardIdx: Int): GameState {
        return getGameStateLogic(token).playCard(handCardIdx)
    }

    private fun getCreatedGameOrThrow(token: GameToken): CreatedGame {
        val game = managementApi.getAllCreated().firstOrNull { it.getId() == token.getGameId() }
        if (game == null) {
            throw GameNotFoundException("Game for id ${token.getGameId()} not found");
        }
        return game
    }

    private fun getGameStateLogic(token: GameToken): GameStateLogic {
        getCreatedGameOrThrow(token)
        return GameStateLogic(drawer)
    }
}

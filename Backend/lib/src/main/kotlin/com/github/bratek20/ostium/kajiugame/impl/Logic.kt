package com.github.bratek20.ostium.kajiugame.impl

import com.github.bratek20.ostium.carddrawer.api.CardDrawerApi
import com.github.bratek20.ostium.kajiugame.api.*
import com.github.bratek20.ostium.gamesmanagement.api.*
import com.github.bratek20.ostium.user.api.Username

class GameApiLogic(
    private val managementApi: GamesManagementApi,
    private val drawer: CardDrawerApi
) : GameApi {

    override fun getState(token: GameToken): GameState {
        val game = getCreatedGameOrThrow(token)
        return GameState.create(
            turn = 1,
            phase = TurnPhase.PlayCard,
            table = Table.create(
                leftZone = createHitZone(),
                centerZone = createHitZone(),
                rightZone = createHitZone(),
                mySide = PlayerSide.create(
                    pool = AttackPool.create(attackGivers = emptyList(), focusLeft = 0),
                    playedCards = emptyList()
                ),
                opponentSide = PlayerSide.create(
                    pool = AttackPool.create(attackGivers = emptyList(), focusLeft = 0),
                    playedCards = emptyList()
                )
            ),
            hand = Hand.create(cards = listOf(
                drawer.draw(),
                drawer.draw(),
                drawer.draw(),
                drawer.draw())),
            myReady = false,
            opponentReady = false
        )
    }

    private fun createCard(): Card {
        return Card.create(
            type = DamageType.Heavy,
            value = 0,
            focusCost = 0
        )
    }

    private fun createHitZone(): HitZone {
        return HitZone.create(
            leftReceiver = AttackReceiver.create(
                type = DamageType.Heavy,
                myDamage = 0,
                opponentDamage = 0
            ),
            centerReceiver = AttackReceiver.create(
                type = DamageType.Heavy,
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

    private fun getCreatedGameOrThrow(token: GameToken): CreatedGame {
        val game = managementApi.getAllCreated().firstOrNull { it.getId() == token.getGameId() }
        if (game == null) {
            throw GameNotFoundException("Game for id ${token.getGameId()} not found");
        }
        return game
    }
}

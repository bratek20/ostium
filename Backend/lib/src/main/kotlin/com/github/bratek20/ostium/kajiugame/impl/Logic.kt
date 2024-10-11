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
                mySide = createPlayerSide(),
                opponentSide = createPlayerSide()
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

    private fun createPlayerSide(): PlayerSide {
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
                focusLeft = 2
            ),
            playedCards = emptyList()
        )
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

    private fun getCreatedGameOrThrow(token: GameToken): CreatedGame {
        val game = managementApi.getAllCreated().firstOrNull { it.getId() == token.getGameId() }
        if (game == null) {
            throw GameNotFoundException("Game for id ${token.getGameId()} not found");
        }
        return game
    }
}

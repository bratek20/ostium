package com.github.bratek20.ostium.gamesetup.impl

import com.github.bratek20.ostium.gamesetup.api.*

import com.github.bratek20.ostium.gamecomponents.api.*

class GameSetupApiLogic: GameSetupApi {
    override fun startGame(): Game {
        return Game.create(
            id = GameId("Game1"),
            hand = Hand.create(
                cards = listOf(
                    CreatureCard.create(
                        id = CreatureCardId("Mouse1"),
                    ),
                    CreatureCard.create(
                        id = CreatureCardId("Mouse2"),
                    ),
                )
            ),
            table = Table.create(
                gateDurabilityCard = GateDurabilityCard.create(
                    myMarker = GateDurabilityMarker(15),
                    opponentMarker = GateDurabilityMarker(15)
                ),
                attackRow = null,
                defenseRow = null,
                gateCard = GateCard.create(false),
            )
        )
    }

    override fun playCard(cardId: CreatureCardId, row: RowType): Game {
        TODO("Not yet implemented")
    }

    override fun moveCard(cardId: CreatureCardId, from: RowType, to: RowType): Game {
        TODO("Not yet implemented")
    }
}
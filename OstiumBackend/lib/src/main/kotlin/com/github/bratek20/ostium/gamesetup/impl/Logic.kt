package com.github.bratek20.ostium.gamesetup.impl

import com.github.bratek20.ostium.gamesetup.api.*

import com.github.bratek20.ostium.gamecomponents.api.*

class GameSetupApiLogic: GameSetupApi {
    override fun startGame(): Game {
        return Game.create(
            hand = Hand.create(
                cards = listOf(
                    CreatureCard.create("HandCard1"),
                    CreatureCard.create("HandCard2")
                )
            ),
            table = Table.create(
                creatureCard = CreatureCard.create("TableCreature"),
                gateCard = GateCard.create(false),
                gateDurabilityCard = GateDurabilityCard.create(
                    myMarker = GateDurabilityMarker(15),
                    opponentMarker = GateDurabilityMarker(15)
                )
            )
        )
    }
}
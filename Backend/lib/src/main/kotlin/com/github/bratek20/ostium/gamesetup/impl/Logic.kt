package com.github.bratek20.ostium.gamesetup.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.gamesetup.api.*

import com.github.bratek20.ostium.gamecomponents.api.*

class GameSetupApiLogic(
    private val logger: Logger
): GameSetupApi {
    private val hand: MutableList<CreatureCard> = mutableListOf(
        CreatureCard.create(
            id = CreatureCardId("Mouse1"),
        ),
        CreatureCard.create(
            id = CreatureCardId("Mouse2"),
        ),
    )
    private var attackRow: CreatureCard? = null
    private var defenseRow: CreatureCard? = null

    override fun startGame(): Game {
        logger.info("Game started")
        return toApiGame()
    }

    override fun playCard(cardId: CreatureCardId, row: RowType): Game {
        val card = hand.first { it.getId() == cardId }
        hand.remove(card)

        when (row) {
            RowType.ATTACK -> attackRow = CreatureCard.create(id = cardId)
            RowType.DEFENSE -> defenseRow = CreatureCard.create(id = cardId)
        }

        return toApiGame()
    }

    override fun moveCard(cardId: CreatureCardId, from: RowType, to: RowType): Game {
        val card = when (from) {
            RowType.ATTACK -> attackRow
            RowType.DEFENSE -> defenseRow
        } ?: throw IllegalStateException("No card in $from row")

        when (to) {
            RowType.ATTACK -> attackRow = card
            RowType.DEFENSE -> defenseRow = card
        }

        when (from) {
            RowType.ATTACK -> attackRow = null
            RowType.DEFENSE -> defenseRow = null
        }

        return toApiGame()
    }

    private fun toApiGame(): Game {
        return Game.create(
            hand = Hand.create(
                cards = hand.toList()
            ),
            table = Table.create(
                gateDurabilityCard = GateDurabilityCard.create(
                    myMarker = GateDurabilityMarker(15),
                    opponentMarker = GateDurabilityMarker(15)
                ),
                attackRow = attackRow,
                defenseRow = defenseRow,
                gateCard = GateCard.create(false),
            )
        )
    }
}
package com.github.bratek20.ostium.game.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.game.api.*

class GameApiLogic(
    private val logger: Logger
): GameApi {
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

    override fun startGame(): Game {
        logger.info("Game started")
        return toApiGame()
    }

    override fun playCard(cardId: CreatureCardId, row: RowType): Game {
        val card = hand.first { it.getId() == cardId }
        hand.remove(card)

        when (row) {
            RowType.ATTACK -> attackRowCard = CreatureCard.create(id = cardId)
            RowType.DEFENSE -> defenseRowCard = CreatureCard.create(id = cardId)
        }

        return toApiGame()
    }

    override fun moveCard(cardId: CreatureCardId, from: RowType, to: RowType): Game {
        val card = when (from) {
            RowType.ATTACK -> attackRowCard
            RowType.DEFENSE -> defenseRowCard
        } ?: throw IllegalStateException("No card in $from row")

        when (to) {
            RowType.ATTACK -> attackRowCard = card
            RowType.DEFENSE -> defenseRowCard = card
        }

        when (from) {
            RowType.ATTACK -> attackRowCard = null
            RowType.DEFENSE -> defenseRowCard = null
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
                attackRow = Row.create(card = attackRowCard),
                defenseRow = Row.create(card = defenseRowCard),
                gateCard = GateCard.create(false),
            )
        )
    }
}
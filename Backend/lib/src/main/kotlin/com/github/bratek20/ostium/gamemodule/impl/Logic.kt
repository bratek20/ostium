package com.github.bratek20.ostium.gamemodule.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.gamemodule.api.*

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

        logger.info("Card $cardId played in $row row")
        return toApiGame()
    }

    override fun moveCard(cardId: CreatureCardId, from: RowType, to: RowType): Game {
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
                attackRow = Row.create(
                    type = RowType.ATTACK,
                    card = attackRowCard
                ),
                defenseRow = Row.create(
                    type = RowType.DEFENSE,
                    card = defenseRowCard
                ),
                gateCard = GateCard.create(false),
            )
        )
    }
}
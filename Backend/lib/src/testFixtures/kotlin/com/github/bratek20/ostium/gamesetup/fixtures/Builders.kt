// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesetup.fixtures

import com.github.bratek20.ostium.gamecomponents.api.*
import com.github.bratek20.ostium.gamecomponents.fixtures.*

import com.github.bratek20.ostium.gamesetup.api.*

data class TableDef(
    var gateDurabilityCard: (GateDurabilityCardDef.() -> Unit) = {},
    var attackRow: (CreatureCardDef.() -> Unit)? = null,
    var defenseRow: (CreatureCardDef.() -> Unit)? = null,
    var gateCard: (GateCardDef.() -> Unit) = {},
)
fun table(init: TableDef.() -> Unit = {}): Table {
    val def = TableDef().apply(init)
    return Table.create(
        gateDurabilityCard = gateDurabilityCard(def.gateDurabilityCard),
        attackRow = def.attackRow?.let { it -> creatureCard(it) },
        defenseRow = def.defenseRow?.let { it -> creatureCard(it) },
        gateCard = gateCard(def.gateCard),
    )
}

data class HandDef(
    var cards: List<(CreatureCardDef.() -> Unit)> = emptyList(),
)
fun hand(init: HandDef.() -> Unit = {}): Hand {
    val def = HandDef().apply(init)
    return Hand.create(
        cards = def.cards.map { it -> creatureCard(it) },
    )
}

data class GameDef(
    var table: (TableDef.() -> Unit) = {},
    var hand: (HandDef.() -> Unit) = {},
)
fun game(init: GameDef.() -> Unit = {}): Game {
    val def = GameDef().apply(init)
    return Game.create(
        table = table(def.table),
        hand = hand(def.hand),
    )
}
// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesetup.fixtures

import com.github.bratek20.ostium.gamecomponents.api.*
import com.github.bratek20.ostium.gamecomponents.fixtures.*

import com.github.bratek20.ostium.gamesetup.api.*

fun diffRowType(given: RowType, expected: String, path: String = ""): String {
    if (given != RowType.valueOf(expected)) { return "${path}value ${given.name} != ${expected}" }
    return ""
}

data class ExpectedTable(
    var gateDurabilityCard: (ExpectedGateDurabilityCard.() -> Unit)? = null,
    var attackRowEmpty: Boolean? = null,
    var attackRow: (ExpectedCreatureCard.() -> Unit)? = null,
    var defenseRowEmpty: Boolean? = null,
    var defenseRow: (ExpectedCreatureCard.() -> Unit)? = null,
    var gateCard: (ExpectedGateCard.() -> Unit)? = null,
)
fun diffTable(given: Table, expectedInit: ExpectedTable.() -> Unit, path: String = ""): String {
    val expected = ExpectedTable().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.gateDurabilityCard?.let {
        if (diffGateDurabilityCard(given.getGateDurabilityCard(), it) != "") { result.add(diffGateDurabilityCard(given.getGateDurabilityCard(), it, "${path}gateDurabilityCard.")) }
    }

    expected.attackRowEmpty?.let {
        if ((given.getAttackRow() == null) != it) { result.add("${path}attackRow empty ${(given.getAttackRow() == null)} != ${it}") }
    }

    expected.attackRow?.let {
        if (diffCreatureCard(given.getAttackRow()!!, it) != "") { result.add(diffCreatureCard(given.getAttackRow()!!, it, "${path}attackRow.")) }
    }

    expected.defenseRowEmpty?.let {
        if ((given.getDefenseRow() == null) != it) { result.add("${path}defenseRow empty ${(given.getDefenseRow() == null)} != ${it}") }
    }

    expected.defenseRow?.let {
        if (diffCreatureCard(given.getDefenseRow()!!, it) != "") { result.add(diffCreatureCard(given.getDefenseRow()!!, it, "${path}defenseRow.")) }
    }

    expected.gateCard?.let {
        if (diffGateCard(given.getGateCard(), it) != "") { result.add(diffGateCard(given.getGateCard(), it, "${path}gateCard.")) }
    }

    return result.joinToString("\n")
}

data class ExpectedHand(
    var cards: List<(ExpectedCreatureCard.() -> Unit)>? = null,
)
fun diffHand(given: Hand, expectedInit: ExpectedHand.() -> Unit, path: String = ""): String {
    val expected = ExpectedHand().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.cards?.let {
        if (given.getCards().size != it.size) { result.add("${path}cards size ${given.getCards().size} != ${it.size}"); return@let }
        given.getCards().forEachIndexed { idx, entry -> if (diffCreatureCard(entry, it[idx]) != "") { result.add(diffCreatureCard(entry, it[idx], "${path}cards[${idx}].")) } }
    }

    return result.joinToString("\n")
}

data class ExpectedGame(
    var table: (ExpectedTable.() -> Unit)? = null,
    var hand: (ExpectedHand.() -> Unit)? = null,
)
fun diffGame(given: Game, expectedInit: ExpectedGame.() -> Unit, path: String = ""): String {
    val expected = ExpectedGame().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.table?.let {
        if (diffTable(given.getTable(), it) != "") { result.add(diffTable(given.getTable(), it, "${path}table.")) }
    }

    expected.hand?.let {
        if (diffHand(given.getHand(), it) != "") { result.add(diffHand(given.getHand(), it, "${path}hand.")) }
    }

    return result.joinToString("\n")
}
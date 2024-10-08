// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.singlegame.fixtures

import com.github.bratek20.ostium.gamesmanagement.api.*
import com.github.bratek20.ostium.gamesmanagement.fixtures.*
import com.github.bratek20.ostium.user.api.*
import com.github.bratek20.ostium.user.fixtures.*

import com.github.bratek20.ostium.singlegame.api.*

fun diffCreatureCardId(given: CreatureCardId, expected: String, path: String = ""): String {
    if (given.value != expected) { return "${path}value ${given.value} != ${expected}" }
    return ""
}

fun diffRowType(given: RowType, expected: String, path: String = ""): String {
    if (given != RowType.valueOf(expected)) { return "${path}value ${given.name} != ${expected}" }
    return ""
}

data class ExpectedCreatureCard(
    var id: String? = null,
)
fun diffCreatureCard(given: CreatureCard, expectedInit: ExpectedCreatureCard.() -> Unit, path: String = ""): String {
    val expected = ExpectedCreatureCard().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.id?.let {
        if (diffCreatureCardId(given.getId(), it) != "") { result.add(diffCreatureCardId(given.getId(), it, "${path}id.")) }
    }

    return result.joinToString("\n")
}

data class ExpectedRow(
    var type: String? = null,
    var cardEmpty: Boolean? = null,
    var card: (ExpectedCreatureCard.() -> Unit)? = null,
)
fun diffRow(given: Row, expectedInit: ExpectedRow.() -> Unit, path: String = ""): String {
    val expected = ExpectedRow().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.type?.let {
        if (diffRowType(given.getType(), it) != "") { result.add(diffRowType(given.getType(), it, "${path}type.")) }
    }

    expected.cardEmpty?.let {
        if ((given.getCard() == null) != it) { result.add("${path}card empty ${(given.getCard() == null)} != ${it}") }
    }

    expected.card?.let {
        if (diffCreatureCard(given.getCard()!!, it) != "") { result.add(diffCreatureCard(given.getCard()!!, it, "${path}card.")) }
    }

    return result.joinToString("\n")
}

data class ExpectedPlayerSide(
    var attackRow: (ExpectedRow.() -> Unit)? = null,
    var defenseRow: (ExpectedRow.() -> Unit)? = null,
)
fun diffPlayerSide(given: PlayerSide, expectedInit: ExpectedPlayerSide.() -> Unit, path: String = ""): String {
    val expected = ExpectedPlayerSide().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.attackRow?.let {
        if (diffRow(given.getAttackRow(), it) != "") { result.add(diffRow(given.getAttackRow(), it, "${path}attackRow.")) }
    }

    expected.defenseRow?.let {
        if (diffRow(given.getDefenseRow(), it) != "") { result.add(diffRow(given.getDefenseRow(), it, "${path}defenseRow.")) }
    }

    return result.joinToString("\n")
}

data class ExpectedTable(
    var mySide: (ExpectedPlayerSide.() -> Unit)? = null,
    var opponentSide: (ExpectedPlayerSide.() -> Unit)? = null,
)
fun diffTable(given: Table, expectedInit: ExpectedTable.() -> Unit, path: String = ""): String {
    val expected = ExpectedTable().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.mySide?.let {
        if (diffPlayerSide(given.getMySide(), it) != "") { result.add(diffPlayerSide(given.getMySide(), it, "${path}mySide.")) }
    }

    expected.opponentSide?.let {
        if (diffPlayerSide(given.getOpponentSide(), it) != "") { result.add(diffPlayerSide(given.getOpponentSide(), it, "${path}opponentSide.")) }
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

data class ExpectedGameState(
    var table: (ExpectedTable.() -> Unit)? = null,
    var myHand: (ExpectedHand.() -> Unit)? = null,
    var opponentHand: (ExpectedHand.() -> Unit)? = null,
    var myName: String? = null,
    var opponentNameEmpty: Boolean? = null,
    var opponentName: String? = null,
)
fun diffGameState(given: GameState, expectedInit: ExpectedGameState.() -> Unit, path: String = ""): String {
    val expected = ExpectedGameState().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.table?.let {
        if (diffTable(given.getTable(), it) != "") { result.add(diffTable(given.getTable(), it, "${path}table.")) }
    }

    expected.myHand?.let {
        if (diffHand(given.getMyHand(), it) != "") { result.add(diffHand(given.getMyHand(), it, "${path}myHand.")) }
    }

    expected.opponentHand?.let {
        if (diffHand(given.getOpponentHand(), it) != "") { result.add(diffHand(given.getOpponentHand(), it, "${path}opponentHand.")) }
    }

    expected.myName?.let {
        if (diffUsername(given.getMyName(), it) != "") { result.add(diffUsername(given.getMyName(), it, "${path}myName.")) }
    }

    expected.opponentNameEmpty?.let {
        if ((given.getOpponentName() == null) != it) { result.add("${path}opponentName empty ${(given.getOpponentName() == null)} != ${it}") }
    }

    expected.opponentName?.let {
        if (diffUsername(given.getOpponentName()!!, it) != "") { result.add(diffUsername(given.getOpponentName()!!, it, "${path}opponentName.")) }
    }

    return result.joinToString("\n")
}
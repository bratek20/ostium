// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesmanagement.fixtures

import com.github.bratek20.ostium.user.api.*
import com.github.bratek20.ostium.user.fixtures.*

import com.github.bratek20.ostium.gamesmanagement.api.*

fun diffGameId(given: GameId, expected: Int, path: String = ""): String {
    if (given.value != expected) { return "${path}value ${given.value} != ${expected}" }
    return ""
}

data class ExpectedCreatedGame(
    var id: Int? = null,
    var creator: String? = null,
    var joinerEmpty: Boolean? = null,
    var joiner: String? = null,
)
fun diffCreatedGame(given: CreatedGame, expectedInit: ExpectedCreatedGame.() -> Unit, path: String = ""): String {
    val expected = ExpectedCreatedGame().apply(expectedInit)
    val result: MutableList<String> = mutableListOf()

    expected.id?.let {
        if (diffGameId(given.getId(), it) != "") { result.add(diffGameId(given.getId(), it, "${path}id.")) }
    }

    expected.creator?.let {
        if (diffUsername(given.getCreator(), it) != "") { result.add(diffUsername(given.getCreator(), it, "${path}creator.")) }
    }

    expected.joinerEmpty?.let {
        if ((given.getJoiner() == null) != it) { result.add("${path}joiner empty ${(given.getJoiner() == null)} != ${it}") }
    }

    expected.joiner?.let {
        if (diffUsername(given.getJoiner()!!, it) != "") { result.add(diffUsername(given.getJoiner()!!, it, "${path}joiner.")) }
    }

    return result.joinToString("\n")
}
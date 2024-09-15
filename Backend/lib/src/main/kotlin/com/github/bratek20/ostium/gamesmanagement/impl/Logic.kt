package com.github.bratek20.ostium.gamesmanagement.impl

import com.github.bratek20.ostium.gamesmanagement.api.*

import com.github.bratek20.ostium.user.api.*

class GamesManagementApiLogic: GamesManagementApi {
    private val games = mutableListOf<CreatedGame>()
    private var nextId = 1

    override fun create(creator: Username): GameId {
        val game = CreatedGame.create(GameId(nextId), creator, null)
        games.add(game)
        nextId++
        return game.getId()
    }

    override fun join(joiner: Username, gameId: GameId) {
        TODO("Not yet implemented")
    }

    override fun getAllCreated(): List<CreatedGame> {
        TODO("Not yet implemented")
    }
}
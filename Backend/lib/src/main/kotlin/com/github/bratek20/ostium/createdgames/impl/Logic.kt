package com.github.bratek20.ostium.createdgames.impl

import com.github.bratek20.ostium.createdgames.api.*

import com.github.bratek20.ostium.user.api.*

class CreatedGamesApiLogic: CreatedGamesApi {
    private val games = mutableListOf<CreatedGame>()
    private var nextId = 1

    override fun getAll(): List<CreatedGame> {
        return games
    }

    override fun create(creator: Username): GameId {
        val game = CreatedGame.create(GameId(nextId), creator)
        games.add(game)
        nextId++
        return game.getId()
    }
}
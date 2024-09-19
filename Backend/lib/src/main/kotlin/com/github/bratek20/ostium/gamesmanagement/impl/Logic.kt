package com.github.bratek20.ostium.gamesmanagement.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.gamesmanagement.api.*

import com.github.bratek20.ostium.user.api.*

class GamesManagementApiLogic(
    private val logger: Logger
): GamesManagementApi {
    private val games = mutableListOf<CreatedGame>()
    private var nextId = 1

    override fun create(creator: Username): GameId {
        val game = CreatedGame.create(GameId(nextId), creator, null)
        games.add(game)
        nextId++

        logger.info("User `$creator` created game with id ${game.getId()}")

        return game.getId()
    }

    override fun join(joiner: Username, gameId: GameId) {
        val game = games.first { it.getId() == gameId }
        games.remove(game)
        games.add(CreatedGame.create(game.getId(), game.getCreator(), joiner))

        logger.info("User `$joiner` joined game with id ${game.getId()}")
    }

    override fun getAllCreated(): List<CreatedGame> {
        return games
    }
}
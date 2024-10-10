package com.github.bratek20.ostium.gamesmanagement.impl

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.ostium.gamesmanagement.api.*

import com.github.bratek20.ostium.user.api.*

class GamesManagementApiLogic(
    private val logger: Logger
): GamesManagementApi {
    private val games = mutableListOf<CreatedGame>()
    private var nextId = 1

    override fun create(creator: Username): GameToken {
        val game = CreatedGame.create(GameId(nextId), creator, null)
        games.add(game)
        nextId++

        logger.info("User `$creator` created game with id ${game.getId()}")

        return GameToken.create(
            gameId = game.getId(),
            username = creator
        )
    }

    override fun join(joiner: Username, gameId: GameId): GameToken {
        val game = games.first { it.getId() == gameId }
        games.remove(game)
        games.add(CreatedGame.create(game.getId(), game.getCreator(), joiner))

        logger.info("User `$joiner` joined game with id ${game.getId()}")

        return GameToken.create(
            gameId = game.getId(),
            username = joiner
        )
    }

    override fun delete(gameId: GameId) {
        val game = games.first { it.getId() == gameId }
        games.remove(game)

        logger.info("Game with id ${game.getId()} deleted")
    }

    override fun getAllCreated(): List<CreatedGame> {
        return games
    }
}
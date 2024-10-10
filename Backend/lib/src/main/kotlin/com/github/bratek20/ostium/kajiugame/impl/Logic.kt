package com.github.bratek20.ostium.kajiugame.impl

import com.github.bratek20.ostium.kajiugame.api.*

import com.github.bratek20.ostium.gamesmanagement.api.*

class GameApiLogic: GameApi {
    override fun getState(token: GameToken): GameState {
        throw GameNotFoundException("Game for id ${token.getGameId()} not found");
    }
}
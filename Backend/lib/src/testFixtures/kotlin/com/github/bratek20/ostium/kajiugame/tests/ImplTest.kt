package com.github.bratek20.ostium.kajiugame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameToken
import com.github.bratek20.ostium.kajiugame.api.GameApi
import com.github.bratek20.ostium.kajiugame.api.GameNotFoundException
import com.github.bratek20.ostium.kajiugame.context.KajiuGameImpl
import com.github.bratek20.ostium.kajiugame.fixtures.assertGameState
import com.github.bratek20.ostium.user.fixtures.username
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test

class KajiuGameImplTest {

    private lateinit var api: GameApi
    private lateinit var gamesManagementApi: GamesManagementApi

    @BeforeEach
    fun createContext() {
        val c = someContextBuilder()
            .withModules(
                LogsMocks(),
                GamesManagementImpl(),

                KajiuGameImpl()
            )
            .build()

        gamesManagementApi = c.get(GamesManagementApi::class.java)
        api = c.get(GameApi::class.java)
    }

    @Test
    fun `should throw api exception if game not created`() {
        assertApiExceptionThrown(
            {
                api.getState(gameToken {
                    gameId = 42
                })
            },
            {
                type = GameNotFoundException::class
                message = "Game for id 42 not found"
            }
        )
    }

    @Test
    fun `should return default state`() {
        val token = gamesManagementApi.create(username())

       val state = api.getState(token)

        assertGameState(state) {

        }
    }
}
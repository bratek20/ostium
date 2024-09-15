package com.github.bratek20.ostium.gamesmanagement.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.assertCreatedGame
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameId
import com.github.bratek20.ostium.user.fixtures.username
import org.assertj.core.api.Assertions.assertThat
import org.junit.jupiter.api.Test

open class GamesManagementImplTest {
    class Context(
        val api: GamesManagementApi,
        val loggerMock: LoggerMock
    )
    open fun createContext(): Context {
        val c = someContextBuilder()
            .withModules(
                LogsMocks(),
                GamesManagementImpl()
            )
            .build()

        return Context(
            api = c.get(GamesManagementApi::class.java),
            loggerMock = c.get(LoggerMock::class.java)
        )
    }

    @Test
    fun `should work`() {
        val c = createContext()
        val api = c.api
        val loggerMock = c.loggerMock

        assertThat(api.getAllCreated()).hasSize(0)

        // create
        api.create(username("test"))
        loggerMock.assertInfos(
            "User `test` created game with id 1"
        )

        val games = api.getAllCreated()
        assertThat(games).hasSize(1)
        assertCreatedGame(games[0]) {
            id = 1
            creator = "test"
            joinerEmpty = true
        }

        // join
        loggerMock.reset()
        api.join(username("test2"), gameId(1))

        loggerMock.assertInfos(
            "User `test2` joined game with id 1"
        )

        val gamesAfterJoin = api.getAllCreated()
        assertThat(gamesAfterJoin).hasSize(1)
        assertCreatedGame(gamesAfterJoin[0]) {
            id = 1
            creator = "test"
            joiner = "test2"
        }

        // create second
        api.create(username("test3"))
        val gamesAfterSecondCreate = api.getAllCreated()
        assertThat(gamesAfterSecondCreate).hasSize(2)
        assertCreatedGame(gamesAfterSecondCreate[1]) {
            id = 2
            creator = "test3"
        }
    }
}
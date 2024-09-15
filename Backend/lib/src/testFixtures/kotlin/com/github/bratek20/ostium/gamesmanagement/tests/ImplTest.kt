package com.github.bratek20.ostium.gamesmanagement.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.assertCreatedGame
import com.github.bratek20.ostium.user.fixtures.username
import org.assertj.core.api.Assertions.assertThat
import org.junit.jupiter.api.Test

open class GamesManagementImplTest {
    open fun createApi(): GamesManagementApi = someContextBuilder()
        .withModules(GamesManagementImpl())
        .get(GamesManagementApi::class.java)

    @Test
    fun `should work`() {
        val api = someContextBuilder()
            .withModules(GamesManagementImpl())
            .get(GamesManagementApi::class.java)

        assertThat(api.getAllCreated()).hasSize(0)

        api.create(username("test"))

        val games = api.getAllCreated()
        assertThat(games).hasSize(1)
        assertCreatedGame(games[0]) {
            id = 1
            creator = "test"
        }

        api.create(username("test2"))

        val games2 = api.getAllCreated()
        assertThat(games2).hasSize(2)
        assertCreatedGame(games2[0]) {
            id = 1
            creator = "test"
        }
        assertCreatedGame(games2[1]) {
            id = 2
            creator = "test2"
        }
    }
}
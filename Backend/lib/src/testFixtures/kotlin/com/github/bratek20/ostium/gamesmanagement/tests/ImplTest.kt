package com.github.bratek20.ostium.gamesmanagement.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.assertCreatedGame
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameId
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

        // create
        api.create(username("test"))

        val games = api.getAllCreated()
        assertThat(games).hasSize(1)
        assertCreatedGame(games[0]) {
            id = 1
            creator = "test"
            joinerEmpty = true
        }

        // join
        api.join(username("test2"), gameId(1))

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
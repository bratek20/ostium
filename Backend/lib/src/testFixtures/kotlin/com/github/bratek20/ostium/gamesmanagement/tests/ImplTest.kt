package com.github.bratek20.ostium.gamesmanagement.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.ostium.gamesmanagement.api.CreatedGamesApi
import com.github.bratek20.ostium.gamesmanagement.context.CreatedGamesImpl
import com.github.bratek20.ostium.gamesmanagement.fixtures.assertCreatedGame
import com.github.bratek20.ostium.user.fixtures.username
import org.assertj.core.api.Assertions.assertThat
import org.junit.jupiter.api.Test

open class CreatedGamesImplTest {
    open fun createApi(): CreatedGamesApi = someContextBuilder()
        .withModules(CreatedGamesImpl())
        .get(CreatedGamesApi::class.java)

    @Test
    fun `should work`() {
        val api = someContextBuilder()
            .withModules(CreatedGamesImpl())
            .get(CreatedGamesApi::class.java)

        assertThat(api.getAll()).hasSize(0)

        api.create(username("test"))

        val games = api.getAll()
        assertThat(games).hasSize(1)
        assertCreatedGame(games[0]) {
            id = 1
            creator = "test"
        }

        api.create(username("test2"))

        val games2 = api.getAll()
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
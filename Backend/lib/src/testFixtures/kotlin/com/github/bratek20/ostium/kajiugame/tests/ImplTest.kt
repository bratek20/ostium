package com.github.bratek20.ostium.kajiugame.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.architecture.exceptions.assertApiExceptionThrown
import com.github.bratek20.ostium.gamesmanagement.fixtures.gameToken
import com.github.bratek20.ostium.kajiugame.api.GameApi
import com.github.bratek20.ostium.kajiugame.api.GameNotFoundException
import com.github.bratek20.ostium.kajiugame.context.KajiuGameImpl
import com.github.bratek20.ostium.singlegame.fixtures.gameState
import org.assertj.core.api.Assertions.assertThat
import org.junit.jupiter.api.Test

class KajiuGameImplTest {
    @Test
    fun `should throw api exception if game not created`() {
        val api = someContextBuilder()
            .withModules(KajiuGameImpl())
            .get(GameApi::class.java)

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
}
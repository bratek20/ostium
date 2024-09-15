package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.ostium.gamesmanagement.api.CreatedGamesApi
import com.github.bratek20.ostium.gamesmanagement.context.CreatedGamesWebClient
import com.github.bratek20.ostium.singlegame.api.GameApi
import com.github.bratek20.ostium.singlegame.context.GameModuleWebClient
import org.junit.jupiter.api.Test

class MainSmokeTest {
    @Test
    fun `main works`() {
        main()

        val httpConfig = httpClientConfig {
            baseUrl = "http://localhost:8080"
        }
        val c = someContextBuilder()
            .withModules(
                HttpClientImpl(),
                CreatedGamesWebClient(httpConfig),
                GameModuleWebClient(httpConfig)
            )

        val createdGamesApi = c.get(CreatedGamesApi::class.java)
        val gameApi = c.get(GameApi::class.java)

        createdGamesApi.getAll()
        gameApi.startGame()
    }
}
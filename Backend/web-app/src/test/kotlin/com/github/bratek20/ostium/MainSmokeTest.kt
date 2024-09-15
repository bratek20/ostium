package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.CreatedGamesWebClient
import com.github.bratek20.ostium.singlegame.api.SingleGameApi
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

        val gamesManagementApi = c.get(GamesManagementApi::class.java)
        val singleGameApi = c.get(SingleGameApi::class.java)

        gamesManagementApi.getAll()
        singleGameApi.startGame()
    }
}
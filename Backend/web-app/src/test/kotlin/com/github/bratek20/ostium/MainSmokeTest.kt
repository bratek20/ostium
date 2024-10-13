package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebClient
import com.github.bratek20.ostium.kaijugame.api.GameApi
import com.github.bratek20.ostium.kaijugame.context.KaijuGameWebClient
import com.github.bratek20.ostium.user.api.Username
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
                GamesManagementWebClient(httpConfig),
                KaijuGameWebClient(httpConfig)
            )

        val gamesManagementApi = c.get(GamesManagementApi::class.java)
        val gameApi = c.get(GameApi::class.java)

        val username = Username("test")

        val gameToken = gamesManagementApi.create(username)
        gameApi.getState(gameToken)
    }
}
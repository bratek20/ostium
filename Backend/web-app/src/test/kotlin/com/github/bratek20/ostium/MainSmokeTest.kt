package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebClient
import com.github.bratek20.ostium.singlegame.api.SingleGameApi
import com.github.bratek20.ostium.singlegame.context.SingleGameWebClient
import com.github.bratek20.ostium.user.api.Username
import org.junit.jupiter.api.Test

class MainSmokeTest {
    @Test
    fun `main works`() {
        //main()

        val httpConfig = httpClientConfig {
            baseUrl = "http://localhost:8080"
        }
        val c = someContextBuilder()
            .withModules(
                HttpClientImpl(),
                GamesManagementWebClient(httpConfig),
                SingleGameWebClient(httpConfig)
            )

        val gamesManagementApi = c.get(GamesManagementApi::class.java)
        val singleGameApi = c.get(SingleGameApi::class.java)

        val username = Username("test")

        val gameId = gamesManagementApi.create(username)
        singleGameApi.getState(gameId, username)
    }
}
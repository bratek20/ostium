package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.infrastructure.httpserver.fixtures.TestWebApp
import com.github.bratek20.ostium.gamesmanagement.api.CreatedGamesApi
import com.github.bratek20.ostium.gamesmanagement.context.CreatedGamesWebClient
import com.github.bratek20.ostium.gamesmanagement.context.CreatedGamesWebServer
import com.github.bratek20.ostium.gamesmanagement.tests.CreatedGamesImplTest

class CreatedGamesIntegrationTest: CreatedGamesImplTest() {
    override fun createApi(): CreatedGamesApi {
        val c = TestWebApp(
            modules = listOf(
                CreatedGamesWebServer(),
            ),
        ).run()

        //TODO-REF helper to create web client - web client factory to create multiple interfaces at once
        return someContextBuilder()
            .withModules(
                HttpClientImpl(),
                CreatedGamesWebClient(
                    config = httpClientConfig {
                        baseUrl = "http://localhost:${c.port}"
                    }
                )
            ).get(CreatedGamesApi::class.java)
    }
}
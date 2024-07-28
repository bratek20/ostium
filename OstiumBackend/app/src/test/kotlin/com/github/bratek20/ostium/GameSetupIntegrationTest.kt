package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.ostium.gamesetup.api.GameSetupApi
import com.github.bratek20.ostium.gamesetup.context.GameSetupWebClient
import com.github.bratek20.ostium.gamesetup.tests.GameSetupImplTest

class GameSetupIntegrationTest: GameSetupImplTest() {
    override fun createApi(): GameSetupApi {
        runWebApp(true)

        return someContextBuilder()
            .withModules(
                HttpClientImpl(),
                GameSetupWebClient(
                    config = httpClientConfig {
                        baseUrl = "http://localhost:8080"
                    }
                )
            )
            .get(GameSetupApi::class.java)
    }
}
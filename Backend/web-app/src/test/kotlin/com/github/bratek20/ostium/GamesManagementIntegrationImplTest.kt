package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.infrastructure.httpserver.fixtures.TestWebApp
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebClient
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebServer
import com.github.bratek20.ostium.gamesmanagement.tests.GamesManagementImplTest

class GamesManagementIntegrationImplTest: GamesManagementImplTest() {
    override fun createContext(): Context {
        val c = TestWebApp(
            modules = listOf(
                GamesManagementWebServer(),
            ),
        ).run()

        //TODO-REF helper to create web client - web client factory to create multiple interfaces at once
        val api = someContextBuilder()
            .withModules(
                HttpClientImpl(),
                GamesManagementWebClient(
                    config = httpClientConfig {
                        baseUrl = "http://localhost:${c.port}"
                    }
                )
            ).get(GamesManagementApi::class.java)

        return Context(
            api = api,
            loggerMock = c.get(LoggerMock::class.java)
        )
    }
}
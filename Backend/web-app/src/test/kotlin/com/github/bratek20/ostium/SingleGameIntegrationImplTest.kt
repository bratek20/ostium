package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.infrastructure.httpserver.api.WebServerModule
import com.github.bratek20.infrastructure.httpserver.fixtures.TestWebApp
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.gamesmanagement.api.GamesManagementApi
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebServer
import com.github.bratek20.ostium.singlegame.api.SingleGameApi
import com.github.bratek20.ostium.singlegame.context.SingleGameWebClient
import com.github.bratek20.ostium.singlegame.context.SingleGameWebServer
import com.github.bratek20.ostium.singlegame.tests.SingleGameImplTest

//TODO-REF TestWebApp should accept also normal modules
class WebServerLogMocks: WebServerModule {
    override fun getControllers(): List<Class<*>> {
        return listOf()
    }

    override fun getImpl(): ContextModule {
        return LogsMocks()
    }
}

class SingleGameIntegrationImplTest: SingleGameImplTest() {
    override fun createContext(): Context {
        val c = TestWebApp(
            modules = listOf(
                GamesManagementWebServer(),
                SingleGameWebServer(),
                WebServerLogMocks()
            ),
        ).run()

        val api = someContextBuilder()
            .withModules(
                HttpClientImpl(),
                SingleGameWebClient(
                    config = httpClientConfig {
                        baseUrl = "http://localhost:${c.port}"
                    }
                )
            ).get(SingleGameApi::class.java)

        return Context(
            managementApi = c.get(GamesManagementApi::class.java),
            api = api,
            loggerMock = c.get(LoggerMock::class.java)
        )
    }
}
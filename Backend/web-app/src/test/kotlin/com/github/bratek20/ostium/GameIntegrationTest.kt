package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.infrastructure.httpclient.context.HttpClientImpl
import com.github.bratek20.infrastructure.httpclient.fixtures.httpClientConfig
import com.github.bratek20.infrastructure.httpserver.api.WebServerModule
import com.github.bratek20.infrastructure.httpserver.fixtures.TestWebApp
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.ostium.game.api.GameApi
import com.github.bratek20.ostium.game.context.GameWebClient
import com.github.bratek20.ostium.game.context.GameWebServer
import com.github.bratek20.ostium.game.tests.GameImplTest

//TODO-REF TestWebApp should accept also normal modules
class WebServerLogMocks: WebServerModule {
    override fun getControllers(): List<Class<*>> {
        return listOf()
    }

    override fun getImpl(): ContextModule {
        return LogsMocks()
    }
}

class GameIntegrationTest: GameImplTest() {
    override fun createContext(): Context {
        val c = TestWebApp(
            modules = listOf(
                GameWebServer(),
                WebServerLogMocks()
            ),
        ).run()

        val api = someContextBuilder()
            .withModules(
                HttpClientImpl(),
                GameWebClient(
                    config = httpClientConfig {
                        baseUrl = "http://localhost:${c.port}"
                    }
                )
            ).get(GameApi::class.java)

        return Context(
            api = api,
            loggerMock = c.get(LoggerMock::class.java)
        )
    }
}
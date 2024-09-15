package com.github.bratek20.ostium

import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.infrastructure.httpserver.api.WebServerModule
import com.github.bratek20.logs.api.Logger
import com.github.bratek20.logs.context.Slf4jLogsImpl
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebServer
import com.github.bratek20.ostium.singlegame.context.SingleGameWebServer
import com.github.bratek20.spring.webapp.SpringWebApp

//TODO-REF SpringWebApp should accept also normal modules
class LogsWebServer: WebServerModule {
    override fun getControllers(): List<Class<*>> {
        return listOf()
    }

    override fun getImpl(): ContextModule {
        return Slf4jLogsImpl()
    }
}

fun main() {
    SpringWebApp(
        modules = listOf(
            GamesManagementWebServer(),
            SingleGameWebServer(),
            LogsWebServer()
        ),
    ).run()
        .get(Logger::class.java)
        .info("Server is running!")
}
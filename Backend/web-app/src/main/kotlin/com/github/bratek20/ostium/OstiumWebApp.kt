package com.github.bratek20.ostium

import com.github.bratek20.logs.api.Logger
import com.github.bratek20.logs.logback.api.LogbackHelper
import com.github.bratek20.logs.logback.context.LogsLogbackImpl
import com.github.bratek20.ostium.gamesmanagement.context.GamesManagementWebServer
import com.github.bratek20.ostium.singlegame.context.SingleGameWebServer
import com.github.bratek20.spring.webapp.SpringWebApp

val version = "0.0.1"
fun main() {
    val c = SpringWebApp(
        modules = listOf(
            GamesManagementWebServer(),
            SingleGameWebServer(),
            LogsLogbackImpl()
        ),
    ).run()

    c.get(LogbackHelper::class.java)
        .setOutputFile("logs.txt")
    c.get(Logger::class.java)
        .info("Server is running! Version: $version")
}
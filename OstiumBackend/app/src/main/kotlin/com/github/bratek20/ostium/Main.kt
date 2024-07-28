package com.github.bratek20.ostium

import com.github.bratek20.ostium.gamesetup.context.GameSetupWebServer
import com.github.bratek20.spring.webapp.SpringWebApp

fun main() {
    runWebApp(false)
}

fun runWebApp(useRandomPort: Boolean) {
    SpringWebApp(
        modules = listOf(
            GameSetupWebServer()
        ),
        useRandomPort = useRandomPort
    ).run()
}
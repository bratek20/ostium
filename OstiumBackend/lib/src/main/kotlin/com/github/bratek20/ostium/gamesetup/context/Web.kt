// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesetup.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.infrastructure.httpclient.api.HttpClientConfig
import com.github.bratek20.infrastructure.httpserver.api.WebServerModule

import com.github.bratek20.ostium.gamesetup.api.*
import com.github.bratek20.ostium.gamesetup.web.*

class GameSetupWebClient(
    private val config: HttpClientConfig
): ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImplObject(GameSetupWebClientConfig::class.java, GameSetupWebClientConfig(config))
            .setImpl(GameSetupApi::class.java, GameSetupApiWebClient::class.java)
    }
}

class GameSetupWebServer: WebServerModule {
    override fun getImpl(): ContextModule {
        return GameSetupImpl()
    }

    override fun getControllers(): List<Class<*>> {
        return listOf(
            GameSetupApiController::class.java,
        )
    }
}
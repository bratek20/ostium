// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamemodule.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.infrastructure.httpclient.api.HttpClientConfig
import com.github.bratek20.infrastructure.httpserver.api.WebServerModule

import com.github.bratek20.ostium.gamemodule.api.*
import com.github.bratek20.ostium.gamemodule.web.*

class GameModuleWebClient(
    private val config: HttpClientConfig
): ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImplObject(GameModuleWebClientConfig::class.java, GameModuleWebClientConfig(config))
            .setImpl(GameApi::class.java, GameApiWebClient::class.java)
    }
}

class GameModuleWebServer: WebServerModule {
    override fun getImpl(): ContextModule {
        return GameModuleImpl()
    }

    override fun getControllers(): List<Class<*>> {
        return listOf(
            GameApiController::class.java,
        )
    }
}
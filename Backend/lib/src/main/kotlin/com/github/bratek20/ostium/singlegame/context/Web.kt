// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.singlegame.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.infrastructure.httpclient.api.HttpClientConfig
import com.github.bratek20.infrastructure.httpserver.api.WebServerModule

import com.github.bratek20.ostium.singlegame.api.*
import com.github.bratek20.ostium.singlegame.web.*

class SingleGameWebClient(
    private val config: HttpClientConfig
): ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImplObject(SingleGameWebClientConfig::class.java, SingleGameWebClientConfig(config))
            .setImpl(SingleGameApi::class.java, SingleGameApiWebClient::class.java)
    }
}

class SingleGameWebServer: WebServerModule {
    override fun apply(builder: ContextBuilder) {
        builder.withModule(SingleGameImpl())
    }

    override fun getControllers(): List<Class<*>> {
        return listOf(
            SingleGameApiController::class.java,
        )
    }
}
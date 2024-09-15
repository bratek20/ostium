package com.github.bratek20.ostium.gamesmanagement.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.gamesmanagement.api.*
import com.github.bratek20.ostium.gamesmanagement.impl.*

class GamesManagementImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(GamesManagementApi::class.java, GamesManagementApiLogic::class.java)
    }
}
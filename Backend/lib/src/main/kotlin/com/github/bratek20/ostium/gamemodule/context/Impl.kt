package com.github.bratek20.ostium.gamemodule.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.gamemodule.api.*
import com.github.bratek20.ostium.gamemodule.impl.*

class GameModuleImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(GameApi::class.java, GameApiLogic::class.java)
    }
}
package com.github.bratek20.ostium.gamesetup.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.gamesetup.api.*
import com.github.bratek20.ostium.gamesetup.impl.*

class GameSetupImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(GameSetupApi::class.java, GameSetupApiLogic::class.java)
    }
}
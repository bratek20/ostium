package com.github.bratek20.ostium.kaijugame.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.kaijugame.api.*
import com.github.bratek20.ostium.kaijugame.impl.*

class KaijuGameImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(GameApi::class.java, GameApiLogic::class.java)
    }
}
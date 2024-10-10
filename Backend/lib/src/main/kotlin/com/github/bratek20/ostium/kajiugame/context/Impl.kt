package com.github.bratek20.ostium.kajiugame.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.kajiugame.api.*
import com.github.bratek20.ostium.kajiugame.impl.*

class KajiuGameImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(GameApi::class.java, GameApiLogic::class.java)
    }
}
package com.github.bratek20.ostium.singlegame.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.singlegame.api.*
import com.github.bratek20.ostium.singlegame.impl.*

class GameModuleImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(SingleGameApi::class.java, SingleGameApiLogic::class.java)
    }
}
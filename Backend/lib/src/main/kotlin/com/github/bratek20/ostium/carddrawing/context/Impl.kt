package com.github.bratek20.ostium.carddrawing.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.carddrawing.api.*
import com.github.bratek20.ostium.carddrawing.impl.*

class CardDrawingImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(CardDrawer::class.java, CardDrawerLogic::class.java)
            .setImpl(CardDrawerFactory::class.java, CardDrawerFactoryLogic::class.java)
    }
}
package com.github.bratek20.ostium.carddrawer.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.ostium.carddrawer.api.*
import com.github.bratek20.ostium.carddrawer.impl.*

class CardDrawerImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(CardDrawerApi::class.java, CardDrawerApiLogic::class.java)
    }
}
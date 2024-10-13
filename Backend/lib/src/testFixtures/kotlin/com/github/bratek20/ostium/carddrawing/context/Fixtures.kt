package com.github.bratek20.ostium.carddrawing.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.ostium.carddrawing.api.CardDrawerFactory
import com.github.bratek20.ostium.carddrawing.fixtures.CardDrawerFactoryMock

class CardDrawerMocks: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder.setImpl(CardDrawerFactory::class.java, CardDrawerFactoryMock::class.java)
    }
}
package com.github.bratek20.ostium.carddrawing.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.ostium.carddrawing.api.CardDrawerApi
import com.github.bratek20.ostium.carddrawing.fixtures.CardDrawerApiMock

class CardDrawerMocks: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder.setImpl(CardDrawerApi::class.java, CardDrawerApiMock::class.java)
    }
}
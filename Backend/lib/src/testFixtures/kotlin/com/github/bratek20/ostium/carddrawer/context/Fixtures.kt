package com.github.bratek20.ostium.carddrawer.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule
import com.github.bratek20.ostium.carddrawer.api.CardDrawerApi
import com.github.bratek20.ostium.carddrawer.fixtures.CardDrawerApiMock

class CardDrawerMocks: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder.setImpl(CardDrawerApi::class.java, CardDrawerApiMock::class.java)
    }
}
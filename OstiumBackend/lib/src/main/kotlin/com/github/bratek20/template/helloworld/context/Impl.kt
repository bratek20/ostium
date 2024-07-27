package com.github.bratek20.template.helloworld.context

import com.github.bratek20.architecture.context.api.ContextBuilder
import com.github.bratek20.architecture.context.api.ContextModule

import com.github.bratek20.template.helloworld.api.*
import com.github.bratek20.template.helloworld.impl.*

class HelloWorldImpl: ContextModule {
    override fun apply(builder: ContextBuilder) {
        builder
            .setImpl(HelloWorldApi::class.java, HelloWorldApiLogic::class.java)
    }
}
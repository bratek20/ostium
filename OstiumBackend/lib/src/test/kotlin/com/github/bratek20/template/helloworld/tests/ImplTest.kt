package com.github.bratek20.template.helloworld.tests

import com.github.bratek20.architecture.context.someContextBuilder
import com.github.bratek20.logs.LoggerMock
import com.github.bratek20.logs.LogsMocks
import com.github.bratek20.template.helloworld.api.HelloWorldApi
import com.github.bratek20.template.helloworld.context.HelloWorldImpl
import org.assertj.core.api.Assertions.assertThat
import org.junit.jupiter.api.Test

class HelloWorldImplTest {
    @Test
    fun `should log hello world`() {
        //given
        val c = someContextBuilder()
            .withModules(
                HelloWorldImpl(),

                LogsMocks()
            )
            .build()

        val api = c.get(HelloWorldApi::class.java)
        val loggerMock = c.get(LoggerMock::class.java)

        //when
        api.sayHello()

        //then
        loggerMock.assertInfos("Hello World!")
    }
}
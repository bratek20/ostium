// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesetup.web

import com.github.bratek20.architecture.serialization.api.Serializer
import com.github.bratek20.architecture.serialization.api.Struct
import com.github.bratek20.architecture.serialization.context.SerializationFactory

import org.springframework.web.bind.annotation.PostMapping
import org.springframework.web.bind.annotation.RequestBody
import org.springframework.web.bind.annotation.RequestMapping
import org.springframework.web.bind.annotation.RestController

import com.github.bratek20.ostium.gamesetup.api.*

@RestController
@RequestMapping("/gameSetupApi")
class GameSetupApiController(
    private val api: GameSetupApi,
) {
    private val serializer: Serializer = SerializationFactory.createSerializer()

    @PostMapping("/startGame")
    fun startGame(): Struct {
        // no request needed
        return serializer.asStruct(GameSetupApiStartGameResponse(api.startGame()))
    }

    @PostMapping("/playCard")
    fun playCard(@RequestBody rawRequest: Struct): Struct {
        val request = serializer.fromStruct(rawRequest, GameSetupApiPlayCardRequest::class.java)
        return serializer.asStruct(GameSetupApiPlayCardResponse(api.playCard(request.getCardId(), request.getRow())))
    }

    @PostMapping("/moveCard")
    fun moveCard(@RequestBody rawRequest: Struct): Struct {
        val request = serializer.fromStruct(rawRequest, GameSetupApiMoveCardRequest::class.java)
        return serializer.asStruct(GameSetupApiMoveCardResponse(api.moveCard(request.getCardId(), request.getFrom(), request.getTo())))
    }
}

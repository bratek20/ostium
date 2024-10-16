// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.kaijugame.web

import com.github.bratek20.infrastructure.httpclient.api.HttpClientFactory

import com.github.bratek20.ostium.kaijugame.api.*

import com.github.bratek20.ostium.gamesmanagement.api.*

class GameApiWebClient(
    factory: HttpClientFactory,
    config: KaijuGameWebClientConfig,
): GameApi {
    private val client = factory.create(config.value)

    override fun getState(token: GameToken): GameState {
        return client.post("/ostium/gameApi/getState", GameApiGetStateRequest.create(token)).getBody(GameApiGetStateResponse::class.java).getValue()
    }

    override fun endPhase(token: GameToken): GameState {
        return client.post("/ostium/gameApi/endPhase", GameApiEndPhaseRequest.create(token)).getBody(GameApiEndPhaseResponse::class.java).getValue()
    }

    override fun playCard(token: GameToken, handCardIdx: Int): GameState {
        return client.post("/ostium/gameApi/playCard", GameApiPlayCardRequest.create(token, handCardIdx)).getBody(GameApiPlayCardResponse::class.java).getValue()
    }

    override fun assignDamage(token: GameToken, zone: HitZonePosition, damageType: DamageType): GameState {
        return client.post("/ostium/gameApi/assignDamage", GameApiAssignDamageRequest.create(token, zone, damageType)).getBody(GameApiAssignDamageResponse::class.java).getValue()
    }

    override fun assignGuard(token: GameToken, zone: HitZonePosition, damageType: DamageType): GameState {
        return client.post("/ostium/gameApi/assignGuard", GameApiAssignGuardRequest.create(token, zone, damageType)).getBody(GameApiAssignGuardResponse::class.java).getValue()
    }
}


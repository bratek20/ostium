// DO NOT EDIT! Autogenerated by HLA tool

package com.github.bratek20.ostium.gamesmanagement.web

import com.github.bratek20.infrastructure.httpclient.api.HttpClientFactory

import com.github.bratek20.ostium.gamesmanagement.api.*

import com.github.bratek20.ostium.user.api.*

class GamesManagementApiWebClient(
    factory: HttpClientFactory,
    config: GamesManagementWebClientConfig,
): GamesManagementApi {
    private val client = factory.create(config.value)

    override fun create(creator: Username): GameId {
        return client.post("/ostium/gamesManagementApi/create", GamesManagementApiCreateRequest.create(creator)).getBody(GamesManagementApiCreateResponse::class.java).value
    }

    override fun join(joiner: Username, gameId: GameId): Unit {
        client.post("/ostium/gamesManagementApi/join", GamesManagementApiJoinRequest.create(joiner, gameId))
    }

    override fun delete(gameId: GameId): Unit {
        client.post("/ostium/gamesManagementApi/delete", GamesManagementApiDeleteRequest.create(gameId))
    }

    override fun getAllCreated(): List<CreatedGame> {
        return client.post("/ostium/gamesManagementApi/getAllCreated", null).getBody(GamesManagementApiGetAllCreatedResponse::class.java).value
    }
}

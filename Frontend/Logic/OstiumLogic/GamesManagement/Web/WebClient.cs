// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;
using HttpClientModule.Api;
using GamesManagement.Api;
using User.Api;

namespace GamesManagement.Web {
    public class GamesManagementApiWebClient: GamesManagementApi {
        readonly HttpClient client;

        public GamesManagementApiWebClient(
            HttpClientFactory factory,
            GamesManagementWebClientConfig config
        ) {
            this.client = factory.Create(config.Value);
        }
        public GameId Create(Username creator) {
            return client.Post("/ostium/gamesManagementApi/create", Optional<GamesManagementApiCreateRequest>.Of(GamesManagementApiCreateRequest.Create(creator))).GetBody<GamesManagementApiCreateResponse>().Get().Value;
        }
        public void Join(Username joiner, GameId gameId) {
            client.Post("/ostium/gamesManagementApi/join", Optional<GamesManagementApiJoinRequest>.Of(GamesManagementApiJoinRequest.Create(joiner, gameId)));
        }
        public void Delete(GameId gameId) {
            client.Post("/ostium/gamesManagementApi/delete", Optional<GamesManagementApiDeleteRequest>.Of(GamesManagementApiDeleteRequest.Create(gameId)));
        }
        public List<CreatedGame> GetAllCreated() {
            return client.Post("/ostium/gamesManagementApi/getAllCreated", Optional<object>.Empty()).GetBody<GamesManagementApiGetAllCreatedResponse>().Get().Value;
        }
    }
}
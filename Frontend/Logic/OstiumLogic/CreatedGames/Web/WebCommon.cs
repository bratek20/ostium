// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;
using HttpClientModule.Api;
using CreatedGames.Api;
using User.Api;

namespace CreatedGames.Web {
    public class CreatedGamesWebClientConfig {
        public HttpClientConfig Value { get; }

        public CreatedGamesWebClientConfig(
            HttpClientConfig value
        ) {
            Value = value;
        }
    }

    public class CreatedGamesApiGetAllResponse {
        public List<CreatedGame> Value { get; }

        public CreatedGamesApiGetAllResponse(
            List<CreatedGame> value
        ) {
            Value = value;
        }
    }

    public class CreatedGamesApiCreateRequest {
        readonly string creator;

        public CreatedGamesApiCreateRequest(
            string creator
        ) {
            this.creator = creator;
        }
        public Username GetCreator() {
            return new Username(creator);
        }
        public static CreatedGamesApiCreateRequest Create(Username creator) {
            return new CreatedGamesApiCreateRequest(creator.Value);
        }
    }

    public class CreatedGamesApiCreateResponse {
        public GameId Value { get; }

        public CreatedGamesApiCreateResponse(
            GameId value
        ) {
            Value = value;
        }
    }
}
// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;
using HttpClientModule.Api;
using GameModule.Api;

namespace GameModule.Web {
    public class GameApiWebClient: GameApi {
        readonly HttpClient client;

        public GameApiWebClient(
            HttpClientFactory factory,
            GameModuleWebClientConfig config
        ) {
            this.client = factory.Create(config.Value);
        }
        public Game StartGame() {
            return client.Post("/gameApi/startGame", Optional<object>.Empty()).GetBody<GameApiStartGameResponse>().Get().Value;
        }
        public Game PlayCard(CreatureCardId cardId, RowType row) {
            return client.Post("/gameApi/playCard", Optional<GameApiPlayCardRequest>.Of(GameApiPlayCardRequest.Create(cardId, row))).GetBody<GameApiPlayCardResponse>().Get().Value;
        }
        public Game MoveCard(CreatureCardId cardId, RowType from, RowType to) {
            return client.Post("/gameApi/moveCard", Optional<GameApiMoveCardRequest>.Of(GameApiMoveCardRequest.Create(cardId, from, to))).GetBody<GameApiMoveCardResponse>().Get().Value;
        }
    }
}
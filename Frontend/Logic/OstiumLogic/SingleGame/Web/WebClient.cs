// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;
using HttpClientModule.Api;
using SingleGame.Api;
using GamesManagement.Api;
using User.Api;

namespace SingleGame.Web {
    public class SingleGameApiWebClient: SingleGameApi {
        readonly HttpClient client;

        public SingleGameApiWebClient(
            HttpClientFactory factory,
            SingleGameWebClientConfig config
        ) {
            this.client = factory.Create(config.Value);
        }
        /// <exception cref="GameNotFoundException"/>
        public GameState GetState(GameId gameId, Username user) {
            return client.Post("/ostium/singleGameApi/getState", Optional<SingleGameApiGetStateRequest>.Of(SingleGameApiGetStateRequest.Create(gameId, user))).GetBody<SingleGameApiGetStateResponse>().Get().Value;
        }
        public GameState PlayCard(GameId gameId, Username user, CreatureCardId cardId, RowType row) {
            return client.Post("/ostium/singleGameApi/playCard", Optional<SingleGameApiPlayCardRequest>.Of(SingleGameApiPlayCardRequest.Create(gameId, user, cardId, row))).GetBody<SingleGameApiPlayCardResponse>().Get().Value;
        }
        public GameState MoveCard(GameId gameId, Username user, CreatureCardId cardId, RowType from, RowType to) {
            return client.Post("/ostium/singleGameApi/moveCard", Optional<SingleGameApiMoveCardRequest>.Of(SingleGameApiMoveCardRequest.Create(gameId, user, cardId, from, to))).GetBody<SingleGameApiMoveCardResponse>().Get().Value;
        }
    }
}
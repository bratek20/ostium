// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;
using HttpClientModule.Api;
using SingleGame.Api;
using GamesManagement.Api;
using User.Api;

namespace SingleGame.Web {
    public class SingleGameWebClientConfig {
        public HttpClientConfig Value { get; }

        public SingleGameWebClientConfig(
            HttpClientConfig value
        ) {
            Value = value;
        }
    }

    public class SingleGameApiGetStateRequest {
        readonly int gameId;
        readonly string user;

        public SingleGameApiGetStateRequest(
            int gameId,
            string user
        ) {
            this.gameId = gameId;
            this.user = user;
        }
        public GameId GetGameId() {
            return new GameId(gameId);
        }
        public Username GetUser() {
            return new Username(user);
        }
        public static SingleGameApiGetStateRequest Create(GameId gameId, Username user) {
            return new SingleGameApiGetStateRequest(gameId.Value, user.Value);
        }
    }

    public class SingleGameApiGetStateResponse {
        readonly GameState value;

        public SingleGameApiGetStateResponse(
            GameState value
        ) {
            this.value = value;
        }
        public GameState GetValue() {
            return value;
        }
    }

    public class SingleGameApiPlayCardRequest {
        readonly int gameId;
        readonly string user;
        readonly string cardId;
        readonly string row;

        public SingleGameApiPlayCardRequest(
            int gameId,
            string user,
            string cardId,
            string row
        ) {
            this.gameId = gameId;
            this.user = user;
            this.cardId = cardId;
            this.row = row;
        }
        public GameId GetGameId() {
            return new GameId(gameId);
        }
        public Username GetUser() {
            return new Username(user);
        }
        public CreatureCardId GetCardId() {
            return new CreatureCardId(cardId);
        }
        public RowType GetRow() {
            return (RowType)Enum.Parse(typeof(RowType), row);
        }
        public static SingleGameApiPlayCardRequest Create(GameId gameId, Username user, CreatureCardId cardId, RowType row) {
            return new SingleGameApiPlayCardRequest(gameId.Value, user.Value, cardId.Value, row.ToString());
        }
    }

    public class SingleGameApiPlayCardResponse {
        readonly GameState value;

        public SingleGameApiPlayCardResponse(
            GameState value
        ) {
            this.value = value;
        }
        public GameState GetValue() {
            return value;
        }
    }

    public class SingleGameApiMoveCardRequest {
        readonly int gameId;
        readonly string user;
        readonly string cardId;
        readonly string from;
        readonly string to;

        public SingleGameApiMoveCardRequest(
            int gameId,
            string user,
            string cardId,
            string from,
            string to
        ) {
            this.gameId = gameId;
            this.user = user;
            this.cardId = cardId;
            this.from = from;
            this.to = to;
        }
        public GameId GetGameId() {
            return new GameId(gameId);
        }
        public Username GetUser() {
            return new Username(user);
        }
        public CreatureCardId GetCardId() {
            return new CreatureCardId(cardId);
        }
        public RowType GetFrom() {
            return (RowType)Enum.Parse(typeof(RowType), from);
        }
        public RowType GetTo() {
            return (RowType)Enum.Parse(typeof(RowType), to);
        }
        public static SingleGameApiMoveCardRequest Create(GameId gameId, Username user, CreatureCardId cardId, RowType from, RowType to) {
            return new SingleGameApiMoveCardRequest(gameId.Value, user.Value, cardId.Value, from.ToString(), to.ToString());
        }
    }

    public class SingleGameApiMoveCardResponse {
        readonly GameState value;

        public SingleGameApiMoveCardResponse(
            GameState value
        ) {
            this.value = value;
        }
        public GameState GetValue() {
            return value;
        }
    }
}
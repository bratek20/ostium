// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;
using HttpClientModule.Api;
using GameModule.Api;

namespace GameModule.Web {
    public class GameModuleWebClientConfig {
        public HttpClientConfig Value { get; }

        public GameModuleWebClientConfig(
            HttpClientConfig value
        ) {
            Value = value;
        }
    }

    public class GameApiStartGameResponse {
        public Game Value { get; }

        public GameApiStartGameResponse(
            Game value
        ) {
            Value = value;
        }
    }

    public class GameApiPlayCardRequest {
        readonly string cardId;
        readonly string row;

        public GameApiPlayCardRequest(
            string cardId,
            string row
        ) {
            this.cardId = cardId;
            this.row = row;
        }
        public CreatureCardId GetCardId() {
            return new CreatureCardId(cardId);
        }
        public RowType GetRow() {
            return (RowType)Enum.Parse(typeof(RowType), row);
        }
        public static GameApiPlayCardRequest Create(CreatureCardId cardId, RowType row) {
            return new GameApiPlayCardRequest(cardId.Value, row.ToString());
        }
    }

    public class GameApiPlayCardResponse {
        public Game Value { get; }

        public GameApiPlayCardResponse(
            Game value
        ) {
            Value = value;
        }
    }

    public class GameApiMoveCardRequest {
        readonly string cardId;
        readonly string from;
        readonly string to;

        public GameApiMoveCardRequest(
            string cardId,
            string from,
            string to
        ) {
            this.cardId = cardId;
            this.from = from;
            this.to = to;
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
        public static GameApiMoveCardRequest Create(CreatureCardId cardId, RowType from, RowType to) {
            return new GameApiMoveCardRequest(cardId.Value, from.ToString(), to.ToString());
        }
    }

    public class GameApiMoveCardResponse {
        public Game Value { get; }

        public GameApiMoveCardResponse(
            Game value
        ) {
            Value = value;
        }
    }
}
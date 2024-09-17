using System;
using B20.Tests.ExtraAsserts;
using GamesManagement.Api;
using SingleGame.Api;
using User.Api;

namespace SingleGame
{
    public class SingleGameApiMock : SingleGameApi
    {
        private GameState game = SingleGameBuilders.BuildGame();
        
        public void SetGame(Action<SingleGameBuilders.GameDef> init)
        {
            game = SingleGameBuilders.BuildGame(init);
        }
        
        public GameState StartGame()
        {
            return game;
        }

        private int calls = 0;
        
        private CreatureCardId lastPlayedCard;
        private RowType lastPlayedRow;
        public GameState PlayCard(GameId gameId, Username user, CreatureCardId cardId, RowType row)
        {
            calls++;
            lastPlayedCard = cardId;
            lastPlayedRow = row;
            return game;
        }

        public void AssertPlayCardLastCall(String expectedCardId, RowType expectedRow)
        {
            AssertExt.Equal(lastPlayedCard, new CreatureCardId(expectedCardId));
            AssertExt.Equal(lastPlayedRow, expectedRow);
        }

        private GameId lastMoveGameId;
        private Username lastMoveUser;
        private CreatureCardId lastMoveCard;
        private RowType lastMoveFrom;
        private RowType lastMoveTo;
        public GameState MoveCard(GameId gameId, Username user, CreatureCardId cardId, RowType from, RowType to)
        {
            calls++;
            lastMoveGameId = gameId;
            lastMoveUser = user;
            lastMoveCard = cardId;
            lastMoveFrom = from;
            lastMoveTo = to;
            return game;
        }

        public void AssertMoveCardLastCall(
            int expectedGameId,
            string expectedUser,
            string expectedCardId, 
            RowType expectedFrom, 
            RowType expectedTo
        )
        {
            AssertExt.Equal(lastMoveGameId, new GameId(expectedGameId));
            AssertExt.Equal(lastMoveUser, new Username(expectedUser));
            AssertExt.Equal(lastMoveCard.Value, expectedCardId);
            AssertExt.Equal(lastMoveFrom, expectedFrom);
            AssertExt.Equal(lastMoveTo, expectedTo);
        }

        public void AssertNoCalls()
        {
            AssertExt.Equal(calls, 0);
        }

        public GameState GetState(GameId gameId, Username user)
        {
            return game;
        }
    }
}
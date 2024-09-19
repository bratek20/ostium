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
        
        private int calls = 0;
        
        private GameId lastPlayedGameId;
        private Username lastPlayedUser;
        private CreatureCardId lastPlayedCard;
        private RowType lastPlayedRow;
        public GameState PlayCard(GameId gameId, Username user, CreatureCardId cardId, RowType row)
        {
            calls++;
            lastPlayedGameId = gameId;
            lastPlayedUser = user;
            lastPlayedCard = cardId;
            lastPlayedRow = row;
            return game;
        }

        public void AssertPlayCardLastCall(
            int expectedGameId,
            string expectedUser,
            String expectedCardId, 
            RowType expectedRow
        )
        {
            AssertExt.Equal(lastPlayedGameId, new GameId(expectedGameId));
            AssertExt.Equal(lastPlayedUser, new Username(expectedUser));
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

        private GameId lastGetStateGameId;
        private Username lastGetStateUser;
        private int getStateCalls = 0;
        
        public GameState GetState(GameId gameId, Username user)
        {
            getStateCalls++;
            lastGetStateGameId = gameId;
            lastGetStateUser = user;
            return game;
        }
        
        public void AssertGetStateLastCall(
            int expectedGameId,
            string expectedUser
        )
        {
            AssertExt.Equal(lastGetStateGameId, new GameId(expectedGameId));
            AssertExt.Equal(lastGetStateUser, new Username(expectedUser));
        }
        
        public void AssertGetStateCallsNumber(int expectedCalls)
        {
            AssertExt.Equal(getStateCalls, expectedCalls);
        }
    }
}
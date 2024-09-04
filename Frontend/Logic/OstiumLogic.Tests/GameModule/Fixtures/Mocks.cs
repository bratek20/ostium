using System;
using B20.Tests.ExtraAsserts;
using GameModule.Api;

namespace GameModule
{
    public class GameApiMock : GameApi
    {
        private Game game = Builders.BuildGame();
        
        public void SetGame(Action<Builders.GameDef> init)
        {
            game = Builders.BuildGame(init);
        }
        
        public Game StartGame()
        {
            return game;
        }

        private int calls = 0;
        
        private CreatureCardId lastPlayedCard;
        private RowType lastPlayedRow;
        public Game PlayCard(CreatureCardId cardId, RowType row)
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

        private CreatureCardId lastMoveCard;
        private RowType lastMoveFrom;
        private RowType lastMoveTo;
        public Game MoveCard(CreatureCardId cardId, RowType from, RowType to)
        {
            calls++;
            lastMoveCard = cardId;
            lastMoveFrom = from;
            lastMoveTo = to;
            return game;
        }

        public void AssertMoveCardLastCall(string expectedCardId, RowType expectedFrom, RowType expectedTo)
        {
            AssertExt.Equal(lastMoveCard.Value, expectedCardId);
            AssertExt.Equal(lastMoveFrom, expectedFrom);
            AssertExt.Equal(lastMoveTo, expectedTo);
        }

        public void AssertNoCalls()
        {
            AssertExt.Equal(calls, 0);
        }
    }
}
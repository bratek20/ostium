using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Tests.Frontend.Traits.Fixtures;
using GameComponents;
using Xunit;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicTest
    {
        private Scenarios scenarios = new Scenarios();
        
        [Fact]
        public void ShouldStartOnMainScreenAndSwitchToGameScreen()
        {
            var c = scenarios.Setup();
            c.Logic.RegisterWindows();
            
            //should not throw
            c.WindowManager.Get(WindowIds.MAIN_WINDOW);
            c.WindowManager.Get(WindowIds.GAME_WINDOW);
            
            c.Logic.Start();
            
            Assert.Equal(WindowIds.MAIN_WINDOW, c.WindowManager.GetCurrent());
            
            //Clicking play button
            (c.WindowManager.Get(WindowIds.MAIN_WINDOW) as MainWindow).PlayButton.Click();
            
            Assert.Equal(WindowIds.GAME_WINDOW, c.WindowManager.GetCurrent());
        }

        [Fact]
        public void ShouldPlayCards()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
            });
            
            //Playing first card on attack row
            var card1Name = c.FirstCardInHand.Name.Model;
            Assert.Equal("Mouse1", card1Name);
            
            Helpers.StartDrag(c.FirstCardInHand, new Position2d(0, 0));
            AssertSelectedCard(c, "Mouse1");
            Helpers.EndDrag(c.FirstCardInHand, new Position2d(10, 10));
            
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertNoCardSelected(c);
            
            Assert.Equal(c.CardsInHand.Count, 1);
            Assert.Equal(c.FirstCardInHand.Name.Model, "Mouse2");
            
            //Playing second card on defense row
            Helpers.StartDrag(c.FirstCardInHand, new Position2d(0, 0));
            Helpers.EndDrag(c.FirstCardInHand, new Position2d(20, 20));
            
            AssertCardInRow(c.DefenseRow, "Mouse2");
        }
        
        [Fact]
        public void ShouldMovePlayedCardBetweenRowsByDragging()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
            });
            
            Helpers.StartDrag(c.FirstCardInHand, new Position2d(0, 0));
            AssertSelectedCard(c, "Mouse1");
            Helpers.EndDrag(c.FirstCardInHand, new Position2d(10, 10));
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertNoCardSelected(c);
            
            Helpers.StartDrag(c.AttackRow.Card, new Position2d(10, 10));
            AssertSelectedCard(c, "Mouse1");
            Helpers.EndDrag(c.AttackRow.Card, new Position2d(20, 20));
            AssertCardInRow(c.DefenseRow, "Mouse1");
            AssertRowEmpty(c.AttackRow);
            AssertNoCardSelected(c);
            
            Helpers.StartDrag(c.DefenseRow.Card, new Position2d(20, 20));
            AssertSelectedCard(c, "Mouse1");
            Helpers.EndDrag(c.DefenseRow.Card, new Position2d(10, 10));
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertRowEmpty(c.DefenseRow);
            AssertNoCardSelected(c);
        }

        [Fact]
        public void ShouldSwapPlayedCards()
        {
            var c = scenarios.InGameWindow(i =>
            {
                i.AttackRowRect = new Rect(new Position2d(10, 10), new Position2d(1, 1));
                i.DefenseRowRect = new Rect(new Position2d(20, 20), new Position2d(1, 1));
            });
            
            Helpers.StartDrag(c.FirstCardInHand, new Position2d(0, 0));
            Helpers.EndDrag(c.FirstCardInHand, new Position2d(10, 10));
            AssertCardInRow(c.AttackRow, "Mouse1");
            
            Helpers.StartDrag(c.FirstCardInHand, new Position2d(0, 0));
            Helpers.EndDrag(c.FirstCardInHand, new Position2d(20, 20));
            AssertCardInRow(c.DefenseRow, "Mouse2");
            
            Helpers.StartDrag(c.AttackRow.Card, new Position2d(10, 10));
            Helpers.EndDrag(c.AttackRow.Card, new Position2d(20, 20));
            AssertCardInRow(c.AttackRow, "Mouse2");
            AssertCardInRow(c.DefenseRow, "Mouse1");
        }
        
        void AssertCardInRow(RowVM row, string cardName)
        {
            Assert.True(row.HasCard);
            Asserts.AssertCreatureCardId(row.Model.Get().GetId(), cardName);
        }
        
        void AssertRowEmpty(RowVM row)
        {
            Assert.False(row.HasCard);
        }
        
        void AssertSelectedCard(Scenarios.InGameWindowContext c, string cardName)
        {
            Assert.True(c.SelectedCard.IsPresent());
            Assert.True(c.SelectedCard.Get().Selected.Model);
            Asserts.AssertCreatureCardId(c.SelectedCard.Get().Id, cardName);
        }
        
        void AssertNoCardSelected(Scenarios.InGameWindowContext c)
        {
            Assert.False(c.SelectedCard.IsPresent());
        }
    }
}
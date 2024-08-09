using System.Collections.Generic;
using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Windows.Impl;
using B20.Logic.Utils;
using B20.Tests.Frontend.Windows.Fixtures;
using GameComponents;
using GameComponents.Api;
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
            var c = scenarios.InGameWindow();
            //Playing first card on attack row
            var card1Name = c.FirstCardInHand.Name.Model;
            Assert.Equal("Mouse1", card1Name);
            
            c.FirstCardInHand.Click();
            c.AttackRow.Click();
            
            AssertCardInRow(c.AttackRow, "Mouse1");
            
            Assert.Equal(c.CardsInHand.Count, 1);
            Assert.Equal(c.FirstCardInHand.Name.Model, "Mouse2");
            
            //Playing second card on defense row
            c.FirstCardInHand.Click();
            c.DefenseRow.Click();

            AssertCardInRow(c.DefenseRow, "Mouse2");
        }
        
        [Fact]
        public void ShouldMovePlayedCardBetweenRows()
        {
            var c = scenarios.InGameWindow();
            
            c.FirstCardInHand.Click();
            c.AttackRow.Click();
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertNoCardSelected(c);
            
            c.AttackRow.Card.Click();
            c.DefenseRow.Click();
            AssertCardInRow(c.DefenseRow, "Mouse1");
            AssertRowEmpty(c.AttackRow);
            AssertNoCardSelected(c);
            
            c.DefenseRow.Card.Click();
            c.AttackRow.Click();
            AssertCardInRow(c.AttackRow, "Mouse1");
            AssertRowEmpty(c.DefenseRow);
        }
        
        [Fact]
        public void ShouldSwapPlayedCards()
        {
            var c = scenarios.InGameWindow();
            
            c.FirstCardInHand.Click();
            c.AttackRow.Click();
            AssertCardInRow(c.AttackRow, "Mouse1");
            
            c.FirstCardInHand.Click();
            c.DefenseRow.Click();
            AssertCardInRow(c.DefenseRow, "Mouse2");
            
            c.AttackRow.Card.Click();
            c.DefenseRow.Card.Click();
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
            Asserts.AssertCreatureCardId(c.SelectedCard.Get(), cardName);
        }
        
        void AssertNoCardSelected(Scenarios.InGameWindowContext c)
        {
            Assert.False(c.SelectedCard.IsPresent());
        }
    }
}
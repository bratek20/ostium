using B20.Events.Api;
using B20.Events.Impl;
using B20.Frontend.Windows.Impl;
using B20.Logic.Utils;
using B20.Tests.Frontend.Windows.Fixtures;
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
            var c = scenarios.InGameWindow();
            var gameWindow = c.WindowManager.Get(WindowIds.GAME_WINDOW) as GameWindow;

            var card1Name = gameWindow.Game.Hand.Cards.Model[0].Name.Model;
            Assert.Equal("Mouse1", card1Name);
            
            gameWindow.Game.Hand.Cards.Model[0].Click();
            gameWindow.Game.Table.AttackRow.Click();
            
            Assert.NotNull(gameWindow.Game.Table.AttackRow.Model);
            Asserts.AssertCreatureCardId(gameWindow.Game.Table.AttackRow.Model.GetId(), "Mouse1");
            
            Assert.Equal(gameWindow.Game.Hand.Cards.Model.Count, 1);
            Assert.Equal(gameWindow.Game.Hand.Cards.Model[0].Name.Model, "Mouse2");
            
            gameWindow.Game.Hand.Cards.Model[0].Click();
            gameWindow.Game.Table.DefenseRow.Click();
            
            Assert.NotNull(gameWindow.Game.Table.DefenseRow.Model);
            Asserts.AssertCreatureCardId(gameWindow.Game.Table.DefenseRow.Model.GetId(), "Mouse2");
        }
    }
}
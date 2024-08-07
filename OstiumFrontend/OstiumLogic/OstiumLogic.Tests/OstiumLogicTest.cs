using B20.Events.Api;
using B20.Events.Impl;
using B20.Frontend.Windows.Impl;
using B20.Logic.Utils;
using B20.Tests.Frontend.Windows.Fixtures;
using GameComponents;
using Xunit;
using Xunit.Sdk;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicTest
    {
        [Fact]
        public void ShouldStartOnMainWindow()
        {
            var eventPublisher = new EventPublisherLogic();
            var windowManager = new WindowManagerLogic(new WindowManipulatorMock());
            
            var logic = new OstiumLogic(eventPublisher, windowManager);
            logic.RegisterWindows();
            
            //should not throw
            windowManager.Get(WindowIds.MAIN_WINDOW);
            windowManager.Get(WindowIds.GAME_WINDOW);
            
            logic.Start();
            
            Assert.Equal(WindowIds.MAIN_WINDOW, windowManager.GetCurrent());
            
            //Clicking play button
            (windowManager.Get(WindowIds.MAIN_WINDOW) as MainWindow).PlayButton.Click();
            
            Assert.Equal(WindowIds.GAME_WINDOW, windowManager.GetCurrent());
            
            var gameWindow = windowManager.Get(WindowIds.GAME_WINDOW) as GameWindow;

            var card1Name = gameWindow.Game.Hand.Card1.Name.Model;
            Assert.Equal("Mouse1", card1Name);
            
            gameWindow.Game.Hand.Card1.Click();
            gameWindow.Game.Table.AttackRow.Click();
            
            Assert.NotNull(gameWindow.Game.Table.AttackRow.Model);
            Asserts.AssertCreatureCardId(gameWindow.Game.Table.AttackRow.Model.GetId(), "Mouse1");
        }
    }
}
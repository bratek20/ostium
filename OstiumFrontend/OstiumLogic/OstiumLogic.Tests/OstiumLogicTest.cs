using B20.Events.Api;
using B20.Events.Impl;
using B20.Frontend.Windows.Impl;
using B20.Logic.Utils;
using B20.Tests.Frontend.Windows.Fixtures;
using Xunit;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicTest
    {
        [Fact]
        public void ShouldStartOnMainWindow()
        {
            var eventPublisher = new EventPublisherLogic(ListUtils.Of<EventListener>());
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
        }
    }
}
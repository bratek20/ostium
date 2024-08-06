using B20.Frontend.Windows.Impl;
using B20.Tests.Frontend.Windows.Fixtures;
using Xunit;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicTest
    {
        [Fact]
        public void ShouldStartOnMainWindow()
        {
            var windowManager = new WindowManagerLogic(new WindowManipulatorMock());
            
            var logic = new OstiumLogic(windowManager);
            logic.RegisterWindows();
            
            //should not throw
            windowManager.Get(WindowIds.MAIN_WINDOW);
            windowManager.Get(WindowIds.GAME_WINDOW);
            
            logic.Start();
            
            Assert.Equal(WindowIds.MAIN_WINDOW, windowManager.GetCurrent());
        }
    }
}
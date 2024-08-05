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
            
            Assert.Equal(WindowIds.MAIN_WINDOW, windowManager.GetCurrent());
        }
    }
}
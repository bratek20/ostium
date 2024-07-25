using B20.Logic;
using Xunit;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicTest: GameStateListener
    {
        [Fact]
        public void ShouldStartOnMainWindow()
        {
            var logic = new OstiumLogic(this);
            
            Assert.True(logic.State.CurrentWindow is MainWindow);
        }

        public void OnStateChanged()
        {
            
        }
    }
}
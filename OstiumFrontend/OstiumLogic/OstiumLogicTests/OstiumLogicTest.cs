using B20.Logic;
using Ostium.Logic;
using Xunit;

namespace OstiumLogicTests
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
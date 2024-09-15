using Main.ViewModel;
using SingleGame.ViewModel;
using Xunit;

namespace Ostium.Logic.Tests.Main.Tests
{
    public class MainViewModelTest
    {
        private Scenarios scenarios = new Scenarios();
        
        [Fact]
        public void ShouldStartOnMainScreenAndSwitchToGameScreen()
        {
            var c = scenarios.Setup();

            //should not throw
            c.WindowManager.Get<MainWindow>();
            c.WindowManager.Get<GameWindow>();
            
            c.Logic.Start();

            Assert.IsType<MainWindow>(c.WindowManager.GetCurrent());
            
            //Clicking play button
            c.WindowManager.Get<MainWindow>().PlayButton.Click();

            Assert.IsType<GameWindow>(c.WindowManager.GetCurrent());
        }

    }
}
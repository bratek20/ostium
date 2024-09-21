using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Logs.Context;
using B20.Frontend.Windows.Api;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.Windows.Fixtures;
using GamesManagement.ViewModel;
using Main.ViewModel;
using SingleGame.ViewModel;
using Xunit;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicSmokeTest
    {
        [Fact(
            Skip = "Comment this line to test real server interaction"
        )]
        public void GreenPathTest()
        {
            var c = CreateContext(ServerType.Cloud);
            
            var windowManager = c.Get<WindowManager>();
            var logic = c.Get<OstiumLogic>();
            logic.Start();
            
            var mainWindow = windowManager.GetCurrent() as MainWindow;
            mainWindow.Username.OnTextChange("SomeUser");
            mainWindow.PlayButton.Click();
            
            var gamesManagementWindow = windowManager.GetCurrent() as GamesManagementWindow;
            gamesManagementWindow.CreateGame.Click();
            
            var gameWindow = windowManager.GetCurrent() as GameWindow;
            
            AssertExt.ListCount(gameWindow.Game.MyHand.Cards.Model, 2);
        }

        private Context CreateContext(ServerType serverType)
        {
            return ContextsFactory.CreateBuilder()
                .WithModules(
                    //NEED TO BE SET BY UNITY
                    new WindowManipulatorInMemoryImpl(),
                    new ConsoleLogsImpl(),
                    
                    //EXPOSED BY LOGIC
                    new OstiumLogicFullImpl(serverType)
                )
                .Build();
        }
    }
}
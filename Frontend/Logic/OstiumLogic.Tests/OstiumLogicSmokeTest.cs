using System;
using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Logs.Context;
using B20.Frontend.Windows.Api;
using B20.Tests.Architecture.Logs.Context;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.Windows.Fixtures;
using GamesManagement.ViewModel;
using Main.ViewModel;
using SingleGame.ViewModel;
using Xunit;
using Xunit.Abstractions;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicSmokeTest
    {
        private readonly ITestOutputHelper _output;
        
        public OstiumLogicSmokeTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact(
            //Skip = "Comment this line to test real server interaction"
        )]
        public void CloudGreenPathTest()
        {
            GreenPathTest(CreateContext(ServerType.Cloud));
        }
        
        [Fact(
            Skip = "Comment this line to test real server interaction"
        )]
        public void LocalGreenPathTest()
        {
            GreenPathTest(CreateContext(ServerType.Local));
        }
        
        void GreenPathTest(Context c)
        {
            var windowManager = c.Get<WindowManager>();
            var logic = c.Get<OstiumLogic>();
            logic.Start();
            
            var mainWindow = windowManager.GetCurrent() as MainWindow;
            mainWindow.Username.OnTextChange("SomeUser");
            mainWindow.PlayButton.Click();
            
            var gamesManagementWindow = windowManager.GetCurrent() as GamesManagementWindow;
            gamesManagementWindow.CreateGame.Click();
            
            var gameWindow = windowManager.GetCurrent() as GameWindow;
            
            AssertExt.ListCount(gameWindow.GameState.MyHand.Cards.Model, 2);
        }

        private Context CreateContext(ServerType serverType)
        {
            return ContextsFactory.CreateBuilder()
                .WithModules(
                    //NEED TO BE SET BY UNITY
                    new WindowManipulatorInMemoryImpl(),
                    new XunitLogsImpl(_output),
                    
                    //EXPOSED BY LOGIC
                    new OstiumLogicFullImpl(serverType)
                )
                .Build();
        }
    }
}
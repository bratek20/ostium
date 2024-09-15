using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Events.Context;
using B20.Architecture.Logs.Context;
using B20.Ext;
using B20.Frontend.Traits.Context;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Context;
using B20.Infrastructure.HttpClientModule.Context;
using B20.Tests.Frontend.Windows.Fixtures;
using SingleGame;
using SingleGame.Api;
using HttpClientModule.Api;
using Main.ViewModel;
using Ostium.Logic.GameModule.Context;
using Ostium.Logic.Tests.GameModule.Context;
using Xunit;
using Diffs = GameComponents.Diffs;

namespace Ostium.Logic.Tests
{
    public class OstiumLogicSmokeTest
    {
        private SingleGameApi CreateWebApi()
        {
            return ContextsFactory.CreateBuilder()
                .WithModules(
                    new DotNetHttpClientModuleImpl(),
                    new GameModuleWebClient(
                        HttpClientConfig.Create(
                            baseUrl: "http://localhost:8080",
                            auth: Optional<HttpClientAuth>.Empty()
                        )
                    )
                )
                .Get<SingleGameApi>();
        }
        
        [Fact(
            Skip = "Comment this line to test local server connection"
        )]
        public void ShouldUseLocalServer()
        {
            var api = CreateWebApi();
            var game = api.StartGame();

            Asserts.AssertGame(game, expected =>
            {
                expected.Hand = hand =>
                {
                    hand.Cards = new List<Action<Diffs.ExpectedCreatureCard>>
                    {
                        card => { card.Id = "Mouse1"; },
                        card => { card.Id = "Mouse2"; }
                    };
                };
            });
        }
        
        public class OstiumMockedBackendImpl: ContextModule
        {
            public void Apply(ContextBuilder builder)
            {
                builder
                    .WithModules(
                        new GameModuleMocks()
                    );
            }
        }
        
        [Fact]
        public void GreenPathFlow()
        {
            var b = ContextsFactory.CreateBuilder()
                .WithModules(
                    //NEED TO BE SET BY UNITY
                    new WindowManipulatorInMemoryImpl(),
                    new ConsoleLogsImpl(),
                    
                    //EXPOSED BY LOGIC
                    new OstiumLogicNoBackendImpl(),
                    new OstiumMockedBackendImpl()
                )
                .Build();
            
            var logic = b.Get<OstiumLogic>();
            var windowManager = b.Get<WindowManager>();

            logic.Start();
            
            var mainWindow = windowManager.Get<MainWindow>();
            mainWindow.PlayButton.Click();
        }
    }
}
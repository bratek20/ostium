using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
using B20.Ext;
using B20.Infrastructure.HttpClientModule.Context;
using GameModule.Api;
using GameModule;
using HttpClientModule.Api;
using Ostium.Logic;
using Ostium.Logic.GameModule.Context;
using Xunit;
using Diffs = GameComponents.Diffs;

namespace OstiumBackend.Tests.GameSetup.Tests
{
    public class SmokeTest
    {
        private GameApi CreateWebApi()
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
                .Get<GameApi>();
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
    }
}
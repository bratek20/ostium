using System;
using System.Collections.Generic;
using B20.Ext;
using B20.Infrastructure.HttpClient.Integrations;
using GameSetup;
using GameSetup.Api;
using GameSetup.Web;
using HttpClientModule.Api;
using HttpClientModule.Impl;
using Xunit;

namespace OstiumBackend.Tests.GameSetup.Tests
{
    public class SmokeTest
    {
        private GameSetupApi CreateWebApi()
        {
            var factory = new HttpClientFactoryLogic(new DotNetHttpRequester());
            return new GameSetupApiWebClient(
                factory,
                new GameSetupWebClientConfig(
                    HttpClientConfig.Create(
                        baseUrl: "http://localhost:8080",
                        auth: Optional<HttpClientAuth>.Empty()
                    )
                )
            );
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
                    hand.Cards = new List<Action<GameComponents.Diffs.ExpectedCreatureCard>>
                    {
                        card => { card.Id = "Mouse1"; },
                        card => { card.Id = "Mouse2"; }
                    };
                };
            });
        }
    }
}
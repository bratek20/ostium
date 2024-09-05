using System;
using System.Collections.Generic;
using GameModule.Api;
using GameModule;
using Ostium.Logic;
using Xunit;
using Diffs = GameComponents.Diffs;

namespace OstiumBackend.Tests.GameSetup.Tests
{
    public class SmokeTest
    {
        private GameApi CreateWebApi()
        {
            return OstiumLogicFactory.CreateWebClientForGameApi();
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
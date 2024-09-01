using System;
using System.Collections.Generic;
using B20.Ext;
using B20.Infrastructure.HttpClient.Integrations;
using GameComponents.Api;
using GameSetup;
using GameSetup.Api;
using GameSetup.Impl;
using GameSetup.Web;
using HttpClientModule.Api;
using HttpClientModule.Impl;
using Xunit;

namespace OstiumBackend.Tests.GameSetup.Tests
{
    public class GameSetupImplTest
    {
        private GameSetupApi CreateApi()
        {
            // Assuming someContextBuilder() is a placeholder for the actual implementation
            // that provides necessary context and module setup for GameSetupApi.
            return new GameSetupApiLogic();
        }

        [Fact]
        public void StartGame_ShouldInitializeCorrectly()
        {
            var api = CreateApi();
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

                expected.Table = table =>
                {
                    table.GateDurabilityCard = card =>
                    {
                        card.MyMarker = 15;
                        card.OpponentMarker = 15;
                    };
                    table.AttackRowEmpty = true;
                    table.DefenseRowEmpty = true;
                    table.GateCard = gate =>
                    {
                        gate.Destroyed = false;
                    };
                };
            });
        }
        


        [Fact]
        public void PlayCard_ShouldPlaceCardInAttackRow()
        {
            var api = CreateApi();
            api.StartGame();

            var game = api.PlayCard(new CreatureCardId("Mouse1"), RowType.ATTACK);

            Asserts.AssertGame(game, expected =>
            {
                expected.Hand = hand =>
                {
                    hand.Cards = new List<Action<GameComponents.Diffs.ExpectedCreatureCard>>
                    {
                        card => { card.Id = "Mouse2"; }
                    };
                };

                expected.Table = table =>
                {
                    table.AttackRow = row =>
                    {
                        row.Id = "Mouse1";
                    };
                };
            });
        }

        [Fact]
        public void MoveCard_ShouldMoveCardToDefenseRow()
        {
            var api = CreateApi();
            api.StartGame();
            api.PlayCard(new CreatureCardId("Mouse1"), RowType.ATTACK);

            var game = api.MoveCard(new CreatureCardId("Mouse1"), RowType.ATTACK, RowType.DEFENSE);

            Asserts.AssertGame(game, expected =>
            {
                expected.Hand = hand =>
                {
                    hand.Cards = new List<Action<GameComponents.Diffs.ExpectedCreatureCard>>
                    {
                        card => { card.Id = "Mouse2"; }
                    };
                };

                expected.Table = table =>
                {
                    table.AttackRowEmpty = true;
                    table.DefenseRow = row =>
                    {
                        row.Id = "Mouse1";
                    };
                };
            });
        }
        
        [Fact]
        public void MoveCard_ShouldSwapCards()
        {
            var api = CreateApi();
            api.StartGame();
            api.PlayCard(new CreatureCardId("Mouse1"), RowType.ATTACK);
            api.PlayCard(new CreatureCardId("Mouse2"), RowType.DEFENSE);

            var game = api.MoveCard(new CreatureCardId("Mouse1"), RowType.ATTACK, RowType.DEFENSE);

            Asserts.AssertGame(game, expected =>
            {
                expected.Table = table =>
                {
                    table.AttackRow = row =>
                    {
                        row.Id = "Mouse2";
                    };
                    table.DefenseRow = row =>
                    {
                        row.Id = "Mouse1";
                    };
                };
            });
        }
    }
}

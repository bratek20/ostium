// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using GameComponents.Api;
using GameSetup.Api;

namespace GameSetup
{
    public static class Builders
    {
        public class TableDef
        {
            public Action<GameComponents.Builders.GateDurabilityCardDef> GateDurabilityCard { get; set; } = _ => { };
            public Action<GameComponents.Builders.CreatureCardDef> AttackRow { get; set; }
            public Action<GameComponents.Builders.CreatureCardDef> DefenseRow { get; set; }
            public Action<GameComponents.Builders.GateCardDef> GateCard { get; set; } = _ => { };
        }

        public static Table BuildTable(Action<TableDef> init = null)
        {
            var def = new TableDef();
            init?.Invoke(def);
            return Table.Create(
                gateDurabilityCard: BuildGateDurabilityCard(def.GateDurabilityCard),
                attackRow: def.AttackRow != null ? BuildCreatureCard(def.AttackRow) : null,
                defenseRow: def.DefenseRow != null ? BuildCreatureCard(def.DefenseRow) : null,
                gateCard: BuildGateCard(def.GateCard)
            );
        }

        public class HandDef
        {
            public List<Action<GameComponents.Builders.CreatureCardDef>> Cards { get; set; } = new List<Action<GameComponents.Builders.CreatureCardDef>>();
        }

        public static Hand BuildHand(Action<HandDef> init = null)
        {
            var def = new HandDef();
            init?.Invoke(def);
            return Hand.Create(
                cards: def.Cards.ConvertAll(BuildCreatureCard)
            );
        }

        public class GameDef
        {
            public Action<TableDef> Table { get; set; } = _ => { };
            public Action<HandDef> Hand { get; set; } = _ => { };
        }

        public static Game BuildGame(Action<GameDef> init = null)
        {
            var def = new GameDef();
            init?.Invoke(def);
            return Game.Create(
                table: BuildTable(def.Table),
                hand: BuildHand(def.Hand)
            );
        }

        private static GateDurabilityCard BuildGateDurabilityCard(Action<GameComponents.Builders.GateDurabilityCardDef> init)
        {
            // Implementation of BuildGateDurabilityCard goes here.
            // Assuming a similar pattern to the other Build methods.
            var def = new GameComponents.Builders.GateDurabilityCardDef();
            init?.Invoke(def);
            return GateDurabilityCard.Create(
                new GateDurabilityMarker(def.MyMarker), 
                new GateDurabilityMarker(def.OpponentMarker)); // Adjust as per actual creation logic
        }

        private static CreatureCard BuildCreatureCard(Action<GameComponents.Builders.CreatureCardDef> init)
        {
            var def = new GameComponents.Builders.CreatureCardDef();
            init?.Invoke(def);
            return CreatureCard.Create(new CreatureCardId(def.Id));
        }

        private static GateCard BuildGateCard(Action<GameComponents.Builders.GateCardDef> init)
        {
            var def = new GameComponents.Builders.GateCardDef();
            init?.Invoke(def);
            return GateCard.Create(def.Destroyed);
        }
    }
}
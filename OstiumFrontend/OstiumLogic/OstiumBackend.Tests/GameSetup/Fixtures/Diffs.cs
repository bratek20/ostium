using System;
using System.Collections.Generic;
using GameComponents.Api;
using GameSetup.Api;

namespace GameSetup
{
    public static class Diffs
    {
        public static string DiffRowType(RowType given, string expected, string path = "")
        {
            return given != (RowType) Enum.Parse(typeof(RowType), expected) ? $"{path}value {given} != {expected}" : string.Empty;
        }

        public class ExpectedTable
        {
            public Action<GameComponents.Diffs.ExpectedGateDurabilityCard> GateDurabilityCard { get; set; }
            public bool? AttackRowEmpty { get; set; }
            public Action<GameComponents.Diffs.ExpectedCreatureCard> AttackRow { get; set; }
            public bool? DefenseRowEmpty { get; set; }
            public Action<GameComponents.Diffs.ExpectedCreatureCard> DefenseRow { get; set; }
            public Action<GameComponents.Diffs.ExpectedGateCard> GateCard { get; set; }
        }

        public static string DiffTable(Table given, Action<ExpectedTable> expectedInit, string path = "")
        {
            var expected = new ExpectedTable();
            expectedInit?.Invoke(expected);
            var result = new List<string>();

            if (expected.GateDurabilityCard != null)
            {
                var diff = GameComponents.Diffs.DiffGateDurabilityCard(given.GetGateDurabilityCard(), expected.GateDurabilityCard, $"{path}gateDurabilityCard.");
                if (!string.IsNullOrEmpty(diff)) result.Add(diff);
            }

            if (expected.AttackRowEmpty.HasValue && (given.GetAttackRow() == null) != expected.AttackRowEmpty.Value)
            {
                result.Add($"{path}attackRow empty {(given.GetAttackRow() == null)} != {expected.AttackRowEmpty.Value}");
            }

            if (expected.AttackRow != null)
            {
                var diff = GameComponents.Diffs.DiffCreatureCard(given.GetAttackRow(), expected.AttackRow, $"{path}attackRow.");
                if (!string.IsNullOrEmpty(diff)) result.Add(diff);
            }

            if (expected.DefenseRowEmpty.HasValue && (given.GetDefenseRow() == null) != expected.DefenseRowEmpty.Value)
            {
                result.Add($"{path}defenseRow empty {(given.GetDefenseRow() == null)} != {expected.DefenseRowEmpty.Value}");
            }

            if (expected.DefenseRow != null)
            {
                var diff = GameComponents.Diffs.DiffCreatureCard(given.GetDefenseRow(), expected.DefenseRow, $"{path}defenseRow.");
                if (!string.IsNullOrEmpty(diff)) result.Add(diff);
            }

            if (expected.GateCard != null)
            {
                var diff = GameComponents.Diffs.DiffGateCard(given.GetGateCard(), expected.GateCard, $"{path}gateCard.");
                if (!string.IsNullOrEmpty(diff)) result.Add(diff);
            }

            return string.Join("\n", result);
        }

        public class ExpectedHand
        {
            public List<Action<GameComponents.Diffs.ExpectedCreatureCard>> Cards { get; set; }
        }

        public static string DiffHand(Hand given, Action<ExpectedHand> expectedInit, string path = "")
        {
            var expected = new ExpectedHand();
            expectedInit?.Invoke(expected);
            var result = new List<string>();

            if (expected.Cards != null)
            {
                if (given.GetCards().Count != expected.Cards.Count)
                {
                    result.Add($"{path}cards size {given.GetCards().Count} != {expected.Cards.Count}");
                }
                else
                {
                    for (int idx = 0; idx < given.GetCards().Count; idx++)
                    {
                        var diff = GameComponents.Diffs.DiffCreatureCard(given.GetCards()[idx], expected.Cards[idx], $"{path}cards[{idx}].");
                        if (!string.IsNullOrEmpty(diff)) result.Add(diff);
                    }
                }
            }

            return string.Join("\n", result);
        }

        public class ExpectedGame
        {
            public Action<ExpectedTable> Table { get; set; }
            public Action<ExpectedHand> Hand { get; set; }
        }

        public static string DiffGame(Game given, Action<ExpectedGame> expectedInit, string path = "")
        {
            var expected = new ExpectedGame();
            expectedInit?.Invoke(expected);
            var result = new List<string>();

            if (expected.Table != null)
            {
                var diff = DiffTable(given.GetTable(), expected.Table, $"{path}table.");
                if (!string.IsNullOrEmpty(diff)) result.Add(diff);
            }

            if (expected.Hand != null)
            {
                var diff = DiffHand(given.GetHand(), expected.Hand, $"{path}hand.");
                if (!string.IsNullOrEmpty(diff)) result.Add(diff);
            }

            return string.Join("\n", result);
        }
    }
}

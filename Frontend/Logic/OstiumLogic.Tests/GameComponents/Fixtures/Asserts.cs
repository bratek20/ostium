// DO NOT EDIT! Autogenerated by HLA tool

using System;
using GameModule.Api;
using Xunit;

namespace GameComponents
{
    public static class Asserts
    {
        public static void AssertGateDurabilityMarker(GateDurabilityMarker given, int expected)
        {
            var diff = Diffs.DiffGateDurabilityMarker(given, expected);
            Assert.True(diff == "", diff);
        }

        public static void AssertCreatureCardId(CreatureCardId given, string expected)
        {
            var diff = Diffs.DiffCreatureCardId(given, expected);
            Assert.True(diff == "", diff);
        }

        public static void AssertCreatureCard(CreatureCard given, Action<Diffs.ExpectedCreatureCard> expectedInit)
        {
            var diff = Diffs.DiffCreatureCard(given, expectedInit);
            Assert.True(diff == "", diff);
        }

        public static void AssertGateCard(GateCard given, Action<Diffs.ExpectedGateCard> expectedInit)
        {
            var diff = Diffs.DiffGateCard(given, expectedInit);
            Assert.True(diff == "", diff);
        }

        public static void AssertGateDurabilityCard(GateDurabilityCard given, Action<Diffs.ExpectedGateDurabilityCard> expectedInit)
        {
            var diff = Diffs.DiffGateDurabilityCard(given, expectedInit);
            Assert.True(diff == "", diff);
        }
    }
}
// DO NOT EDIT! Autogenerated by HLA tool

using System;
using GameComponents;
using GameModule.Api;
using Xunit;

namespace GameModule
{
    public static class Asserts
    {
        public static void AssertTable(Table given, Action<Diffs.ExpectedTable> expectedInit)
        {
            var diff = Diffs.DiffTable(given, expectedInit);
            Assert.True(diff == "", diff);
        }

        public static void AssertHand(Hand given, Action<Diffs.ExpectedHand> expectedInit)
        {
            var diff = Diffs.DiffHand(given, expectedInit);
            Assert.True(diff == "", diff);
        }

        public static void AssertGame(GameModule.Api.Game given, Action<Diffs.ExpectedGame> expectedInit)
        {
            var diff = Diffs.DiffGame(given, expectedInit);
            Assert.True(diff == "", diff);
        }
    }
}
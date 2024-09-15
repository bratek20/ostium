using System;
using SingleGame.Api;

namespace SingleGame
{
    public static class Diffs
    {
        public static string DiffRowType(RowType given, string expected, string path = "")
        {
            return given != (RowType) Enum.Parse(typeof(RowType), expected) ? $"{path}value {given} != {expected}" : string.Empty;
        }
        
        public static string DiffCreatureCardId(CreatureCardId given, string expected, string path = "")
        {
            return given.Value != expected ? $"{path}value {given.Value} != {expected}" : string.Empty;
        }
    }
}

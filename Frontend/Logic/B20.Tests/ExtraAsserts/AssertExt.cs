using System.Collections.Generic;
using System.Linq;

namespace B20.Tests.ExtraAsserts
{
    public class AssertExt
    {
        public static void Equal<T>(T actual, T expected)
        {
            Xunit.Assert.Equal(expected, actual);
        }
        public static void ListCount<T>(List<T> list, int expected)
        {
            Equal(list.Count, expected);
        }
        
        public static void EnumerableCount<T>(IEnumerable<T> enumerable, int expected)
        {
            Equal(enumerable.Count(), expected);
        }
    }
}
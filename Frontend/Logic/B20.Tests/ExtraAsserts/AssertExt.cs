namespace B20.Tests.ExtraAsserts
{
    public class AssertExt
    {
        public static void Equal<T>(T actual, T expected)
        {
            Xunit.Assert.Equal(expected, actual);
        }
        public static void ListCount<T>(System.Collections.Generic.List<T> list, int expected)
        {
            Xunit.Assert.Equal(expected, list.Count);
        }
    }
}
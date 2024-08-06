using System;

namespace B20.Tests.Architecture.Exceptions.Fixtures
{
    public class Assert
    {
        public class ExpectedException
        {
            public Type Type { get; set; }
            public string Message { get; set; }
            public string MessagePrefix { get; set; }
        }
        public static void ThrowsApiException(Action action, Action<ExpectedException> init)
        {
            var expected = new ExpectedException();
            init(expected);

            Exception thrownException = Xunit.Assert.ThrowsAny<Exception>(action);

            if (expected.Type != null)
            {
                Xunit.Assert.IsType(expected.Type, thrownException);
            }
            if (expected.Message != null)
            {
                Xunit.Assert.Equal(expected.Message, thrownException.Message);
            }
            if (expected.MessagePrefix != null)
            {
                Xunit.Assert.StartsWith(expected.MessagePrefix, thrownException.Message);
            }
        }
    }
}
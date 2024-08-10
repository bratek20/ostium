using B20.Frontend.Postion;
using Xunit;

namespace B20.Tests.Frontend.Types
{
    public class Position2DTest
    {
        [Fact]
        public void ToStringTest()
        {
            var position = new Position2D(5, 10);
            Assert.Equal("(5, 10)", position.ToString());
        }
    }
}
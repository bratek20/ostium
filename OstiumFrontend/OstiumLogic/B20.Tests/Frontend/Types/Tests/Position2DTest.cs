using B20.Frontend.Postion;
using Xunit;

namespace B20.Tests.Frontend.Types
{
    public class Position2DTest
    {
        [Fact]
        public void ToStringTest()
        {
            var position = new Position2d(5, 10);
            Assert.Equal("(5, 10)", position.ToString());
        }
        
        [Fact]
        public void EqualsTest()
        {
            var position1 = new Position2d(5, 10);
            var position2 = new Position2d(5, 10);
            Assert.Equal(position1, position2);
        }
    }
}
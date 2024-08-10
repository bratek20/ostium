using B20.Frontend.Postion;
using Xunit;

namespace B20.Tests.Frontend.Types
{
    public class RectTest
    {
        [Fact]
        public void ToStringTest()
        {
            var rect = new Rect(5, 10, 15, 20);
            Assert.Equal("(lbx: 5, lby: 10, w: 15, h: 20)", rect.ToString());
        }
        
        [Fact]
        public void IsInside()
        {
            var rect = new Rect(100, 200, 30, 40);
            
            //edge points are inside
            Assert.True(rect.IsInside(new Position2D(100, 200)));
            Assert.True(rect.IsInside(new Position2D(130, 200)));
            Assert.True(rect.IsInside(new Position2D(100, 240)));
            Assert.True(rect.IsInside(new Position2D(130, 240)));
            
            //points outside
            Assert.False(rect.IsInside(new Position2D(99, 200)));
            Assert.False(rect.IsInside(new Position2D(100, 199)));
            Assert.False(rect.IsInside(new Position2D(131, 200)));
            Assert.False(rect.IsInside(new Position2D(100, 241)));
        }
    }
}
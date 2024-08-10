using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Tests.Architecture.Exceptions.Fixtures;
using B20.Tests.Frontend.Types.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class WithRectTest
    {
        [Fact]
        public void IsInside_ShouldThrowIfRectProviderNotSet()
        {
            var withRect = new WithRect();
            Asserts.ThrowsApiException(
                () => withRect.IsInside(Builders.CreatePosition2D()),
                e => e.Type = typeof(RectProviderNotSetException)
            );
        }

        [Fact]
        public void IsInside_SupportsChangeableRect()
        {
            var withRect = new WithRect();
            var rect = new Rect(0, 0, 10, 10);
            withRect.RectProvider = () => rect;
            
            Assert.True(withRect.IsInside(new Position2D(5, 5)));
            
            rect = new Rect(10, 10, 10, 10);
            Assert.False(withRect.IsInside(new Position2D(5, 5)));
        }
    }
}
using B20.Architecture.Exceptions.Fixtures;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Tests.Frontend.Traits.Fixtures;
using B20.Tests.Frontend.Types.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class WithRectTest
    {
        private WithRect withRect;
        
        public WithRectTest()
        {
            withRect = TraitsScenarios.Setup().TraitFactory.Create<WithRect>();
        }
        
        [Fact]
        public void IsInside_ShouldThrowIfRectProviderNotSet()
        {
            ExceptionsAsserts.ThrowsApiException(
                () => withRect.IsInside(Builders.CreatePosition2D()),
                e => e.Type = typeof(RectProviderNotSetException)
            );
        }

        [Fact]
        public void IsInside_SupportsChangeableRect()
        {
            var rect = new Rect(0, 0, 10, 10);
            withRect.SetRectProvider(() => rect);
            
            Assert.True(withRect.IsInside(new Position2d(5, 5)));
            
            rect = new Rect(10, 10, 10, 10);
            Assert.False(withRect.IsInside(new Position2d(5, 5)));
        }
    }
}
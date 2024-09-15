using B20.Frontend.UiElements;
using B20.Frontend.Postion;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.Traits.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class WithPosition2dTest
    {
        [Fact]
        public void ShouldWork()
        {
            var trait = TraitsScenarios.Setup().TraitFactory.Create<WithPosition2d>();
            
            Position2d currentPosition = null;
            trait.SetPositionSetter(p => currentPosition = p);
            
            trait.Update(new Position2d(1, 2));

            AssertExt.Equal(trait.Model, new Position2d(1, 2));
            AssertExt.Equal(currentPosition, new Position2d(1, 2));
        }
    }
}
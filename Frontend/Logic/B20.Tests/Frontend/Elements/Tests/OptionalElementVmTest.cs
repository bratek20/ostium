using B20.Ext;
using B20.Frontend.UiElements;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class OptionalElementVmTest
    {
        [Fact]
        public void ShouldUpdateElement()
        {
            var element = new OptionalUiElement<SomeViewModel, SomeModel>(new SomeViewModel());
            
            element.Update(Optional<SomeModel>.Of(new SomeModel(1)));
            Assert.Equal(1, element.Element.Model.Value);
            
            element.Update(Optional<SomeModel>.Of(new SomeModel(2)));
            Assert.Equal(2, element.Element.Model.Value);
        }
    }
}
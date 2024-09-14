using B20.Frontend.UiElements;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class InputFieldTest
    {
        [Fact]
        public void ShouldWork()
        {
            var inputField = new InputField();
            
            inputField.Update("Some input");
            
            AssertExt.Equal(inputField.Model, "Some input");
        }
    }
}
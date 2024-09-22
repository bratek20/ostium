using B20.Architecture.Exceptions.Fixtures;
using B20.Frontend.UiElements;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class ButtonTest
    {
        private Button button;
        
        public ButtonTest()
        {
            button = new Button();
        }
        
        [Fact]
        public void ShouldWork()
        {
            // Arrange
            var button = new Button();
            var clicked = false;
            button.OnClick(() => clicked = true);
            
            // Act
            button.Click();
            
            // Assert
            Assert.True(clicked);
        }
        
        [Fact]
        public void ShouldThrowsIfOnClickNotSet()
        {
            ExceptionsAsserts.ThrowsApiException(
            () => button.Click(),
            e =>
            {
                e.Type = typeof(ButtonOnClickNotSetException);
                e.Message = "Button OnClick not set";
            }
            );
        }
    }
}
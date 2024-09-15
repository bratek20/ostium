using B20.Architecture.Contexts.Context;
using B20.Architecture.Logs.Fixtures;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements;
using B20.Tests.Architecture.Logs.Context;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class InputFieldTest
    {
        [Fact]
        public void ShouldWork()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new TraitsImpl(),
                    new LogsMocks()
                )
                .SetClass<InputField>()
                .Build();
            
            var loggerMock = c.Get<LoggerMock>();
            var inputField = c.Get<InputField>();
            
            inputField.OnChange("Some input");
            
            AssertExt.Equal(inputField.Model, "Some input");
            loggerMock.AssertInfos(
                "Input field changed to `Some input`"
            );
        }
    }
}
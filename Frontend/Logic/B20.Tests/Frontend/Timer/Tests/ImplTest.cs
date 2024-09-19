using B20.Architecture.Contexts.Context;
using B20.Frontend.Timer.Api;
using B20.Frontend.Timer.Context;
using B20.Frontend.Timer.Impl;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Frontend.Timer.Tests
{
    public class ImplTest
    {
        [Fact]
        public void TestProgress()
        {
            // Arrange
            var timerApi = ContextsFactory.CreateBuilder()
                .WithModule(new TimerImpl())
                .Get<TimerApi>();
            
            int updates = 0;
            timerApi.Schedule(() => updates++, 1);
            
            // Act
            timerApi.Progress(999);
            AssertExt.Equal(updates, 0);
            
            
            timerApi.Progress(1);
            AssertExt.Equal(updates, 1);
        }
    }
}
using B20.Architecture.Contexts.Context;
using B20.Architecture.Exceptions.Fixtures;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Context;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.Windows.Fixtures;
using Xunit;

namespace B20.Frontend.Windows.Tests
{
    public class WindowsImplTest
    {
        class NotRegisteredWindow : Window
        {
        }
        
        class Window1 : Window
        {
        }
        
        class Window2 : Window
        {
        }
        
        private InMemoryWindowManipulator manipulator;
        private WindowManager windowManager;

        public WindowsImplTest()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new WindowsImpl(),
                    new WindowManipulatorInMemoryImpl()
                )
                .AddImpl<Window, Window1>()
                .AddImpl<Window, Window2>()
                .Build();
            
            manipulator = c.Get<InMemoryWindowManipulator>();
            windowManager = c.Get<WindowManager>();
        }

        [Fact]
        public void ShouldThrowExceptionForGetIfWindowNotRegistered()
        {
            Asserts.ThrowsApiException(
                () => windowManager.Get<NotRegisteredWindow>(),
                e =>
                {
                    e.Type = typeof(WindowNotFoundException);
                    e.Message = "Window NotRegisteredWindow not found";
                }
            );
        }
        
        [Fact]
        public void ShouldHandleWindowLogic()
        {
            // Creation
            AssertVisible<Window1>(false);
            AssertVisible<Window2>(false);
            
            Assert.IsType<Window1>(windowManager.Get<Window1>());
            Assert.IsType<Window2>(windowManager.Get<Window2>());
            
            // Open first window
            windowManager.Open<Window1>();
            AssertVisible<Window1>(true);
            AssertVisible<Window2>(false);

            // Open second window
            windowManager.Open<Window2>();
            AssertVisible<Window1>(false);
            AssertVisible<Window2>(true);
        }
        
        private void AssertVisible<T>(bool visible) where T : Window
        {
            AssertExt.Equal(manipulator.GetVisible<T>(), visible);
        }
    }
}
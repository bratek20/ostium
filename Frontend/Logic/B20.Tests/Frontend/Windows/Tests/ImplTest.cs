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
        class NotRegisteredWindow : Window<EmptyWindowState>
        {
        }
        
        class Window1 : Window<EmptyWindowState>
        {
            private WindowManager windowManager;
            public Window1(WindowManager windowManager)
            {
                this.windowManager = windowManager;
            }
            
            public void OpenWindow2(int value)
            {
                windowManager.Open<Window2, Window2State>(new Window2State
                    {
                        Value = value
                    }
                );
            }
        }
        
        class Window2State
        {
            public int Value { get; set; }
        }
        
        class Window2 : Window<Window2State>
        {
            public int StateValue => State.Value;
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
            ExceptionsAsserts.ThrowsApiException(
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
            //Creation
            Assert.IsType<Window1>(windowManager.Get<Window1>());
            Assert.IsType<Window2>(windowManager.Get<Window2>());
            
            AssertVisible<Window1>(false);
            AssertVisible<Window2>(false);
            
            // Open first window
            windowManager.Open<Window1, EmptyWindowState>(new EmptyWindowState());
            AssertVisible<Window1>(true);
            AssertVisible<Window2>(false);
            Assert.IsType<Window1>(windowManager.GetCurrent());

            // Open second window from first window
            windowManager.Get<Window1>().OpenWindow2(42);
            AssertVisible<Window1>(false);
            AssertVisible<Window2>(true);
            Assert.IsType<Window2>(windowManager.GetCurrent());
            
            AssertExt.Equal(((Window2)windowManager.GetCurrent()).StateValue, 42);
        }
        
        private void AssertVisible<T>(bool visible) where T : Window
        {
            AssertExt.Equal(manipulator.GetVisible<T>(), visible);
        }
    }
}
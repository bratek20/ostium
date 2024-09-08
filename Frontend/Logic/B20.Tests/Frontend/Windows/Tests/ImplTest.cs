using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Exceptions;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Architecture.Exceptions.Fixtures;
using B20.Frontend.Windows.Context;
using B20.Tests.ExtraAsserts;
using B20.Tests.Frontend.Windows.Fixtures;
using Xunit;

namespace B20.Frontend.Windows.Tests
{
    public class WindowsImplTest
    {
        class TestWindow1 : Window
        {
            public WindowId GetId()
            {
                return new WindowId("window1");
            }
        }
        
        class TestWindow2 : Window
        {
            public WindowId GetId()
            {
                return new WindowId("window2");
            }
        }
        
        private WindowManipulatorMock manipulatorMock;
        private WindowManager windowManager;

        public WindowsImplTest()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new WindowsImpl(),
                    new WindowManipulatorMockImpl()
                )
                .AddImpl<Window, TestWindow1>()
                .AddImpl<Window, TestWindow2>()
                .Build();
            
            manipulatorMock = c.Get<WindowManipulatorMock>();
            windowManager = c.Get<WindowManager>();
        }

        [Fact]
        public void ShouldThrowExceptionForGetIfWindowNotRegistered()
        {
            Asserts.ThrowsApiException(
                () => windowManager.Get(new WindowId("notRegistered")),
                e =>
                {
                    e.Type = typeof(WindowNotFoundException);
                    e.Message = "Window notRegistered not found";
                }
            );
        }
        
        [Fact]
        public void ShouldHandleWindowLogic()
        {
            // Creation
            manipulatorMock.AssertNoSetVisibleCalls();
            Assert.IsType<TestWindow1>(windowManager.Get(new WindowId("window1")));
            Assert.IsType<TestWindow2>(windowManager.Get(new WindowId("window2")));
            
            // Open first window
            windowManager.Open(new WindowId("window1"));
            manipulatorMock.AssertVisible("window1", true);
            manipulatorMock.AssertVisible("window2", false);

            // Open second window
            windowManager.Open(new WindowId("window2"));
            manipulatorMock.AssertVisible("window1", false);
            manipulatorMock.AssertVisible("window2", true);
        }
    }
}
using System.Collections.Generic;
using B20.Architecture.Exceptions;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Architecture.Exceptions.Fixtures;
using B20.Tests.Frontend.Windows.Fixtures;
using Xunit;

namespace B20.Frontend.Windows.Tests
{
    public class WindowsImplTest
    {
        class TestWindow : Window
        {
            private WindowId id;
            public TestWindow(string id)
            {
                this.id = new WindowId(id);
            }
            
            public WindowId GetId()
            {
                return id;
            }

            public void OnOpen()
            {
                
            }
        }
        
        private WindowManipulatorMock manipulatorMock;
        private WindowManager windowManager;

        public WindowsImplTest()
        {
            manipulatorMock = new WindowManipulatorMock();
            windowManager = new WindowManagerLogic(manipulatorMock);
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
            var window1 = new TestWindow("window1");
            var window2 = new TestWindow("window2");
            
            windowManager.Register(window1);
            windowManager.Register(window2);
            
            manipulatorMock.AssertNoSetVisibleCalls();
            Assert.Equal(window1, windowManager.Get(new WindowId("window1")));
            Assert.Equal(window2, windowManager.Get(new WindowId("window2")));
            
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
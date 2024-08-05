using System.Collections.Generic;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
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
        }

        [Fact]
        public void ShouldHandleWindowLogic()
        {
            // Creation
            WindowManipulatorMock manipulatorMock = new WindowManipulatorMock();
            WindowManager windowManager = new WindowManagerLogic(manipulatorMock);
            
            windowManager.Register(new TestWindow("window1"));
            windowManager.Register(new TestWindow("window2"));
            
            // Init state
            manipulatorMock.AssertVisible("window1", false);
            manipulatorMock.AssertVisible("window2", false);
            
            // Open first window
            windowManager.Open(new WindowId("window1"));
            manipulatorMock.AssertVisible("window1", true);
            
            // Open second window
            windowManager.Open(new WindowId("window2"));
            manipulatorMock.AssertVisible("window1", false);
            manipulatorMock.AssertVisible("window2", true);
        }
    }
}
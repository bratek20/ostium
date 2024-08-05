using System.Collections.Generic;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using Xunit;

namespace B20.Frontend.Windows.Tests
{
    public class WindowManipulatorMock : WindowManipulator
    {
        private Dictionary<WindowId, bool> windowVisibility = new Dictionary<WindowId, bool>();

        public void SetVisible(WindowId id, bool visible)
        {
            windowVisibility[id] = visible;
        }

        public void AssertVisible(WindowId id, bool visible)
        {
            Assert.Equal(visible, windowVisibility[id]);
        }
    }

    public class WindowsImplTest
    {
        [Fact]
        public void ShouldHandleWindowLogic()
        {
            // Creation
            WindowManipulatorMock manipulatorMock = new WindowManipulatorMock();
            WindowManager windowManager = new WindowManagerLogic(manipulatorMock);
            
            WindowId window1 = new WindowId("window1");
            WindowId window2 = new WindowId("window2");
            
            windowManager.Register(window1);
            windowManager.Register(window2);
            
            // Init state
            manipulatorMock.AssertVisible(window1, false);
            manipulatorMock.AssertVisible(window2, false);
            
            // Open first window
            windowManager.Open(window1);
            manipulatorMock.AssertVisible(window1, true);
            
            // Open second window
            windowManager.Open(window2);
            manipulatorMock.AssertVisible(window1, false);
            manipulatorMock.AssertVisible(window2, true);
        }
    }
}
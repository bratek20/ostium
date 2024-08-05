using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using Xunit;

namespace B20.Frontend.Windows.Tests
{
    public class WindowOpenerMock : WindowOpener
    {
        private WindowId lastOpenedWindowId;
        
        public void Open(WindowId id)
        {
            lastOpenedWindowId = id;
        }

        public void AssertOpenCalled(WindowId id)
        {
            Assert.Equal(id, lastOpenedWindowId);
        }

    }
    
    public class WindowsImplTest
    {
        [Fact]
        public void ShouldOpenWindow()
        {
            // Given
            WindowOpenerMock openerMock = new WindowOpenerMock();
            WindowManager windowManager = new WindowManagerLogic(openerMock);
            
            WindowId windowId = new WindowId("windowId");
            
            // When
            windowManager.Open(windowId);
            var current = windowManager.GetCurrent();
            
            // Then
            openerMock.AssertOpenCalled(windowId);
            Assert.Equal(windowId, current);
        }
    }
}
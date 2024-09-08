using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using Xunit;

namespace B20.Tests.Frontend.Windows.Fixtures
{
    public class WindowManipulatorMock : WindowManipulator
    {
        private Dictionary<string, bool> windowVisibility = new Dictionary<string, bool>();

        public void SetVisible(WindowId id, bool visible)
        {
            windowVisibility[id.Value] = visible;
        }

        public void AssertVisible(string windowId, bool visible)
        {
            Assert.Equal(visible, windowVisibility[windowId]);
        }
        
        public void AssertNoSetVisibleCalls()
        {
            Assert.Empty(windowVisibility);
        }
    }
    
    public class WindowManipulatorMockImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<WindowManipulator, WindowManipulatorMock>();
        }
    }
}
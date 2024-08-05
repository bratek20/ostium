using System.Collections.Generic;
using B20.Frontend.Windows.Api;
using Xunit;

namespace B20.Tests.Frontend.Windows.Fixtures
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
}
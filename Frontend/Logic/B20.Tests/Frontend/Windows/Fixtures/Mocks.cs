using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;

namespace B20.Tests.Frontend.Windows.Fixtures
{
    public class InMemoryWindowManipulator : WindowManipulator
    {
        private Dictionary<string, bool> windowVisibility = new Dictionary<string, bool>();
        
        public void SetVisible(Window window, bool visible)
        {
            windowVisibility[window.GetType().Name] = visible;
        }

        public bool GetVisible<T>() where T : Window
        {
            return windowVisibility[typeof(T).Name];
        }
    }
    
    public class WindowManipulatorInMemoryImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<WindowManipulator, InMemoryWindowManipulator>();
        }
    }
}
using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using Xunit;

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

    public class WindowManagerMock : WindowManager
    {
        public T Get<T>() where T : class, Window
        {
            throw new System.NotImplementedException();
        }
        
        private Type lastOpenedWindow;
        
        public void Open<TWindow, TWindowState>(TWindowState state) where TWindow : Window<TWindowState> where TWindowState : WindowState
        {
            lastOpenedWindow = typeof(TWindow);   
        }

        public void AssertLastOpenedWindow<T>() where T : class, Window
        {
            Assert.True(lastOpenedWindow == typeof(T), "Last opened window is not " + typeof(T).Name);
        }

        public Window GetCurrent()
        {
            throw new System.NotImplementedException();
        }
    }
}
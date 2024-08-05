using System.Collections.Generic;
using B20.Frontend.Windows.Api;

namespace B20.Frontend.Windows.Impl
{
    public class WindowManagerLogic: WindowManager
    {
        private WindowManipulator windowManipulator;
        private Window currentWindow;
        private List<Window> registeredWindows = new List<Window>();
        
        public WindowManagerLogic(WindowManipulator windowManipulator)
        {
            this.windowManipulator = windowManipulator;
        }
        
        public void Register(Window window)
        {
            registeredWindows.Add(window);
            windowManipulator.SetVisible(window.GetId(), false);
        }

        public void Open(WindowId id)
        {
            Window window = registeredWindows.Find(w => w.GetId().Value == id.Value);
        
            if (currentWindow != null)
            {
                SetVisible(currentWindow, false);
            }
            
            SetVisible(window, true);
            currentWindow = window;
        }

        public WindowId GetCurrent()
        {
            return currentWindow.GetId();
        }
        
        private void SetVisible(Window window, bool visible)
        {
            windowManipulator.SetVisible(window.GetId(), visible);
        }
    }
}
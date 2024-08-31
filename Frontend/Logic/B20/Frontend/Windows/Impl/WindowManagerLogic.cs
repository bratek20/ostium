using System.Collections.Generic;
using B20.Frontend.Windows.Api;

namespace B20.Frontend.Windows.Impl
{
    public class WindowManagerLogic: WindowManager
    {
        private WindowManipulator windowManipulator;
        private Window currentWindow;
        private List<Window> registeredWindows = new List<Window>();
        private bool firstOpen = true;
        
        public WindowManagerLogic(WindowManipulator windowManipulator)
        {
            this.windowManipulator = windowManipulator;
        }
        
        public void Register(Window window)
        {
            registeredWindows.Add(window);
        }

        public Window Get(WindowId id)
        {
            var window = registeredWindows.Find(w => w.GetId().Value == id.Value);
            if (window == null)
            {
                throw new WindowNotFoundException("Window " + id.Value + " not found");
            }
            
            return window;
        }

        public void Open(WindowId id)
        {
            if (firstOpen)
            {
                registeredWindows.ForEach(w => SetVisible(w, false));
                firstOpen = false;
            }

            if (currentWindow != null)
            {
                SetVisible(currentWindow, false);
            }
            
            Window window = Get(id);
            SetVisible(window, true);
            currentWindow = window;
            currentWindow.OnOpen();
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
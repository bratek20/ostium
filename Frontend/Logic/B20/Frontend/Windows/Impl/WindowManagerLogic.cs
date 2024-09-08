using System.Collections.Generic;
using System.Linq;
using B20.Frontend.Windows.Api;

namespace B20.Frontend.Windows.Impl
{
    public class WindowManagerLogic: WindowManager
    {
        private WindowManipulator windowManipulator;
        private Window currentWindow;
        private List<Window> windows;
        private bool firstOpen = true;
        
        public WindowManagerLogic(
            WindowManipulator windowManipulator,
            IEnumerable<Window> windows
        )
        {
            this.windowManipulator = windowManipulator;
            this.windows = windows.ToList();
        }

        public Window Get(WindowId id)
        {
            var window = windows.Find(w => w.GetId().Value == id.Value);
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
                windows.ForEach(w => SetVisible(w, false));
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
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

        public WindowManagerLogic(
            WindowManipulator windowManipulator,
            IEnumerable<Window> windows
        )
        {
            this.windowManipulator = windowManipulator;
            this.windows = windows.ToList();
            
            this.windows.ForEach(w => SetVisible(w, false));
        }

        private void SetVisible(Window window, bool visible)
        {
            windowManipulator.SetVisible(window, visible);
        }

        public T Get<T>() where T : class, Window
        {
            var window = windows.Find(w => w is T) as T;
            if (window == null)
            {
                throw new WindowNotFoundException("Window " + typeof(T).Name + " not found");
            }
            
            return window;
        }

        public void Open<T>() where T : class, Window
        {
            if (currentWindow != null)
            {
                SetVisible(currentWindow, false);
            }
            
            Window window = Get<T>();
            SetVisible(window, true);
            currentWindow = window;
            currentWindow.OnOpen();
        }

        public T GetCurrent<T>() where T : class, Window 
        {
            return currentWindow as T;
        }
    }
}
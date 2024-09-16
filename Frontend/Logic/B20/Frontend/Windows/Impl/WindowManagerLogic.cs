using System;
using System.Collections.Generic;
using System.Linq;
using B20.Frontend.Windows.Api;

namespace B20.Frontend.Windows.Impl
{
    public class WindowManagerLogic: WindowManager
    {
        private WindowManipulator windowManipulator;
        private Window currentWindow;
        
        // to break circular dependencies and allow windows to use manager
        private Lazy<IEnumerable<Window>> lazyWindows; 
        private List<Window> cachedWindows;
        private List<Window> Windows
        {
            get
            {
                if (cachedWindows != null)
                {
                    return cachedWindows;
                }
                
                cachedWindows = lazyWindows.Value.ToList();
                foreach (var window in cachedWindows)
                {
                    SetVisible(window, false);
                }
                
                return cachedWindows;
            }
        }

        public WindowManagerLogic(
            WindowManipulator windowManipulator,
            Lazy<IEnumerable<Window>> lazyWindows
        )
        {
            this.windowManipulator = windowManipulator;
            this.lazyWindows = lazyWindows;
        }

        private void SetVisible(Window window, bool visible)
        {
            windowManipulator.SetVisible(window, visible);
        }

        public T Get<T>() where T : class, Window
        {
            var window = Windows.Find(w => w is T) as T;
            if (window == null)
            {
                throw new WindowNotFoundException("Window " + typeof(T).Name + " not found");
            }
            
            return window;
        }

        public void Open<TWindow, TWindowState>(TWindowState state) where TWindow : Window<TWindowState>
        {
            if (currentWindow != null)
            {
                SetVisible(currentWindow, false);
            }
            
            var window = (Window<TWindowState>)Get<TWindow>();
            SetVisible(window, true);
            
            currentWindow = window;
            window.Open(state);
        }

        public Window GetCurrent() 
        {
            return currentWindow;
        }
    }
}
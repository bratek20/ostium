using System.Collections.Generic;
using B20.Frontend.Windows.Api;

namespace B20.Frontend.Windows.Impl
{
    public class WindowManagerLogic: WindowManager
    {
        private WindowManipulator windowManipulator;
        private WindowId currentWindowId;
        private List<WindowId> registeredWindows = new List<WindowId>();
        
        public WindowManagerLogic(WindowManipulator windowManipulator)
        {
            this.windowManipulator = windowManipulator;
        }

        public void Register(WindowId id)
        {
            registeredWindows.Add(id);
            windowManipulator.SetVisible(id, false);
        }

        public void Open(WindowId id)
        {
            if (currentWindowId != null)
            {
                windowManipulator.SetVisible(currentWindowId, false);
            }
            
            windowManipulator.SetVisible(id, true);
            currentWindowId = id;
        }

        public WindowId GetCurrent()
        {
            return currentWindowId;
        }
    }
}
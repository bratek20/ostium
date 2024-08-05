using B20.Frontend.Windows.Api;

namespace B20.Frontend.Windows.Impl
{
    public class WindowManagerLogic: WindowManager
    {
        private WindowOpener windowOpener;
        private WindowId currentWindowId;
        
        public WindowManagerLogic(WindowOpener windowOpener)
        {
            this.windowOpener = windowOpener;
        }
        
        public void Open(WindowId id)
        {
            windowOpener.Open(id);
            currentWindowId = id;
        }

        public WindowId GetCurrent()
        {
            return currentWindowId;
        }
    }
}
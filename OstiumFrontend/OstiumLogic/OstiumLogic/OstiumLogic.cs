using B20.Frontend.Windows.Api;

namespace Ostium.Logic
{
    public class OstiumLogic
    {
        private WindowManager windowManager;
        
        public OstiumLogic(WindowManager windowManager)
        {
            this.windowManager = windowManager;
        }
        
        public void RegisterWindows()
        {
            windowManager.Register(new MainWindow(windowManager));
            windowManager.Register(new GameWindow());
        }

        public void Start()
        {
            windowManager.Open(WindowIds.MAIN_WINDOW);
        }
    }
}
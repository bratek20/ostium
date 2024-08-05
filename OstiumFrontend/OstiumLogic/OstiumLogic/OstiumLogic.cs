using B20.Frontend.Windows.Api;

namespace Ostium.Logic
{
    public class OstiumLogic
    {
        private WindowManager windowManager;
        
        public OstiumLogic(WindowManager windowManager)
        {
            this.windowManager = windowManager;
            
            RegisterWindows();
            this.windowManager.Open(WindowIds.MAIN_WINDOW);
        }
        
        private void RegisterWindows()
        {
            windowManager.Register(new MainWindow());
            windowManager.Register(new GameWindow());
        }
    }
}
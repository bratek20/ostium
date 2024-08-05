using B20.Frontend.Windows.Api;
using B20.Logic;

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
            windowManager.Register(WindowIds.MAIN_WINDOW);
            windowManager.Register(WindowIds.GAME_WINDOW);
        }
    }
}
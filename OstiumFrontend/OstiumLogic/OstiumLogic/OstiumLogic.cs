using B20.Events.Api;
using B20.Frontend.Windows.Api;
using GameSetup.Impl;

namespace Ostium.Logic
{
    public class OstiumLogic
    {
        private EventPublisher eventPublisher;
        private WindowManager windowManager;
        
        public OstiumLogic(EventPublisher eventPublisher, WindowManager windowManager)
        {
            this.eventPublisher = eventPublisher;
            this.windowManager = windowManager;
        }
        
        public void RegisterWindows()
        {
            windowManager.Register(new MainWindow(windowManager));
            windowManager.Register(new GameWindow(eventPublisher, new GameSetupApiLogic()));
        }

        public void Start()
        {
            windowManager.Open(WindowIds.MAIN_WINDOW);
        }
    }
}
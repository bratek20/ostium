using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using Ostium.Logic.GameModule.Context;

namespace Ostium.Logic
{
    public class OstiumLogic
    {
        private WindowManager windowManager;
        
        public OstiumLogic(
            WindowManager windowManager
        )
        {
            this.windowManager = windowManager;
        }

        public void Start()
        {
            windowManager.Open<MainWindow>();
        }
    }
    
    public class OstiumLogicImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .WithModules(
                    new GameModuleViewModel()
                )
                .SetClass<OstiumLogic>()
                .SetClass<PlayButton>()
                .AddImpl<Window, MainWindow>()
                .AddImpl<Window, GameWindow>();
        }
    }
}
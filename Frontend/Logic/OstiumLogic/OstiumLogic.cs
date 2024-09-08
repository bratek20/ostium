using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Contexts.Impl;
using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Infrastructure.HttpClient.Integrations;
using B20.Infrastructure.HttpClientModule.Context;
using GameModule.Api;
using GameModule.Web;
using HttpClientModule.Api;
using HttpClientModule.Impl;
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
            windowManager.Open(WindowIds.MAIN_WINDOW);
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
using B20.Architecture.Contexts.Api;
using B20.Architecture.Events.Context;
using B20.Ext;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Context;
using B20.Infrastructure.HttpClientModule.Context;
using HttpClientModule.Api;
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
    
    public class OstiumLogicPartialImpl: ContextModule
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
    
    public class OstiumLogicFullImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .WithModules(
                    new OstiumLogicPartialImpl(),
                    new WindowsImpl(),
                    new EventsImpl(),
                    new DotNetHttpClientModuleImpl(),
                    new GameModuleWebClient(
                        HttpClientConfig.Create(
                            baseUrl: "http://localhost:8080",
                            auth: Optional<HttpClientAuth>.Empty()
                        )
                    )
                );
        }
    }
}
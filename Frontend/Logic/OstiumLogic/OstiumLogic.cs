using B20.Architecture.Contexts.Api;
using B20.Architecture.Events.Context;
using B20.Ext;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Context;
using B20.Infrastructure.HttpClientModule.Context;
using GameModule.Context;
using HttpClientModule.Api;
using Main.ViewModel;
using Ostium.Logic.GameModule.Context;
using Ostium.Logic.MainWindowModule.Context;

namespace Ostium.Logic
{
    public class OstiumLogic
    {
        private WindowManager windowManager;
        
        public OstiumLogic(WindowManager windowManager)
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
                    new GameModuleViewModel(),
                    new MainViewModel()
                )
                .SetClass<OstiumLogic>();
        }
    }

    public class OstiumLogicNoBackendImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .WithModules(
                    new OstiumLogicPartialImpl(),
                    new WindowsImpl(),
                    new EventsImpl(),
                    new UiElementsImpl(),
                    new TraitsImpl()
                );
        }
    }
    
    public class OstiumLogicFullImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .WithModules(
                    new OstiumLogicNoBackendImpl(),
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
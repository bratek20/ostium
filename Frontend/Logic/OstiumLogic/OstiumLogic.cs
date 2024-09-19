using B20.Architecture.Contexts.Api;
using B20.Architecture.Events.Context;
using B20.Ext;
using B20.Frontend.Timer.Context;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Context;
using B20.Infrastructure.HttpClientModule.Context;
using GamesManagement.Context;
using SingleGame.Context;
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
            windowManager.Open<MainWindow, EmptyWindowState>(new EmptyWindowState());
        }
    }

    public class OstiumLogicNoBackendImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .WithModules( //core
                    new WindowsImpl(),
                    new EventsImpl(),
                    new UiElementsImpl(),
                    new TraitsImpl(),
                    new TimerImpl()
                )
                .WithModules( // ostium modules
                    new MainViewModel(),
                    new GamesManagementViewModel(),
                    new SingleGameViewModel()
                )
                .SetClass<OstiumLogic>();
        }
    }
    
    public class OstiumLogicFullImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            var httpConfig = HttpClientConfig.Create(
                baseUrl: "http://localhost:8080",
                auth: Optional<HttpClientAuth>.Empty()
            );
            
            builder
                .WithModules(
                    new OstiumLogicNoBackendImpl(),
                    
                    new DotNetHttpClientModuleImpl(),
                    new SingleGameWebClient(httpConfig),
                    new GamesManagementWebClient(httpConfig)
                );
        }
    }
}
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
    public class OstiumLogicFactory
    {
        public static OstiumLogic Create(
            WindowManipulator windowManipulator
        ) {
            return new OstiumLogic(
                new EventPublisherLogic(), 
                new WindowManagerLogic(windowManipulator),
                CreateWebClientForGameApi()
            );
        }

        public static GameApi CreateWebClientForGameApi()
        {
            return ContextModuleFactory.CreateBuilder()
                .WithModules(
                    new DotNetHttpClientModuleImpl(),
                    new GameModuleWebClient(
                        HttpClientConfig.Create(
                            baseUrl: "http://localhost:8080",
                            auth: Optional<HttpClientAuth>.Empty()
                        )
                    )
                )
                .Get<GameApi>();
        }
    }

    public class OstiumLogic
    {
        // context
        public EventPublisher EventPublisher { get; }
        public WindowManager WindowManager { get; }
        public GameApi GameApi { get; }
        
        public OstiumLogic(
            EventPublisher eventPublisher, 
            WindowManager windowManager,
            GameApi gameApi
        ) {
            EventPublisher = eventPublisher;
            WindowManager = windowManager;
            GameApi = gameApi;
            
            RegisterWindows();
        }
        
        private void RegisterWindows()
        {
            WindowManager.Register(new MainWindow(WindowManager));
            WindowManager.Register(new GameWindow(EventPublisher, GameApi));
        }

        public void Start()
        {
            WindowManager.Open(WindowIds.MAIN_WINDOW);
        }
    }
}
using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Infrastructure.HttpClient.Integrations;
using GameModule.Api;
using GameModule.Web;
using HttpClientModule.Api;
using HttpClientModule.Impl;

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
                createWebClientForGameApi()
            );
        }

        public static GameApi createWebClientForGameApi()
        {
            var factory = new HttpClientFactoryLogic(new DotNetHttpRequester());
            return new GameApiWebClient(
                factory,
                new GameModuleWebClientConfig(
                    HttpClientConfig.Create(
                        baseUrl: "http://localhost:8080",
                        auth: Optional<HttpClientAuth>.Empty()
                    )
                )
            );
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
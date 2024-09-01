using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Infrastructure.HttpClient.Integrations;
using GameSetup.Api;
using GameSetup.Impl;
using GameSetup.Web;
using HttpClientModule.Api;
using HttpClientModule.Impl;

namespace Ostium.Logic
{
    public class OstiumLogicFactory
    {
        public static OstiumLogic Create(
            WindowManipulator windowManipulator, 
            bool useBuiltInServer
        ) {
            return new OstiumLogic(
                new EventPublisherLogic(), 
                new WindowManagerLogic(windowManipulator),
                useBuiltInServer ? CreateBuiltInGameSetupApi() : createWebClientForGameSetupApi()
            );
        }
        
        static GameSetupApi CreateBuiltInGameSetupApi()
        {
            return new GameSetupApiLogic();
        }
        
        public static GameSetupApi createWebClientForGameSetupApi()
        {
            var factory = new HttpClientFactoryLogic(new DotNetHttpRequester());
            return new GameSetupApiWebClient(
                factory,
                new GameSetupWebClientConfig(
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
        public GameSetupApi GameSetupApi { get; }
        
        internal OstiumLogic(
            EventPublisher eventPublisher, 
            WindowManager windowManager,
            GameSetupApi gameSetupApi
        ) {
            EventPublisher = eventPublisher;
            WindowManager = windowManager;
            GameSetupApi = gameSetupApi;
            
            RegisterWindows();
        }
        
        private void RegisterWindows()
        {
            WindowManager.Register(new MainWindow(WindowManager));
            WindowManager.Register(new GameWindow(EventPublisher, GameSetupApi));
        }

        public void Start()
        {
            WindowManager.Open(WindowIds.MAIN_WINDOW);
        }
    }
}
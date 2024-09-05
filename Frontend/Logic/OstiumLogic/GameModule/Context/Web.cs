using B20.Architecture.ContextModule.Api;
using GameModule.Api;
using GameModule.Web;
using HttpClientModule.Api;

namespace Ostium.Logic.GameModule.Context
{
    public class GameModuleWebClient : ContextModule
    {
        private readonly HttpClientConfig _config;
        public GameModuleWebClient(HttpClientConfig config)
        {
            _config = config;
        }
        
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImplObject(new GameModuleWebClientConfig(_config))
                .SetImpl<GameApi, GameApiWebClient>();
        }
    }
}
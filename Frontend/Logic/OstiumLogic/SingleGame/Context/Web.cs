using B20.Architecture.Contexts.Api;
using SingleGame.Api;
using SingleGame.Web;
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
                .SetImpl<SingleGameApi, GameApiWebClient>();
        }
    }
}
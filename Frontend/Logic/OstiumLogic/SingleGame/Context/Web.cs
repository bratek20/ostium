using B20.Architecture.Contexts.Api;
using SingleGame.Api;
using SingleGame.Web;
using HttpClientModule.Api;

namespace Ostium.Logic.GameModule.Context
{
    public class SingleGameWebClient : ContextModule
    {
        private readonly HttpClientConfig _config;
        public SingleGameWebClient(HttpClientConfig config)
        {
            _config = config;
        }
        
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImplObject(new SingleGameWebClientConfig(_config))
                .SetImpl<SingleGameApi, SingleGameApiWebClient>();
        }
    }
}
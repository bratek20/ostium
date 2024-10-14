using B20.Architecture.Contexts.Api;
using GamesManagement.Api;
using GamesManagement.Web;
using HttpClientModule.Api;

namespace GamesManagement.Context
{
    public class GamesManagementWebClient : ContextModule
    {
        private readonly HttpClientConfig _config;
        public GamesManagementWebClient(HttpClientConfig config)
        {
            _config = config;
        }
        
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImplObject<GamesManagementWebClientConfig>(new GamesManagementWebClientConfig(_config))
                .SetImpl<GamesManagementApi, GamesManagementApiWebClient>();
        }
    }
}
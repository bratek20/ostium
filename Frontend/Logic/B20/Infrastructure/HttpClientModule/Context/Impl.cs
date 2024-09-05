using B20.Architecture.ContextModule.Api;
using B20.Infrastructure.HttpClient.Integrations;
using HttpClientModule.Api;
using HttpClientModule.Impl;

namespace B20.Infrastructure.HttpClientModule.Context
{
    public class HttpClientModuleImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<HttpClientFactory, HttpClientFactoryLogic>();
        }
    }
    
    public class DotNetHttpClientModuleImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .WithModule(new HttpClientModuleImpl())
                .SetImpl<HttpRequester, DotNetHttpRequester>();
        }
    }
}
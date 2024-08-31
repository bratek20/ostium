using B20.Ext;
using HttpClient.Api;

namespace HttpClient.Impl
{
    class HttpResponseLogic: HttpResponse
    {
        public int getStatusCode()
        {
            return 200;
        }

        public Optional<object> getBody<T>()
        {
            throw new System.NotImplementedException();
        }
    }

    class HttpClientLogic : Api.HttpClient
    {
        public HttpResponse Get(string path)
        {
            return new HttpResponseLogic();
        }

        public HttpResponse Post(string path, Optional<object> body)
        {
            throw new System.NotImplementedException();
        }
    }
    public class HttpClientFactoryLogic: HttpClientFactory
    {
        public Api.HttpClient Create(HttpClientConfig config)
        {
            return new HttpClientLogic();
        }
    }
}
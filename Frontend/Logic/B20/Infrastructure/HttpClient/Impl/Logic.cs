using B20.Ext;
using B20.Logic.Utils;
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
        private readonly HttpRequester requester;

        public HttpClientLogic(HttpRequester requester)
        {
            this.requester = requester;
        }

        public HttpResponse Get(string path)
        {
            requester.Send(
                HttpRequest.Create(
                    url: "http://localhost:8080/test",
                    method: HttpMethod.GET,
                    content: Optional<string>.Empty(), 
                    contentType: "",
                    headers: ListUtils.Of<HttpHeader>()
                )
            );
            return new HttpResponseLogic();
        }

        public HttpResponse Post(string path, Optional<object> body)
        {
            throw new System.NotImplementedException();
        }
    }
    public class HttpClientFactoryLogic: HttpClientFactory
    {
        private readonly HttpRequester requester;

        public HttpClientFactoryLogic(HttpRequester requester)
        {
            this.requester = requester;
        }

        public Api.HttpClient Create(HttpClientConfig config)
        {
            return new HttpClientLogic(requester);
        }
    }
}
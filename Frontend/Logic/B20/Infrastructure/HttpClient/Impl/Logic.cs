using B20.Ext;
using B20.Logic.Utils;
using HttpClient.Api;
using System.Collections.Generic;
using B20.Architecture.Serialization.Context;
using Serialization.Api;

namespace HttpClient.Impl
{
    class HttpResponseLogic : HttpResponse
    {
        private static Serializer serializer = SerializationFactory.CreateSerializer();
        
        private readonly int statusCode;
        private readonly string responseBody;

        public HttpResponseLogic(int statusCode, string responseBody)
        {
            this.statusCode = statusCode;
            this.responseBody = responseBody;
        }

        public int GetStatusCode()
        {
            return statusCode;
        }

        public Optional<T> GetBody<T>()
        {
            if (string.IsNullOrEmpty(responseBody))
            {
                return Optional<T>.Empty();
            }

            try
            {
                T result = serializer.Deserialize<T>(SerializedValue.Create(responseBody, SerializationType.JSON));
                return Optional<T>.Of(result);
            }
            catch
            {
                return Optional<T>.Empty();
            }
        }
    }

    class HttpClientLogic : Api.HttpClient
    {
        private static Serializer serializer = SerializationFactory.CreateSerializer();
        
        private readonly HttpRequester requester;
        private readonly HttpClientConfig config;

        public HttpClientLogic(HttpRequester requester, HttpClientConfig config)
        {
            this.requester = requester;
            this.config = config;
        }

        public HttpResponse Get(string path)
        {
            var response = requester.Send(
                HttpRequest.Create(
                    url: config.GetBaseUrl() + path,
                    method: HttpMethod.GET,
                    content: Optional<string>.Empty(),
                    contentType: "",
                    headers: ListUtils.Of<HttpHeader>()
                )
            );
            return new HttpResponseLogic(200, response);
        }

        public HttpResponse Post(string path, Optional<object> body)
        {
            string content = body.IsPresent() ? serializer.Serialize(body.Get()).GetValue() : "";
            var headers = new List<HttpHeader>();

            if (config.GetAuth().IsPresent())
            {
                headers.Add(new HttpHeader("Authorization", config.GetAuth().Get().GetValue()));
            }

            var response = requester.Send(
                HttpRequest.Create(
                    url: config.GetBaseUrl() + path,
                    method: HttpMethod.POST,
                    content: Optional<string>.Of(content),
                    contentType: "application/json",
                    headers: headers
                )
            );

            return new HttpResponseLogic(200, response);
        }
    }

    public class HttpClientFactoryLogic : HttpClientFactory
    {
        private readonly HttpRequester requester;

        public HttpClientFactoryLogic(HttpRequester requester)
        {
            this.requester = requester;
        }

        public Api.HttpClient Create(HttpClientConfig config)
        {
            return new HttpClientLogic(requester, config);
        }
    }
}

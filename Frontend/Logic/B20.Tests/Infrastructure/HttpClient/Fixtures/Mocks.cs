using HttpClient.Api;

namespace B20.Tests.Infrastructure.HttpClient.Fixtures
{
    public class HttpRequesterMock: HttpRequester
    {
        public string Send(HttpRequest request)
        {
            return Response;
        }

        public string Response { set; get; }
    }
}
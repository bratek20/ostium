using B20.Tests.Infrastructure.HttpClient.Fixtures;
using HttpClient.Api;
using HttpClient.Impl;
using Xunit;

namespace HttpClient.Tests
{
    public class HttpClientImplTest
    {
        private readonly HttpRequesterMock requesterMock;
        private readonly HttpClientFactory factory;

        public HttpClientImplTest()
        {
            requesterMock = new HttpRequesterMock();
            factory = new HttpClientFactoryLogic(requesterMock);
        }
        
        private Api.HttpClient CreateClient()
        {
            return factory.Create(null);
        }
            
        [Fact]
        public void ShouldSupportGet()
        {
            // // given
            // const {client, requester} = setup({
            //     config: {
            //         baseUrl: "http://localhost:8080"
            //     }
            // })
            // requester.setResponse('{"value": "Some value"}')
            //
            // // when
            // const response = client.get("/test")
            //
            // //then
            // AssertEquals(response.getStatusCode(), 200)
            //
            // const body = response.getBody(SomeResponse).get()
            // AssertEquals(body.getValue(), "Some value")
            //
            // requester.assertCalledOnce({
            //     url: "http://localhost:8080/test",
            //     method: HttpMethod.GET,
            //     contentEmpty: true,
            //     contentTypeEmpty: true,
            //     headers: []
            // })
            
            requesterMock.Response = "{\"value\": \"Some value\"}";
            var response = CreateClient().Get("/test");
            
            Assert.Equal(response.getStatusCode(), 200);

            requesterMock.AssertCalledOnce(e =>
            {
                e.Url = "http://localhost:8080/test";
            });
        }
    }
}
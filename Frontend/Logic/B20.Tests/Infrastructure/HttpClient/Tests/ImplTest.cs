using B20.Tests.Infrastructure.HttpClient.Fixtures;
using HttpClient.Api;
using HttpClient.Impl;
using Xunit;

namespace HttpClient.Tests
{
    public class HttpClientImplTest
    {
        // interface SetupArgs {
        //     config?: Builder.HttpClientConfigDef
        //         context?: HandlerContext
        // }
        // function setup(args: SetupArgs = {}) {
        //     const context = args.context ?? EmptyContext()
        //     const requester = new HttpRequesterMock()
        //     const factory = new Impl.HttpClientFactoryLogic(requester, context)
        //
        //     const client = factory.create(
        //         Builder.httpClientConfig(args.config),
        //     )
        //
        //     const logMocks = Log.Mocks.Setup()
        //     return {client, requester, context, logMocks}
        // }
        class Context
        {
            public Api.HttpClient Client;
            public HttpRequesterMock RequesterMock;
        }
        private Context Setup()
        {
            return new Context
            {
                Client = new HttpClientFactoryLogic().Create(null),
                RequesterMock = new HttpRequesterMock()
            };
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
            
            var c = Setup();
            c.RequesterMock.Response = "{\"value\": \"Some value\"}";
            var response = c.Client.Get("/test");
            
            Assert.Equal(response.getStatusCode(), 200);

            c.RequesterMock.AssertCalledOnce(e =>
            {
                e.Url = "http://localhost:8080/test";
            });
        }
    }
}
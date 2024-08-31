using System;
using System.Collections.Generic;
using B20.Ext;
using B20.Tests.ExtraAsserts;
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
        
        class CreateArgs {
            public string BaseUrl = "http://localhost:8080";
            public string AuthValue = null;
        }
        private Api.HttpClient CreateClient(Action<CreateArgs> setup = null)
        {
            var args = new CreateArgs();
            setup?.Invoke(args);
            return factory.Create(HttpClientConfig.Create(
                args.BaseUrl, 
                Optional<String>.Of(args.AuthValue).Map(HttpClientAuth.Create)
            ));
        }

        [Fact]
        public void ShouldSupportGet()
        {
            var client = CreateClient(it =>
            {
                it.BaseUrl = "http://localhost:8081";
            });
            
            requesterMock.Response = "{\"value\": \"Some value\"}";
            var response = client.Get("/test");
            
            AssertExt.Equals(response.getStatusCode(), 200);
            requesterMock.AssertCalledOnce(e =>
            {
                e.Url = "http://localhost:8081/test";
                e.Method = HttpMethod.GET;
                e.ContentEmpty = true;
                e.Headers = new List<HttpHeader>();
            });
        }
        
        // class SomeRequest {
        //     private value = STRING
        //
        //     static create(value: string) {
        //         const instance = new SomeRequest()
        //         instance.value = value
        //         return instance
        //     }
        // }
        // test("Should work - post: auth header, debug log for request", () => {
        //     // given
        //     const {client, requester, logMocks} = setup({
        //         config: {
        //             baseUrl: "http://localhost:8080",
        //             auth: {
        //                 value: "Basic abc"
        //             } 
        //         }
        //     })
        //     requester.setResponse('{"value": "response value"}')
        //
        //     // when
        //     const response = client.post("/test", Optional.of(SomeRequest.create("request value")))
        //
        //     //then
        //     AssertEquals(response.getStatusCode(), 200)
        //
        //     const body = response.getBody(SomeResponse).get()
        //     AssertEquals(body.getValue(), "response value")
        //
        //     requester.assertCalledOnce({
        //         url: "http://localhost:8080/test",
        //         method: HttpMethod.POST,
        //         content: '{"value":"request value"}',
        //         contentType: "application/json",
        //         headers: [
        //         {
        //             key: "Authorization",
        //             value: "Basic abc"
        //         }
        //         ]
        //     })
        //
        //     logMocks.assertOneDebug(
        //         "Request url: http://localhost:8080/test, " +
        //         "method: POST, " +
        //         `content: {"value":"request value"}, ` + 
        //             "contentType: application/json, " +
        //         "headers: [Authorization: Basic abc, ]" +
        //         `response: {"value": "response value"}`
        //     );            
        // });
        private class SomeRequest
        {
            public string value;

            private SomeRequest(string value)
            {
                this.value = value;
            }

            public static SomeRequest Create(string value)
            {
                return new SomeRequest(value);
            }
        }

        private class SomeResponse
        {
            public string value;
        }
        
        [Fact]
        public void ShouldSupportPostWithAuthHeader()
        {
            // Arrange
            var client = CreateClient(it =>
            {
                it.BaseUrl = "http://localhost:8080";
                it.AuthValue = "Basic abc";
            });

            requesterMock.Response = "{\"value\": \"response value\"}";

            var request = SomeRequest.Create("request value");

            // Act
            var response = client.Post("/test", Optional<object>.Of(request));

            // Assert
            AssertExt.Equals(response.getStatusCode(), 200);

            var responseBody = response.getBody<SomeResponse>().Get();
            AssertExt.Equals(responseBody.value, "response value");

            requesterMock.AssertCalledOnce(e =>
            {
                e.Url = "http://localhost:8080/test";
                e.Method = HttpMethod.POST;
                e.Content = "{\"value\":\"request value\"}";
                e.ContentType = "application/json";
                e.Headers = new List<HttpHeader>
                {
                    new HttpHeader("Authorization", "Basic abc")
                };
            });
            
        }
    }
}
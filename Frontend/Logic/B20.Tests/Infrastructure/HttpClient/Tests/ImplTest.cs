using System;
using System.Collections.Generic;
using B20.Architecture.Logs.Fixtures;
using B20.Ext;
using B20.Tests.ExtraAsserts;
using HttpClientModule.Api;
using HttpClientModule.Fixtures;
using HttpClientModule.Impl;
using Xunit;

namespace HttpClientModule.Tests
{
    public class HttpClientImplTest
    {
        private readonly HttpRequesterMock requesterMock;
        private readonly HttpClientFactory factory;
        private readonly LoggerMock loggerMock;

        public HttpClientImplTest()
        {
            requesterMock = new HttpRequesterMock();
            loggerMock = new LoggerMock();
            factory = new HttpClientFactoryLogic(requesterMock, loggerMock);
        }
        
        class CreateArgs {
            public string BaseUrl = "http://localhost:8080";
            public string AuthValue = null;
        }
        private HttpClient CreateClient(Action<CreateArgs> setup = null)
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
            
            AssertExt.Equal(response.GetStatusCode(), 200);
            requesterMock.AssertCalledOnce(e =>
            {
                e.Url = "http://localhost:8081/test";
                e.Method = HttpMethod.GET;
                e.ContentEmpty = true;
                e.Headers = new List<HttpHeader>();
            });
        }

        private class SomeRequest
        {
            private string value;

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
            readonly string value;
            
            public SomeResponse(string value)
            {
                this.value = value;
            }
            
            public string GetValue()
            {
                return value;
            }
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
            AssertExt.Equal(response.GetStatusCode(), 200);

            var responseBody = response.GetBody<SomeResponse>().Get();
            AssertExt.Equal(responseBody.GetValue(), "response value");

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
            
            loggerMock.AssertInfos(
                "Calling POST, " 
                + "url: http://localhost:8080/test, "
                + "content: {\"value\":\"request value\"}, "
                + "response: {\"value\": \"response value\"}"
            );
        }
    }
}
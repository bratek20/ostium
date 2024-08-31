using System;
using HttpClient.Api;
using Xunit;

namespace B20.Tests.Infrastructure.HttpClient.Fixtures
{
    public class HttpRequesterMock: HttpRequester
    {
        private int sendCalls = 0;
        private HttpRequest lastRequest;
        public string Send(HttpRequest request)
        {
            sendCalls++;
            lastRequest = request;
            
            return Response;
        }

        public string Response { set; get; }

        public class ExpectedX
        {
            public string? Url { get; set; }
        }
        public void AssertCalledOnce(Action<ExpectedX> expectedInit)
        {
            var expected = new ExpectedX();
            expectedInit.Invoke(expected);
            
            Assert.Equal(sendCalls, 1);

            if (expected.Url != null)
            {
                Assert.Equal(lastRequest.GetUrl(), expected.Url);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using B20.Tests.ExtraAsserts;
using HttpClientModule.Api;
using Xunit;

namespace HttpClientModule.Fixtures
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
            public string Url { get; set; }
            public HttpMethod Method { get; set; }
            public string Content { get; set; }
            public bool ContentEmpty { get; set; }
            public string ContentType { get; set; }
            public List<HttpHeader> Headers { get; set; }
        }
        public void AssertCalledOnce(Action<ExpectedX> expectedInit)
        {
            var expected = new ExpectedX();
            expectedInit.Invoke(expected);
            
            AssertExt.Equal(sendCalls, 1);

            if (expected.Url != null)
            {
                Assert.Equal(lastRequest.GetUrl(), expected.Url);
            }
            if (expected.Method != null)
            {
                AssertExt.Equal(lastRequest.GetMethod(), expected.Method);
            }
            if (expected.Content != null)
            {
                Assert.Equal(lastRequest.GetContent().Get(), expected.Content);
            }
            if (expected.ContentEmpty)
            {
                Assert.True(lastRequest.GetContent().IsEmpty());
            }
            if (expected.ContentType != null)
            {
                Assert.Equal(lastRequest.GetContentType(), expected.ContentType);
            }
            if (expected.Headers != null)
            {
                Assert.Equal(lastRequest.GetHeaders().Count, expected.Headers.Count);
                for (var i = 0; i < lastRequest.GetHeaders().Count; i++)
                {
                    Assert.Equal(lastRequest.GetHeaders()[i].GetKey(), expected.Headers[i].GetKey());
                    Assert.Equal(lastRequest.GetHeaders()[i].GetValue(), expected.Headers[i].GetValue());
                }
            }
        }
    }
}
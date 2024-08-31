using System;
using System.Collections.Generic;
using HttpClient.Api;
using Xunit;

namespace B20.Tests.Infrastructure.HttpClient.Fixtures
{
    // interface ReqeustArgs {
    //     url: string
    //         method: HttpMethod
    //         content: Optional<string>
    //         contentType: Optional<string>
    //         headers: HttpHeader[]
    // }
    // interface ExpectedRequestArgs {
    //     url?: string
    //         method?: HttpMethod
    //         content?: string
    //         contentEmpty?: boolean
    //         contentType?: string
    //         contentTypeEmpty?: boolean
    //         headers?: ExpectedHttpHeader[]
    // }
    // function assertRequestArgs(given: ReqeustArgs, expected: ExpectedRequestArgs) {
    //     if (Defined(expected.url)) {
    //         AssertEquals(given.url, expected.url)
    //     }
    //     if (Defined(expected.method)) {
    //         AssertEquals(given.method, expected.method)
    //     }
    //     if (Defined(expected.content)) {
    //         AssertEquals(given.content.get(), expected.content)
    //     }
    //     if (Defined(expected.contentEmpty)) {
    //         AssertEquals(given.content.isEmpty(), expected.contentEmpty)
    //     }
    //     if (Defined(expected.contentType)) {
    //         AssertEquals(given.contentType.get(), expected.contentType)
    //     }
    //     if (Defined(expected.contentTypeEmpty)) {
    //         AssertEquals(given.contentType.isEmpty(), expected.contentTypeEmpty)
    //     }
    //
    //     if (Defined(expected.headers)) {
    //         AssertEquals(given.headers.length, expected.headers.length)
    //         for (let i = 0; i < given.headers.length; i++) {
    //             Assert.httpHeader(given.headers[i], expected.headers[i])
    //         }
    //     }
    // }
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
            
            Assert.Equal(sendCalls, 1);

            if (expected.Url != null)
            {
                Assert.Equal(lastRequest.GetUrl(), expected.Url);
            }
            if (expected.Method != null)
            {
                Assert.Equal(lastRequest.GetMethod(), expected.Method);
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
                    Assert.Equal(lastRequest.GetHeaders()[i], expected.Headers[i]);
                }
            }
        }
    }
}
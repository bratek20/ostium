// DO NOT EDIT! Autogenerated by HLA tool

using System;
using System.Collections.Generic;
using B20.Ext;

namespace HttpClientModule.Api {
    public class HttpClientAuth {
        readonly string value;

        public HttpClientAuth(
            string value
        ) {
            this.value = value;
        }
        public string GetValue() {
            return value;
        }
        public static HttpClientAuth Create(string value) {
            return new HttpClientAuth(value);
        }
    }

    public class HttpClientConfig {
        readonly string baseUrl;
        readonly HttpClientAuth? auth;

        public HttpClientConfig(
            string baseUrl,
            HttpClientAuth? auth
        ) {
            this.baseUrl = baseUrl;
            this.auth = auth;
        }
        public string GetBaseUrl() {
            return baseUrl;
        }
        public Optional<HttpClientAuth> GetAuth() {
            return Optional<HttpClientAuth>.Of(auth);
        }
        public static HttpClientConfig Create(string baseUrl, Optional<HttpClientAuth> auth) {
            return new HttpClientConfig(baseUrl, auth.OrElse(null));
        }
    }

    public class HttpHeader {
        readonly string key;
        readonly string value;

        public HttpHeader(
            string key,
            string value
        ) {
            this.key = key;
            this.value = value;
        }
        public string GetKey() {
            return key;
        }
        public string GetValue() {
            return value;
        }
        public static HttpHeader Create(string key, string value) {
            return new HttpHeader(key, value);
        }
    }

    public class HttpRequest {
        readonly string url;
        readonly string method;
        readonly string? content;
        readonly string contentType;
        readonly List<HttpHeader> headers;

        public HttpRequest(
            string url,
            string method,
            string? content,
            string contentType,
            List<HttpHeader> headers
        ) {
            this.url = url;
            this.method = method;
            this.content = content;
            this.contentType = contentType;
            this.headers = headers;
        }
        public string GetUrl() {
            return url;
        }
        public HttpMethod GetMethod() {
            return (HttpMethod)Enum.Parse(typeof(HttpMethod), method);
        }
        public Optional<string> GetContent() {
            return Optional<string>.Of(content);
        }
        public string GetContentType() {
            return contentType;
        }
        public List<HttpHeader> GetHeaders() {
            return headers;
        }
        public static HttpRequest Create(string url, HttpMethod method, Optional<string> content, string contentType, List<HttpHeader> headers) {
            return new HttpRequest(url, method.ToString(), content.OrElse(null), contentType, headers);
        }
    }
}
using System;
using System.Net.Http;
using System.Text;
using HttpClientModule.Api;
using DotNetHttpMethod = System.Net.Http.HttpMethod;
using HttpMethod = HttpClientModule.Api.HttpMethod;

namespace B20.Infrastructure.HttpClient.Integrations
{
    public class DotNetHttpRequester : HttpRequester
    {
        private readonly System.Net.Http.HttpClient _httpClient;
    
        public DotNetHttpRequester()
        {
            _httpClient = new System.Net.Http.HttpClient();
        }
    
        public string Send(HttpRequest request)
        {
            try
            {
                var httpRequestMessage = new HttpRequestMessage
                {
                    Method = GetDotNetHttpMethod(request.GetMethod()),
                    RequestUri = new Uri(request.GetUrl())
                };
    
                // Add content if it's a POST request
                if (request.GetMethod() == HttpMethod.POST)
                {
                    string content = request.GetContent().OrElse(string.Empty);
                    httpRequestMessage.Content = new StringContent(content, Encoding.UTF8, request.GetContentType());
                }
    
                // Add headers
                foreach (var header in request.GetHeaders())
                {
                    httpRequestMessage.Headers.Add(header.GetKey(), header.GetValue());
                }
    
                // Send the request and get the response
                var response = _httpClient.SendAsync(httpRequestMessage).Result;
                response.EnsureSuccessStatusCode();
    
                // Read and return the response content
                return response.Content.ReadAsStringAsync().Result;
            }
            catch (Exception ex)
            {
                throw new HttpRequesterException("An error occurred while sending the HTTP request, message: " + ex.Message);
            }
        }
        
        private DotNetHttpMethod GetDotNetHttpMethod(HttpMethod method)
        {
            return method switch
            {
                HttpMethod.GET => DotNetHttpMethod.Get,
                HttpMethod.POST => DotNetHttpMethod.Post,
                _ => throw new ArgumentException("Unsupported HTTP method: " + method)
            };
        }
    }
}
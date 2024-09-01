using System.Collections.Generic;
using B20.Ext;
using B20.Infrastructure.HttpClient.Integrations;
using HttpClientModule.Api;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;
using Xunit;

namespace HttpClientModule.Tests
{
    public class DotNetHttpRequesterTest
    {
        [Fact]
        public void Send_GetRequest_ReturnsExpectedResponse()
        {
            // Arrange
            var server = WireMockServer.Start();

            server.Given(
                Request.Create().WithPath("/api/test").UsingGet()
            )
            .RespondWith(
                Response.Create().WithStatusCode(200).WithBody("Mocked GET Response")
            );

            var requester = new DotNetHttpRequester();

            var request = HttpRequest.Create(
                url: server.Url + "/api/test",
                method: HttpMethod.GET,
                content: Optional<string>.Empty(),
                contentType: "text/plain",
                headers: new List<HttpHeader>()
            );

            // Act
            var response = requester.Send(request);

            // Assert
            Assert.Equal("Mocked GET Response", response);

            // Cleanup
            server.Stop();
        }

        [Fact]
        public void Send_PostRequest_ReturnsExpectedResponse()
        {
            // Arrange
            var server = WireMockServer.Start();

            server.Given(
                Request.Create().WithPath("/api/test").UsingPost()
                .WithBody("Test Content")
                .WithHeader("Content-Type", "application/json")
            )
            .RespondWith(
                Response.Create().WithStatusCode(200).WithBody("Mocked POST Response")
            );

            var requester = new DotNetHttpRequester();

            var request = HttpRequest.Create(
                url: server.Url + "/api/test",
                method: HttpMethod.POST,
                content: Optional<string>.Of("Test Content"),
                contentType: "application/json",
                headers: new List<HttpHeader> {
                    HttpHeader.Create("Content-Type", "application/json")
                }
            );

            // Act
            var response = requester.Send(request);

            // Assert
            Assert.Equal("Mocked POST Response", response);

            // Cleanup
            server.Stop();
        }
    }
}
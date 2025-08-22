using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Sastt.Web;
using Xunit;

namespace Sastt.IntegrationTests;

public class ApiEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ApiEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Training_endpoint_requires_authentication()
    {
        var response = await _client.GetAsync("/training");
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }
}

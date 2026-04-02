using System.Net.Http.Json;
using Fotografias.Api.Test.Config;
using Humanizer;

namespace Fotografias.Api.Test;

public class AuthControllerTest: IClassFixture<FotografiasApiWebApplication<Program>>
{
    private readonly HttpClient http;
    public AuthControllerTest(FotografiasApiWebApplication<Program> factory)
    {
        http = factory.CreateClient();
    }

    [Fact]
    public async Task Create_Token()
    {
        string? _token = string.Empty;
        var form = new Dictionary<string, string>()
        {
            {"client_id", "f47ac10b-58cc-4372-a567-0e02b2c3d479"},
            {"client_secret", "Y2xhdmVfc3VwZXJfc2VjcmV0YV9tdXlfc2VndXJhXzEyMzQ1Njc4OTA="},
            {"client_credentials", "cliente_credentials"},
            {"scoped", "scoped"}
        };

        var content = new FormUrlEncodedContent(form);
        var response = await http.PostAsync("api/auth/token", content);

        var result = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
        result?.TryGetValue("token", out _token);

        Assert.True((int)response.StatusCode == 201);
        Assert.NotNull(_token);
        Assert.NotEmpty(_token);
    }

    [Theory]
    [InlineData("G47ac10b-58cc-4372-a567-0e02b2c3d479", "Y2xhdmVfc3VwZXJfc2VjcmV0YV9tdXlfc2VndXJhXzEyMzQ1Njc4OTA=")]
    public async Task Create_Token_Fail(string cliente, string secret)
    {
        var form = new Dictionary<string, string>()
        {
            {"client_id", cliente},
            {"client_secret", secret},
            {"client_credentials", "cliente_credentials"},
            {"scoped", "scoped"}
        };

        var content = new FormUrlEncodedContent(form);
        var response = await http.PostAsync("api/auth/token", content);

        Assert.True((int)response.StatusCode == 401);
    }
}

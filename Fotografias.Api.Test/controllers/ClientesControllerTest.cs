using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Fotografias.Api.Domain.dtos.responses;
using Fotografias.Api.Test.Config;

namespace Fotografias.Api.Test;

public class ClientesControllerTest : IClassFixture<FotografiasApiWebApplication<Program>>
{
    private readonly HttpClient http;
    public ClientesControllerTest(FotografiasApiWebApplication<Program> factory)
    {
        http = factory.CreateClient();
    }

    private async Task<string> GetToken()
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
        return string.IsNullOrEmpty(_token) ? string.Empty : _token;
    }

    [Fact]
    public async Task Get_Ok()
    {
        //Arrange
        var token = await GetToken();
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await http.GetAsync("api/clientes");

        //Act
        response.EnsureSuccessStatusCode();
        var clientes = await response.Content.ReadAsStringAsync();
        var elementos = JsonSerializer.Deserialize<ClientesResponse<List<ClientesAttributes>>>(clientes, new JsonSerializerOptions{ PropertyNameCaseInsensitive = true });
        
        //Assert
        Assert.True(response.IsSuccessStatusCode && (int)response.StatusCode == 200);
        Assert.NotNull(elementos);
        Assert.True(elementos?.data?.attributes?.Any());
    }

    [Theory]
    [InlineData("campos@itdurango.edu.mx")]
    public async Task Get_Ok_Single(string correo)
    {
        var token = await GetToken();
        http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await http.GetAsync($"api/clientes/cliente/{correo}");

        response.EnsureSuccessStatusCode();
        var clientes = await response.Content.ReadAsStringAsync();
        var elemento = JsonSerializer.Deserialize<ClientesResponse<ClientesAttributes>>(clientes, new JsonSerializerOptions(){ PropertyNameCaseInsensitive = true });

        Assert.True((int)response.StatusCode == 200);
        Assert.NotNull(elemento);
    }
    
}

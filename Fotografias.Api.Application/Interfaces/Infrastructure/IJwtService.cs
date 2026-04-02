namespace Fotografias.Api.Application.Interfaces.Infrastructure;

public interface IJwtService
{
    public ValueTask<string> GenerateToken(string username);
}

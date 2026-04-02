using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Fotografias.Api.Application.Interfaces.Infrastructure;
using Fotografias.Api.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Fotografias.Api.Infrastructure;

public class JwtService : IJwtService
{
    private readonly JwtConfig _jwt;
    private readonly string _key;
    public JwtService(IOptions<JwtConfig> jwt)
    {
        _jwt = jwt.Value;
        _key = !string.IsNullOrEmpty(jwt.Value.key) ? jwt.Value.key : string.Empty;
    }

    public async ValueTask<string> GenerateToken(string username)
    {
        var claims = new[] { new Claim(ClaimTypes.Name, username)};
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwt.issuer,
            audience: _jwt.audience,
            claims: claims,
            expires: DateTime.Now.AddSeconds(30),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

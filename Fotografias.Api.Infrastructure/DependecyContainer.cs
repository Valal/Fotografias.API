using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Fotografias.Api.Application.Interfaces.Infrastructure;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Fotografias.Api.Domain;
using Microsoft.Extensions.Options;

namespace Fotografias.Api.Infrastructure;

public static class DependencyContainer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        JwtConfig? jwt = configuration.GetSection("Jwt").Get<JwtConfig>();
#pragma warning disable CS8604 // Possible null reference argument.
        var key = Encoding.UTF8.GetBytes(jwt?.key);
#pragma warning restore CS8604 // Possible null reference argument.
        services.AddAuthentication(options =>
        {
           options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
           options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
        }).AddJwtBearer(options =>
        {
           options.TokenValidationParameters = new TokenValidationParameters
           {
               ValidateIssuer = true,
               ValidateAudience = true,
               ValidateLifetime = true,
               ValidateIssuerSigningKey = true,

               ValidIssuer = jwt.issuer,
               ValidAudience = jwt.audience,

               IssuerSigningKey = new SymmetricSecurityKey(key)
           }; 
        });
        services.Configure<JwtConfig>(configuration.GetSection("Jwt"));
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IRepository, Repository>();
        return services;
    }
}

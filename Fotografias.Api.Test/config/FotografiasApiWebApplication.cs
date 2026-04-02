using Fotografias.Api.Application.Interfaces;
using Fotografias.Api.Application.Interfaces.Infrastructure;
using Fotografias.Api.Application.Presenters;
using Fotografias.Api.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Fotografias.Api.Test.Config;

public class FotografiasApiWebApplication<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(s =>
        {
            s.AddScoped<IJwtService, JwtService>();
            s.AddScoped<IClientesPresenter, ClientesPresenter>(); 
        });
    }
}

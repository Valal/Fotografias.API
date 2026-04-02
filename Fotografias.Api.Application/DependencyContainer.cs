using Microsoft.Extensions.DependencyInjection;
using Fotografias.Api.Application.Interfaces;
using Fotografias.Api.Application.Presenters;

namespace Fotografias.Api.Application;

public static class DependencyContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection service)
    {
        service.AddScoped<IClientesPresenter, ClientesPresenter>();
        return service;
    }
}

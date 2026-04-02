using Fotografias.Api.Application;
using Fotografias.Api.Application.Interfaces.Infrastructure;
using Fotografias.Api.Infrastructure.Context;
using Microsoft.Extensions.Configuration;

namespace Fotografias.Api.Infrastructure;

public class Repository : IRepository
{
    private readonly ConexionBD _conexionBD;
    public Repository(IConfiguration configuration)
    {
        string? connectionString = configuration.GetConnectionString("Default");            
        _conexionBD = new ConexionBD(connectionString);
    }

    public IClientesContext clientesContext => new ClientesContext(_conexionBD);
}

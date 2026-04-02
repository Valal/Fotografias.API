using System.Data;
using System.Reflection.Metadata;
using System.Security.Permissions;
using Dapper;
using Fotografias.Api.Application.Interfaces.Infrastructure;
using Fotografias.Api.Domain.dtos.responses;
using Microsoft.AspNetCore.Http;

namespace Fotografias.Api.Infrastructure.Context;

public class ClientesContext : IClientesContext
{
    private readonly ConexionBD _conexionBD;
    public ClientesContext(ConexionBD conexionBD)
    {
        _conexionBD = conexionBD;
    }

    public async Task<List<ClientesAttributes>> Get()
    {
        List<ClientesAttributes> lattr = new List<ClientesAttributes>();
        var atributos = await _conexionBD.GetData<ClientesAttributes>("get_clientes");
        if(atributos.Any())
        {
            foreach(ClientesAttributes at in atributos)
            {
                lattr.Add(at);       
            }
        }        
        return lattr;
    }
    public async Task<ClientesAttributes?> GetId(string id)
    {
        DynamicParameters dp = new DynamicParameters();
        dp.Add("@cliente", id, DbType.String, ParameterDirection.Input);
        var result = await _conexionBD.GetSingleData<ClientesAttributes>("get_clientes", dp);
        return result;
    }
}

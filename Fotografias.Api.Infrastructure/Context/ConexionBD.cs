using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace Fotografias.Api.Infrastructure;

public class ConexionBD
{
    private readonly string? _conexion;
    public ConexionBD(string? conexion)
    {
        _conexion = conexion;
    }

    private SqlConnection Conexion()
    {
        return new SqlConnection(_conexion);
    }

    public async Task<T?> GetSingleData<T>(string storeprocedure, DynamicParameters? parameters = null)
    {
        using(var conn = Conexion())
        {
            return await conn.QueryFirstOrDefaultAsync<T>(storeprocedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
    public async Task<List<T>> GetData<T>(string storeprocedure, DynamicParameters? parameters = null)
    {
        using(var conn = Conexion())
        {
            var result = await conn.QueryMultipleAsync(storeprocedure, parameters, commandType: CommandType.StoredProcedure);
            return (await result.ReadAsync<T>()).ToList();
        }
    }
}

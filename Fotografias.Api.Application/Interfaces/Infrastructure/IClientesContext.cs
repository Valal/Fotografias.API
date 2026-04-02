
using Fotografias.Api.Domain.dtos.responses;

namespace Fotografias.Api.Application.Interfaces.Infrastructure;

public interface IClientesContext
{
    public Task<List<ClientesAttributes>> Get();  
    public Task<ClientesAttributes?> GetId(string id); 
}
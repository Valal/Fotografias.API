using Fotografias.Api.Domain.dtos.responses;

namespace Fotografias.Api.Application.Interfaces;

public interface IClientesPresenter
{
    public ValueTask<ClientesResponse<List<ClientesAttributes>>> Get();
    public ValueTask<ClientesResponse<ClientesAttributes>> GetId(string id);
}

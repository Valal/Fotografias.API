using Fotografias.Api.Application.Interfaces.Infrastructure;
using Fotografias.Api.Application.Interfaces;
using Fotografias.Api.Domain.dtos.responses;

namespace Fotografias.Api.Application.Presenters;

public class ClientesPresenter : IClientesPresenter
{
    private readonly IRepository _repository;

    public ClientesPresenter(IRepository repository)
    {
        _repository = repository;
    }

    public async ValueTask<ClientesResponse<List<ClientesAttributes>>> Get()
    {
        var attr = await _repository.clientesContext.Get();
        
        return new ClientesResponse<List<ClientesAttributes>>(){ data = new ClientesData<List<ClientesAttributes>>(){ type = "clientes", attributes = attr }};
    }

    public async ValueTask<ClientesResponse<ClientesAttributes>> GetId(string id)
    {
        var attr = await _repository.clientesContext.GetId(id);
        return new ClientesResponse<ClientesAttributes>(){ data = new ClientesData<ClientesAttributes>(){ type = "clientes", attributes = attr }};
    }
}

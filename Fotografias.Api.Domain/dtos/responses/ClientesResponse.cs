namespace Fotografias.Api.Domain.dtos.responses;

public class ClientesAttributes
{
    public int code {get;set;}
    public string? Nombre { get; set; }
    public string? Telefono { get; set; }
    public string? Correo { get; set; }
}

public class ClientesData<T>
{
    public string? type {get;set;}
    public T? attributes {get; set;}
}

public class ClientesResponse<T>
{
    public ClientesData<T>? data {get; set; }
}

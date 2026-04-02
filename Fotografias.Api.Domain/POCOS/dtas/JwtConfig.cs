namespace Fotografias.Api.Domain;

public class JwtConfig
{
    public string? key {get;set;}
    public string? issuer {get;set;}
    public string? audience {get;set;}
    public string? subject {get;set;}
}

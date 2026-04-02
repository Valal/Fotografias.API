namespace Fotografias.Api.Application.Interfaces;

public interface IFilesAdmin
{
    public ValueTask<byte[]> Upload(Stream file, string name);
}

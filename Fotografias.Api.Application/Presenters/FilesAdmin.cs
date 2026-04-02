using Fotografias.Api.Application.Interfaces;

namespace Fotografias.Api.Application;

public class FilesAdmin : IFilesAdmin
{
    public FilesAdmin(){}
    public async ValueTask<byte[]> Upload(Stream file, string name)
    {
        using(var m = new MemoryStream())
        {
            await m.CopyToAsync(file);
            return m.ToArray();
        }
    }
}

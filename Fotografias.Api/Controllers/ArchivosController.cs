using System.ComponentModel.DataAnnotations;
using Fotografias.Api.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApp.Namespace
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArchivosController : ControllerBase
    {
        private readonly IFilesAdmin _file;

        public ArchivosController(IFilesAdmin fileAdmin)
        {
            _file = fileAdmin;
        }

        [HttpPost]
        [RequestSizeLimit(5*1024*1024)]
        public async Task<FileContentResult> Post(IFormFile file)
        {
            var result = await _file.Upload(file.OpenReadStream(), file.FileName);
            return File(result, "application/pdf", "resultpdf");
        }

    }
}

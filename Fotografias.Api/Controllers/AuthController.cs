using Fotografias.Api.Application.Interfaces.Infrastructure;
using Fotografias.Api.Domain;
using Fotografias.Api.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace Fotografias.Api.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwt)
        {
            _jwtService = jwt;
        }

        [HttpPost("token")]
        [Consumes("application/x-www-form-urlencoded")]
        public async Task<ActionResult<string>> Token(
            [FromForm(Name = "client_id")] string clientId, 
            [FromForm(Name = "client_secret")] string secret,
            [FromForm(Name = "scoped")] string scope,
            [FromForm(Name = "client_credentials")] string clientcredentials
        )
        {
            if(clientId == "f47ac10b-58cc-4372-a567-0e02b2c3d479" && secret == "Y2xhdmVfc3VwZXJfc2VjcmV0YV9tdXlfc2VndXJhXzEyMzQ1Njc4OTA=")
            {
                var token = await _jwtService.GenerateToken(clientId);
                return Created(Request.Scheme+"://"+Request.Host+Request.Path, new {token});
            }
            return Unauthorized();
        }
    }
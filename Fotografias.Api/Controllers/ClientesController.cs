using Fotografias.Api.Domain.dtos.responses;
using Fotografias.Api.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace Fotografias.Api.Controllers;
    
    [Authorize]
    [Route("api/[controller]")]
    [EnableRateLimiting("fixed")]
    
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesPresenter _clientes;
        public ClientesController(IClientesPresenter clientes)
        {
            _clientes = clientes;   
        }

        [HttpGet]
        public async Task<ActionResult<ClientesResponse<List<ClientesAttributes>>>> Get(){
            var result = await _clientes.Get();
            return  result?.data?.attributes != null && result.data.attributes.Any() ? Ok(result) : 
                BadRequest(new { mensaje = "no existen clientes disponibles."}); 
        }

        [HttpGet("cliente/{id}")]
        public async Task<ActionResult<ClientesResponse<ClientesAttributes>>> GetId(string id)
        {
            var result = await _clientes.GetId(id);
            return result?.data != null ? Ok(result) 
            : BadRequest(new { mensaje = "No existe el cliente consultado."});   
        }
    }
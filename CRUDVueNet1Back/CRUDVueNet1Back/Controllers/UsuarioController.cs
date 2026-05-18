using CRUDVueNet1Back.Context;
using CRUDVueNet1Back.DTOs;
using CRUDVueNet1Back.Services.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDVueNet1Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        [Route("Login")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> Login([FromBody] UsuarioLoginDTO request)
        {
            var _respuesta = await _usuarioService.Login(request);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }

    }
}

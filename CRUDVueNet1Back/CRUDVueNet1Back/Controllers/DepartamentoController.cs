using CRUDVueNet1Back.Services.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDVueNet1Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentoController : ControllerBase
    {
        private readonly IDepartamentoService _departamentoService;

        public DepartamentoController(IDepartamentoService departamentoService)
        {
            _departamentoService = departamentoService;
        }

        [HttpGet]
        [Route("ListaDepartamentos")]
        public async Task<ActionResult> GetDepartamentos()
        {
            var _respuesta = await _departamentoService.ListarDeps();
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }
    }
}

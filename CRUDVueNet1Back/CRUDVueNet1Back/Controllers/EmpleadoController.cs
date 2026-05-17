
using CRUDVueNet1Back.Services.Contratos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUDVueNet1Back.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;

        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
        }

        [HttpGet]
        [Route("ListaEmpleados")]
        public async Task<ActionResult> GetEmpleados(int id)
        {
            var _respuesta = await _empleadoService.ListarEmps(id);
            return StatusCode((int)_respuesta.StatusCode,_respuesta);
        }
        [HttpGet]
        [Route("ObtenerEmpleado")]
        public async Task<ActionResult> GetEmpleado(int id)
        {
            var _respuesta = await _empleadoService.ObtenerEmp(id);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }

    }
}

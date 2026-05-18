
using CRUDVueNet1Back.DTOs;
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
        public async Task<ActionResult> GetEmpleados([FromQuery] int idDep=0)
        {
            var _respuesta = await _empleadoService.ListarEmps(idDep);
            return StatusCode((int)_respuesta.StatusCode,_respuesta);
        }
        [HttpGet]
        [Route("ObtenerEmpleado/{idEmp}")]
        public async Task<ActionResult> GetEmpleado(int idEmp)
        {
            var _respuesta = await _empleadoService.ObtenerEmp(idEmp);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }
        [HttpPost]
        [Route("GuardarEmpleado")]
        public async Task<ActionResult> PostEmpleado([FromBody] EmpleadoDTO empdto)
        {
            var _respuesta = await _empleadoService.GuardarEmp(empdto);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }
        [HttpPut]
        [Route("EditarEmpleado")]
        public async Task<ActionResult> PutEmpleado([FromBody] EmpleadoDTO empdto)
        {
            var _respuesta = await _empleadoService.EditarEmp(empdto);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }
        [HttpDelete]
        [Route("EliminarEmpleado/{idEmp}")]
        public async Task<ActionResult> DeleteEmpleado(int idEmp)
        {
            var _respuesta = await _empleadoService.EliminarEmp(idEmp);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }
        [HttpPut]
        [Route("ActualizarEstadoEmpleado/{idEmp}")]
        public async Task<ActionResult> UpdateEstadoEmpleado(int idEmp)
        {
            var _respuesta = await _empleadoService.EstadoEmp(idEmp);
            return StatusCode((int)_respuesta.StatusCode, _respuesta);
        }

    }
}

using CRUDVueNet1Back.Context;
using CRUDVueNet1Back.DTOs;
using CRUDVueNet1Back.Models;
using CRUDVueNet1Back.Services.Contratos;
using CRUDVueNet1Back.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CRUDVueNet1Back.Services.Implementations
{
    public class EmpleadoServiceImpl : IEmpleadoService
    {
        private readonly CrudvueNet1Context _contexto;

        public EmpleadoServiceImpl(CrudvueNet1Context contexto)
        {
            _contexto = contexto;
        }

        public async Task<ApiResponse<List<EmpleadoDTO>>> ListarEmps(int idDep)
        {
            var _response = new ApiResponse<List<EmpleadoDTO>>();
            try
            {
                List<Empleado> _listaEmpleadosBD;
                //string nombreDepartamentoFijo = "Sin Depto"; forma 2
                if (idDep == 0)
                {
                    _listaEmpleadosBD = await _contexto.Empleados
                        .AsNoTracking()
                        .Include(d => d.IdDepartamentoNavigation)
                        .OrderByDescending(a => a.Activo).ToListAsync();
                }
                else
                {
                    _listaEmpleadosBD = await _contexto.Empleados.FromSqlRaw("EXEC sp_ListarEmpleadosPorIdDepartamento {0}", idDep).ToListAsync();
                    var idDetptos = _listaEmpleadosBD.Select(e => e.IdDepartamento).Distinct().ToList();
                    await _contexto.Departamentos.Where(d => idDetptos.Contains(d.IdDepartamento)).LoadAsync();
                    // 2. Buscamos rápido el nombre de ese único departamento en SQL
                    //var depto = await _context.Departamentos.FindAsync(idDep);
                    //if (depto != null)
                    //{
                    //    nombreDepartamentoFijo = depto.NombreDepartamento;
                    //}
                }
                var _listaEmpleadosDTO = _listaEmpleadosBD.Select(emp => new EmpleadoDTO
                {
                    IdEmpleado = emp.IdEmpleado,
                    NombreEmpleado = emp.NombreEmpleado,
                    IdDepartamento = emp.IdDepartamento,
                    NombreDepartamento = emp.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
                    Activo = emp.Activo,
                }).ToList();
                // Si es idDep == 0 usa el Include, si es un SP usa el nombre fijo que encontramos
                //    NombreDepartamento = idDep == 0
                //? (emp.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto")
                //: nombreDepartamentoFijo
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _listaEmpleadosDTO;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Error al cargar empleados: " + ex.Message);
            }
            return _response;
        }

        public async Task<ApiResponse<EmpleadoDTO>> ObtenerEmp(int idEmp)
        {
            var _response  = new ApiResponse<EmpleadoDTO>();
            try
            {
                var _empleadoBD = await _contexto.Empleados
                    .Include(d => d.IdDepartamentoNavigation)
                    .FirstOrDefaultAsync(e => e.IdEmpleado == idEmp);
                if (_empleadoBD == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _response.ErrorMessage.Add($"No se encontro el usuario con id {idEmp}");
                    return _response;
                }
                var _empleadoDTO = new EmpleadoDTO
                {
                    IdEmpleado = idEmp,
                    NombreEmpleado = _empleadoBD?.NombreEmpleado ?? string.Empty,
                    IdDepartamento = _empleadoBD?.IdDepartamento ?? 0,
                    NombreDepartamento = _empleadoBD?.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
                    Activo = _empleadoBD?.Activo ?? true
                };
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = _empleadoDTO;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage.Add("Error al cargar empleado: " + ex.Message);
            }
            return _response;
        }
        public Task<ApiResponse<EmpleadoDTO>> GuardarEmp(EmpleadoDTO empdto)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResponse<EmpleadoDTO>> EditarEmp(EmpleadoDTO empdto)
        {
            throw new NotImplementedException();
        }
        public Task<ApiResponse<bool>> EliminarEmp(int idEmp)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse<bool>> EstadoEmp(int idEmp)
        {
            throw new NotImplementedException();
        }

    }
}

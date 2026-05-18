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
        #region
        //public async Task<ApiResponse<List<EmpleadoDTO>>> ListarEmps(int idDep)
        //{
        //    var _response = new ApiResponse<List<EmpleadoDTO>>();
        //    try
        //    {
        //        List<Empleado> _listaEmpleadosBD;
        //        //string nombreDepartamentoFijo = "Sin Depto"; forma 2
        //        if (idDep == 0)
        //        {
        //            _listaEmpleadosBD = await _contexto.Empleados
        //                .AsNoTracking()
        //                .Include(d => d.IdDepartamentoNavigation)
        //                .OrderByDescending(a => a.Activo).ToListAsync();
        //        }
        //        else
        //        {
        //            _listaEmpleadosBD = await _contexto.Empleados.FromSqlRaw("EXEC sp_ListarEmpleadosPorIdDepartamento {0}", idDep).ToListAsync();
        //            var idDetptos = _listaEmpleadosBD.Select(e => e.IdDepartamento).Distinct().ToList();
        //            await _contexto.Departamentos.Where(d => idDetptos.Contains(d.IdDepartamento)).LoadAsync();
        //            // 2. Buscamos rápido el nombre de ese único departamento en SQL
        //            //var depto = await _context.Departamentos.FindAsync(idDep);
        //            //if (depto != null)
        //            //{
        //            //    nombreDepartamentoFijo = depto.NombreDepartamento;
        //            //}
        //        }
        //        var _listaEmpleadosDTO = _listaEmpleadosBD.Select(emp => new EmpleadoDTO
        //        {
        //            IdEmpleado = emp.IdEmpleado,
        //            NombreEmpleado = emp.NombreEmpleado,
        //            IdDepartamento = emp.IdDepartamento,
        //            NombreDepartamento = emp.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
        //            Activo = emp.Activo,
        //        }).ToList();
        //        // Si es idDep == 0 usa el Include, si es un SP usa el nombre fijo que encontramos
        //        //    NombreDepartamento = idDep == 0
        //        //? (emp.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto")
        //        //: nombreDepartamentoFijo
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.IsSuccess = true;
        //        _response.Result = _listaEmpleadosDTO;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessage.Add("Error al cargar empleados: " + ex.Message);
        //    }
        //    return _response;
        //}
        //public async Task<ApiResponse<EmpleadoDTO>> ObtenerEmp(int idEmp)
        //{
        //    var _response  = new ApiResponse<EmpleadoDTO>();
        //    try
        //    {
        //        var _empleadoBD = await _contexto.Empleados
        //            .Include(d => d.IdDepartamentoNavigation)
        //            .FirstOrDefaultAsync(e => e.IdEmpleado == idEmp);
        //        if (_empleadoBD == null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode=HttpStatusCode.NotFound;
        //            _response.ErrorMessage.Add($"No se encontro el usuario con id {idEmp}");
        //            return _response;
        //        }
        //        var _empleadoDTO = new EmpleadoDTO
        //        {
        //            IdEmpleado = idEmp,
        //            NombreEmpleado = _empleadoBD?.NombreEmpleado ?? string.Empty,
        //            IdDepartamento = _empleadoBD?.IdDepartamento ?? 0,
        //            NombreDepartamento = _empleadoBD?.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
        //            Activo = _empleadoBD?.Activo ?? true
        //        };
        //        _response.IsSuccess = true;
        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.Result = _empleadoDTO;

        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.ErrorMessage.Add("Error al cargar empleado: " + ex.Message);
        //    }
        //    return _response;
        //}
        //public async Task<ApiResponse<EmpleadoDTO>> GuardarEmp(EmpleadoDTO empdto)
        //{
        //    var _response = new ApiResponse<EmpleadoDTO>();
        //    try
        //    {
        //        var _empleadoBD = new Empleado
        //        {
        //            NombreEmpleado = empdto.NombreEmpleado,
        //            IdDepartamento = empdto.IdDepartamento,
        //            Activo = true
        //        };
        //        await _contexto.Empleados.AddAsync(_empleadoBD);
        //        await _contexto.SaveChangesAsync();
        //        var empleadoRecienGuardado = await _contexto.Empleados
        //            .Include(e => e.IdDepartamentoNavigation)
        //            .FirstOrDefaultAsync(e => e.IdEmpleado == _empleadoBD.IdEmpleado);
        //        var dtoRespuesta = new EmpleadoDTO
        //        {
        //            IdEmpleado = empleadoRecienGuardado!.IdEmpleado,
        //            NombreEmpleado = empleadoRecienGuardado.NombreEmpleado,
        //            IdDepartamento = empleadoRecienGuardado.IdDepartamento,
        //            NombreDepartamento = empleadoRecienGuardado.IdDepartamentoNavigation.NombreDepartamento,
        //            Activo = empleadoRecienGuardado.Activo
        //        };

        //        _response.StatusCode = HttpStatusCode.OK;
        //        _response.IsSuccess = true;
        //        _response.Result = dtoRespuesta;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessage.Add("Error al guardar empleado: " + ex.Message);
        //    }
        //    return _response;
        //}
        //public async Task<ApiResponse<EmpleadoDTO>> EditarEmp(EmpleadoDTO empdto)
        //{
        //    var _response  = new ApiResponse<EmpleadoDTO>();
        //    try
        //    {
        //        var _empleadoBD = await _contexto.Empleados.FindAsync(empdto.IdEmpleado);
        //        if (_empleadoBD == null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.ErrorMessage.Add($"No se encontro el empleado con id {empdto.IdEmpleado}");
        //            return _response;
        //        }
        //        _empleadoBD.NombreEmpleado = empdto.NombreEmpleado;
        //        _empleadoBD.IdDepartamento = empdto.IdDepartamento;

        //        await _contexto.SaveChangesAsync();

        //        var empActualizado = await _contexto.Empleados
        //            .Include(d => d.IdDepartamentoNavigation)
        //            .FirstOrDefaultAsync(e => e.IdEmpleado == _empleadoBD.IdEmpleado);
        //        if (empActualizado == null)
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.ErrorMessage.Add("Error al recuperar los datos actualizados del empleado.");
        //            return _response;
        //        }
        //        var dtoRespuesta = new EmpleadoDTO
        //        {
        //            IdEmpleado = empActualizado.IdEmpleado,
        //            NombreEmpleado = empActualizado.NombreEmpleado,
        //            IdDepartamento = empActualizado.IdDepartamento,
        //            NombreDepartamento = empActualizado.IdDepartamentoNavigation.NombreDepartamento,
        //            Activo = empActualizado.Activo
        //        };
        //        _response.IsSuccess = true;
        //        _response.StatusCode=HttpStatusCode.OK;
        //        _response.Result = dtoRespuesta;

        //    }catch (Exception ex)
        //    {
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessage.Add("Error al editar empleado: " + ex.Message);
        //    }
        //    return _response;
        //}
        //public async Task<ApiResponse<bool>> EliminarEmp(int idEmp)
        //{
        //    var _response = new ApiResponse<bool>();
        //    try
        //    {
        //        var _empleadoBD = await _contexto.Empleados.FindAsync(idEmp);
        //        if( _empleadoBD == null )
        //        {
        //            _response.IsSuccess = false;
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            _response.ErrorMessage.Add($"Error al eliminar al empleado con id {idEmp}");
        //            return _response;
        //        }
        //        else
        //        {
        //            _contexto.Empleados.Remove(_empleadoBD);
        //            await _contexto.SaveChangesAsync();

        //            _response.IsSuccess = true;
        //            _response.StatusCode = HttpStatusCode.OK;
        //            _response.Result = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.Result=false;
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessage.Add("Error al eliminar empleado: " + ex.Message);
        //    }
        //    return _response;
        //}
        //public async Task<ApiResponse<bool>> EstadoEmp(int idEmp)
        //{
        //    var _response = new ApiResponse<bool>();
        //    try
        //    {
        //    var _estadoEmp = await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_EstadoEmpleado {0}", idEmp);

        //    }catch (Exception ex)
        //    {
        //        _response.Result = false;
        //        _response.StatusCode = HttpStatusCode.InternalServerError;
        //        _response.IsSuccess = false;
        //        _response.ErrorMessage.Add("Error al eliminar empleado: " + ex.Message);
        //    }
        //    return _response;
        //}
        #endregion

        public async Task<ApiResponse<List<EmpleadoDTO>>> ListarEmps(int idDep)
        {
            var _response = new ApiResponse<List<EmpleadoDTO>>();
            try
            {
                List<Empleado> _listaEmpleadosBD;
                if (idDep == 0)
                {
                    _listaEmpleadosBD = await _contexto.Empleados
                        .AsNoTracking()
                        .Include(d => d.IdDepartamentoNavigation)
                        .OrderByDescending(a => a.Activo)
                        .ToListAsync();
                }
                else
                {
                    _listaEmpleadosBD = await _contexto.Empleados
                        .FromSqlRaw("EXEC sp_ListarEmpleadosPorIdDepartamento {0}", idDep)
                        .AsNoTracking()
                        .ToListAsync();

                    var idDetptos = _listaEmpleadosBD.Select(e => e.IdDepartamento).Distinct().ToList();
                    await _contexto.Departamentos.Where(d => idDetptos.Contains(d.IdDepartamento)).LoadAsync();
                }

                var _listaEmpleadosDTO = _listaEmpleadosBD.Select(emp => new EmpleadoDTO
                {
                    IdEmpleado = emp.IdEmpleado,
                    NombreEmpleado = emp.NombreEmpleado,
                    IdDepartamento = emp.IdDepartamento,
                    NombreDepartamento = emp.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
                    Activo = emp.Activo,
                }).ToList();

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
            var _response = new ApiResponse<EmpleadoDTO>();
            try
            {
                var _empleadoBD = await _contexto.Empleados
                    .Include(d => d.IdDepartamentoNavigation)
                    .FirstOrDefaultAsync(e => e.IdEmpleado == idEmp);

                if (_empleadoBD == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage.Add($"No se encontró el empleado con id {idEmp}");
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

        public async Task<ApiResponse<EmpleadoDTO>> GuardarEmp(EmpleadoDTO empdto)
        {
            var _response = new ApiResponse<EmpleadoDTO>();
            try
            {
                var _empleadoBD = new Empleado
                {
                    NombreEmpleado = empdto.NombreEmpleado,
                    IdDepartamento = empdto.IdDepartamento,
                    Activo = true
                };

                await _contexto.Empleados.AddAsync(_empleadoBD);
                await _contexto.SaveChangesAsync();

                var empleadoRecienGuardado = await _contexto.Empleados
                    .Include(e => e.IdDepartamentoNavigation)
                    .FirstOrDefaultAsync(e => e.IdEmpleado == _empleadoBD.IdEmpleado);

                var dtoRespuesta = new EmpleadoDTO
                {
                    IdEmpleado = empleadoRecienGuardado!.IdEmpleado,
                    NombreEmpleado = empleadoRecienGuardado.NombreEmpleado,
                    IdDepartamento = empleadoRecienGuardado.IdDepartamento,
                    // ✨ Corregido con operador seguro ?. para evitar caídas
                    NombreDepartamento = empleadoRecienGuardado.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
                    Activo = empleadoRecienGuardado.Activo
                };

                _response.StatusCode = HttpStatusCode.Created; // Cambiado a Created (201) que es el estándar REST correcto para inserciones
                _response.IsSuccess = true;
                _response.Result = dtoRespuesta;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Error al guardar empleado: " + ex.Message);
            }
            return _response;
        }

        public async Task<ApiResponse<EmpleadoDTO>> EditarEmp(EmpleadoDTO empdto)
        {
            var _response = new ApiResponse<EmpleadoDTO>();
            try
            {
                var _empleadoBD = await _contexto.Empleados.FindAsync(empdto.IdEmpleado);
                if (_empleadoBD == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage.Add($"No se encontró el empleado con id {empdto.IdEmpleado}");
                    return _response;
                }

                _empleadoBD.NombreEmpleado = empdto.NombreEmpleado;
                _empleadoBD.IdDepartamento = empdto.IdDepartamento;

                await _contexto.SaveChangesAsync();

                var empActualizado = await _contexto.Empleados
                    .Include(d => d.IdDepartamentoNavigation)
                    .FirstOrDefaultAsync(e => e.IdEmpleado == _empleadoBD.IdEmpleado);

                if (empActualizado == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage.Add("Error al recuperar los datos actualizados del empleado.");
                    return _response;
                }

                var dtoRespuesta = new EmpleadoDTO
                {
                    IdEmpleado = empActualizado.IdEmpleado,
                    NombreEmpleado = empActualizado.NombreEmpleado,
                    IdDepartamento = empActualizado.IdDepartamento,
                    // ✨ Corregido con operador seguro ?.
                    NombreDepartamento = empActualizado.IdDepartamentoNavigation?.NombreDepartamento ?? "Sin Depto",
                    Activo = empActualizado.Activo
                };

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = dtoRespuesta;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Error al editar empleado: " + ex.Message);
            }
            return _response;
        }

        public async Task<ApiResponse<bool>> EliminarEmp(int idEmp)
        {
            var _response = new ApiResponse<bool>();
            try
            {
                var _empleadoBD = await _contexto.Empleados.FindAsync(idEmp);
                if (_empleadoBD == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    // Corregido el mensaje para que sea semánticamente un NotFound común
                    _response.ErrorMessage.Add($"No se encontró el empleado con id {idEmp}");
                    return _response;
                }

                _contexto.Empleados.Remove(_empleadoBD);
                await _contexto.SaveChangesAsync();

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Result = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Error al eliminar empleado: " + ex.Message);
            }
            return _response;
        }

        public async Task<ApiResponse<bool>> EstadoEmp(int idEmp)
        {
            var _response = new ApiResponse<bool>();
            try
            {
                // ExecuteSqlRawAsync nos da las filas afectadas en la base de datos
                var filasAfectadas = await _contexto.Database.ExecuteSqlRawAsync("EXEC sp_EstadoEmpleado {0}", idEmp);

                // ✨ Si afectó 0 filas, significa que el ID no existía en SQL Server
                if (filasAfectadas == 0)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.NotFound;
                    _response.ErrorMessage.Add($"No se encontró ningún empleado con el id {idEmp} para cambiar su estado.");
                    _response.Result = false;
                    return _response;
                }

                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                _response.Result = true;
            }
            catch (Exception ex)
            {
                _response.Result = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                // ✨ Corregido el mensaje del catch para que hable del Estado, no de Eliminar
                _response.ErrorMessage.Add("Error al cambiar el estado del empleado: " + ex.Message);
            }
            return _response;
        }

    }
}

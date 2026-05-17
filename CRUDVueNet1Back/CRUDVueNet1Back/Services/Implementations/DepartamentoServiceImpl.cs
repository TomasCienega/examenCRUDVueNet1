using Azure;
using CRUDVueNet1Back.Context;
using CRUDVueNet1Back.DTOs;
using CRUDVueNet1Back.Services.Contratos;
using CRUDVueNet1Back.Utilities;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace CRUDVueNet1Back.Services.Implementations
{
    public class DepartamentoServiceImpl : IDepartamentoService
    {
        private readonly CrudvueNet1Context _context;

        public DepartamentoServiceImpl(CrudvueNet1Context context)
        {
            _context = context;
        }

        public async Task<ApiResponse<List<DepartamentoDTO>>> ListarDeps()
        {
            var _response = new ApiResponse<List<DepartamentoDTO>>();
            try
            {
                var _listaBd = await _context.Departamentos.AsNoTracking().ToListAsync();

                var _listaDTO = _listaBd.Select(dep => new DepartamentoDTO
                {
                    IdDepartamento = dep.IdDepartamento,
                    NombreDepartamento = dep.NombreDepartamento,
                }).ToList();
                _response.Result = _listaDTO;
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Error al cargar departamentos: " + ex.Message);
            }
            return _response;
        }
    }
}

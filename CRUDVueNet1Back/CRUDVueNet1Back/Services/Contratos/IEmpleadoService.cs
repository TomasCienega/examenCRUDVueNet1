using CRUDVueNet1Back.DTOs;
using CRUDVueNet1Back.Utilities;

namespace CRUDVueNet1Back.Services.Contratos
{
    public interface IEmpleadoService
    {
        Task<ApiResponse<List<EmpleadoDTO>>> ListarEmps(int idDep);
        Task<ApiResponse<EmpleadoDTO>> ObtenerEmp(int idEmp);

        Task<ApiResponse<EmpleadoDTO>> GuardarEmp(EmpleadoDTO empdto);

        Task<ApiResponse<EmpleadoDTO>> EditarEmp(EmpleadoDTO empdto);

        Task<ApiResponse<bool>> EliminarEmp(int idEmp);

        Task<ApiResponse<bool>> EstadoEmp(int idEmp);


    }
}

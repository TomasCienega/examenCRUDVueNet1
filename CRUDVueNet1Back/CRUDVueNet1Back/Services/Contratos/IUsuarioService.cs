using CRUDVueNet1Back.DTOs;
using CRUDVueNet1Back.Utilities;

namespace CRUDVueNet1Back.Services.Contratos
{
    public interface IUsuarioService
    {
        Task<ApiResponse<UsuarioLoginRespuestaDTO>> Login(UsuarioLoginDTO request);
    }
}

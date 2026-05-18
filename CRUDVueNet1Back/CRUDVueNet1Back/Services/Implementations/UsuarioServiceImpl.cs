using CRUDVueNet1Back.Context;
using CRUDVueNet1Back.DTOs;
using CRUDVueNet1Back.Services.Contratos;
using CRUDVueNet1Back.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace CRUDVueNet1Back.Services.Implementations
{
    public class UsuarioServiceImpl : IUsuarioService
    {
        private readonly CrudvueNet1Context _contexto;
        private readonly string _secretKey;

        public UsuarioServiceImpl(CrudvueNet1Context contexto, IConfiguration config)
        {
            _contexto = contexto;
            _secretKey = config.GetSection("Jwt").GetValue<string>("Key")!;
        }

        public async Task<ApiResponse<UsuarioLoginRespuestaDTO>> Login(UsuarioLoginDTO request)
        {
            var _response = new ApiResponse<UsuarioLoginRespuestaDTO>();
            try
            {
                // 1. Encriptamos la clave que escribió el usuario para compararla
                string claveHashed = ConvertirSHA256.Encriptar(request.Clave);

                // 2. Buscamos en la base de datos
                var usuario = await _contexto.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == request.Correo && u.Clave == claveHashed);

                // 🚨 AQUÍ ESTÁ LA ADAPTACIÓN CORRECTA:
                if (usuario == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.Unauthorized; // 401 
                    _response.ErrorMessage.Add("Correo o contraseña incorrectos.");
                    return _response; // Retornamos el objeto de respuesta controlado
                }

                // 3. Si existe, creamos el Token JWT usando tu SecretKey
                var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Correo));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddHours(8),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(keyBytes),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);
                string tokenString = tokenHandler.WriteToken(tokenConfig);

                // 4. Llenamos el DTO de éxito
                var resultado = new UsuarioLoginRespuestaDTO{
                    Token = tokenString
                };

                _response.Result = resultado;
                _response.StatusCode = HttpStatusCode.OK; // 200
                _response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.ErrorMessage.Add("Error en el proceso de autenticación: " + ex.Message);
            }

            return _response;
        }
    }
}

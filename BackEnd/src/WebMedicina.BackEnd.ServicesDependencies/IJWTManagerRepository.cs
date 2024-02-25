using System.Security.Claims;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IJWTManagerRepository {

        /// <summary>
        /// Generar jwt token y refresh token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Devuelve null si la informacion del usuario no es valida</returns>
        /// <exception cref="InvalidOperationException"></exception>
        Tokens? GenerateJWTTokens(UserInfoDto userInfo);

        /// <summary>
        /// Obtener los claims de token expirado
        /// </summary>
        /// <param name="token"></param>
        /// <returns>ClaimsPrincipal del token</returns>
        /// <exception cref="SecurityTokenException"></exception>
        ClaimsPrincipal GetClaimsFromExpiredToken(string token);

        /// <summary>
        ///  Añadir nuevo refresh token para un usuario, y eliminar los antigüos
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <param name="idMedico"></param>
        /// <returns>Refresh token creado</returns>
        bool AddRefreshToken(string refreshToken, int idMedico);

        /// <summary>
        /// Eliminar refresh tokende un usuario
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="refreshToken"></param>
        /// <returns>Refresh token eliminado</returns>
        bool DeleteRefreshTokens(int idMedico, string refreshToken);


        /// <summary>
        /// Obtener el refresh token de un usuario y eliminar si está caducado
        /// </summary>
        /// <param name="idMedico"></param>
        /// <param name="refreshToken"></param>
        /// <returns>Refresh token de un usuario</returns>
        UserRefreshTokens? ObtenerRefreshToken(int idMedico, string refreshToken);
    }
}

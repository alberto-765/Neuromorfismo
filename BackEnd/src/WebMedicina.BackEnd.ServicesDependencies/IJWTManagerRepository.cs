using System.Security.Claims;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IJWTManagerRepository {
        Tokens? GenerateJWTTokens(UserInfoDto userInfo);
        ClaimsPrincipal GetClaimsFromExpiredToken(string token);

        // Refresh Token
        bool AddRefreshToken(string refreshToken, int idMedico);
        bool DeleteRefreshTokens(int idMedico, string refreshToken);
        UserRefreshTokens? ObtenerRefreshToken(int idMedico, string refreshToken);
    }
}

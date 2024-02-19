using IdentityModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.Service {
    public class JWTManagerRepository : IJWTManagerRepository {
        private readonly IConfiguration _iconfiguration;
        private readonly TokensDal _tokensDal;
        private readonly JWTConfig _jwtConfig;

        public JWTManagerRepository(IConfiguration iconfiguration, TokensDal tokensDal, IOptionsSnapshot<JWTConfig> jwtConfig) {
            _iconfiguration = iconfiguration;
            _tokensDal = tokensDal;
            _jwtConfig = jwtConfig.Value;
        }


        /// <summary>
        /// Generar jwt token y refresh token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Devuelve null si la informacion del usuario no es valida</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Tokens? GenerateJWTTokens(UserInfoDto userInfo) {
            if (userInfo.IdMedico == 0 || string.IsNullOrWhiteSpace(userInfo.UserLogin) || string.IsNullOrWhiteSpace(userInfo.Nombre) || string.IsNullOrWhiteSpace(userInfo.Apellidos) || string.IsNullOrWhiteSpace(userInfo.Rol)) {
                return null;
            }

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userInfo.IdMedico.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), //IDENTIFICADOR
                new Claim("UserLogin", userInfo.UserLogin),
                new Claim(ClaimTypes.Name, userInfo.Nombre),
                new Claim(ClaimTypes.Surname, userInfo.Apellidos),
                new Claim(ClaimTypes.Role, userInfo.Rol),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_iconfiguration.GetSection("JWT")["key"] ?? throw new InvalidOperationException("No se ha encontrado la key para crear el token.")));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.Now.AddHours(double.TryParse(_jwtConfig.ValidezTokenEnHoras, out double horas) ? horas : 6);

            JwtSecurityToken token = new(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            // Generamos refresh token y añadimos a BD
            string refreshToken = GenerateRefreshToken();
            if(!AddRefreshToken(refreshToken, userInfo.IdMedico)) {
                return null;
            }

            return new Tokens (new JwtSecurityTokenHandler().WriteToken(token), refreshToken);
        }

        // Generar refresh token
        private string GenerateRefreshToken() {
            var randomNumber = new byte[32];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Obtener los claims de token expirado
        /// </summary>
        /// <param name="token"></param>
        /// <returns>ClaimsPrincipal del token</returns>
        /// <exception cref="SecurityTokenException"></exception>
        public ClaimsPrincipal GetClaimsFromExpiredToken(string token) {
            // Definimos las validaciones del token
            TokenValidationParameters tokenValidationParameters = new()  {
                ValidateIssuer = true,
                ValidateAudience = true,
                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role,
                ValidateLifetime = false, // no validamos lifeTime 
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtConfig.Issuer,
                ValidAudience = _jwtConfig.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Key)),
            };

            // Validamos token, obtenemos claims principal y token convertido si es valido
            JwtSecurityTokenHandler tokenHandler = new();
            ClaimsPrincipal claimsUsuario = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);

            // Si el token es valido devolverá un SecurityToken

            // Validamos si el algoritmo del token es HmacSha256
            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                throw new SecurityTokenException("Invalid token");
            }

            return claimsUsuario;
        }


        /// <summary>
        ///  Añadir nuevo refresh token para un usuario, y eliminar los antigüos
        /// </summary>
        /// <param name="token"></param>
        /// <param name="netUserId"></param>
        /// <returns>Refresh token creado</returns>
        public bool AddRefreshToken(string refreshToken, int idMedico) {
            UserRefreshTokens userRefreshToken = new() {
                IdMedico = idMedico,
                RefreshToken = refreshToken,
                FechaExpiracion = DateTime.UtcNow.AddDays(double.TryParse(_jwtConfig.ValidezRefreshTokenEnDias, out double dias) ? dias : 6)
            };

            // Eliminamos los refreshtokens antes de añadir el nuevo
            _tokensDal.DeleteRefreshTokens(idMedico);

            return _tokensDal.AddRefreshToken(userRefreshToken);
        }

        /// <summary>
        /// Eliminar refresh tokende un usuario
        /// </summary>
        /// <param name="netUserId"></param>
        /// <param name="refreshToken"></param>
        /// <returns>Refresh token eliminado</returns>
        public bool DeleteRefreshTokens(int idMedico, string refreshToken) {
            return _tokensDal.DeleteRefreshTokens(idMedico, refreshToken);
        }

        /// <summary>
        /// Obtener el refresh token de un usuario y eliminar si está caducado
        /// </summary>
        /// <param name="netUserId"></param>
        /// <param name="refreshToken"></param>
        /// <returns>Refresh token de un usuario</returns>
        public UserRefreshTokens? ObtenerRefreshToken(int idMedico, string refreshToken) {
            UserRefreshTokens? userRefreshtoken = _tokensDal.GetRefreshToken(idMedico, refreshToken);

            // Eliminamos el refreshToken si ya ha caducado y devolvemos null
            if (userRefreshtoken is not null && userRefreshtoken.FechaExpiracion < DateTime.UtcNow) {
                DeleteRefreshTokens(idMedico, refreshToken);
                userRefreshtoken = null;
            }

            return userRefreshtoken;
        }
    }
}

using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Usuarios;
using Microsoft.AspNetCore.Identity;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using Duende.IdentityServer.Models;

namespace WebMedicina.BackEnd.Service {
    public class UserAccountService : IUserAccountService  {
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;
        private readonly IJWTManagerRepository _jwtManager;
        private readonly WebmedicinaContext _context;


        public UserAccountService(IIdentityService identityService, WebmedicinaContext context, IAdminsService adminService, IJWTManagerRepository jwtManager) {
            _adminService = adminService;
            _identityService = identityService;
            _context = context;
            _jwtManager = jwtManager;
        }


        public async Task<EstadoCrearUsuario> CrearUsuarioYMedico(UserRegistroDto model) {
            EstadoCrearUsuario estadoCreacion = EstadoCrearUsuario.IdentityUserKO;
            
            using var transaction = _context.Database.BeginTransaction();
            try {

                var user = new IdentityUser {
                    UserName = model.UserLogin
                };

                // Creamos user con identity
                if (await _identityService.CrearUser(user, model)) {

                    // Actualizamos nuevo estado de la creacion
                    estadoCreacion = EstadoCrearUsuario.MedicoKO;

                    // Insertamos el nuevo medico a la tabla y generamos token si todo esta OK
                    if (_adminService.CrearMedico(model, user.Id)) {
                        await transaction.CommitAsync();
                        _context.SaveChanges();
                    }
                    // Revertimos toda la transacción si el usuario no se ha creado correctamente
                    await transaction.RollbackAsync();
                    estadoCreacion = EstadoCrearUsuario.UserYMedicoOK;
                }

                return estadoCreacion;
            } catch (Exception) {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // Validar contraseña login usuario y devolver nuevo token
        public Tokens? ObtenerTokenLogin(UserLoginDto userLogin) {
           
            // Obtenemos los datos del medico y su rol
            MedicosModel? medico = null;

            // Validamos el username y obtenemos la info del usuario con el rol
            if (!string.IsNullOrWhiteSpace(userLogin.UserName)) {
                medico = _identityService.ObtenerUsuarioYRolLogin(userLogin.UserName).Result;
            }

            // Generamos la info del usuario si se ha obtenido correctamente
            if (medico is not null) {
                UserInfoDto userInfo = medico.ToUserInfoDto();
                return _jwtManager.GenerateJWTTokens(userInfo);
            } 

            return null;
        }

        // Rrefresh access token
        public Tokens? RefreshAccesToken(Tokens tokenExpirado) {
            Tokens? nuevoToken = null;

            // Obtenemos la informacion del usuario del access token
            UserInfoDto userInfo = _jwtManager.GetClaimsFromExpiredToken(tokenExpirado.AccessToken).ToUserInfoDto();

            // Validamos el id del medico
            if (userInfo.IdMedico > 0) {

                // Obtenemos el refresh token de BD
                UserRefreshTokens? refreshToken = _jwtManager.ObtenerRefreshToken(userInfo.IdMedico, tokenExpirado.RefreshToken);

                // Validamos que el refresh token de la peticion sea el mismo que el de BD
                if (refreshToken is not null && refreshToken.RefreshToken == tokenExpirado.RefreshToken) {

                    // Genereamos nuevo token y nuevo refresh token
                    nuevoToken = _jwtManager.GenerateJWTTokens(userInfo);

                    // Eliminamos antigui refresh token si los datos del usuario son válidos
                    if (nuevoToken is not null && _jwtManager.DeleteRefreshTokens(userInfo.IdMedico, tokenExpirado.RefreshToken)) {
                        nuevoToken = _jwtManager.AddRefreshToken(nuevoToken, userInfo.IdMedico) ? nuevoToken : null;  
                    }
                }
            }

            // Si los claims del usuario y el refreshToken eran validos se crea nuevo token y refreshToken
            return nuevoToken;
        }

        // Eliminar refreshTokens del usuario
        public void CerrarSesion(Tokens tokens, UserInfoDto userInfo) {
            if (userInfo.IdMedico > 0 && !string.IsNullOrWhiteSpace(tokens.RefreshToken)) {
                _jwtManager.DeleteRefreshToken(userInfo.IdMedico, tokens.RefreshToken);
            }
        }
    }
}

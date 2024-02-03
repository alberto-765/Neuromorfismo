using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Usuarios;
using Microsoft.AspNetCore.Identity;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;

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

                var user = new UserModel {
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
                        estadoCreacion = EstadoCrearUsuario.UserYMedicoOK;
                    } else {
                        // Revertimos toda la transacción si el usuario no se ha creado correctamente
                        await transaction.RollbackAsync();
                    }
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
            if (medico is not null && medico.IdMedico > 0 && !string.IsNullOrWhiteSpace(medico.UserLogin) && !string.IsNullOrWhiteSpace(medico.Nombre) && 
                !string.IsNullOrWhiteSpace(medico.Apellidos) && !string.IsNullOrWhiteSpace(medico.Rol)) {
                UserInfoDto userInfo = medico.ToUserInfoDto();
                return _jwtManager.GenerateJWTTokens(userInfo);
            } 

            return null;
        }

        /// <summary>
        /// Refrescar acces token
        /// </summary>
        /// <param name="tokenExpirado"></param>
        /// <returns>Devuelve null si el refreshToken o los datos del usuario no es valido</returns>
        public Tokens? RefreshAccesToken(Tokens tokenExpirado) {
            try {
                Tokens? nuevoToken = null;

                // Obtenemos la informacion del usuario del access token
                UserInfoDto userInfo = _jwtManager.GetClaimsFromExpiredToken(tokenExpirado.AccessToken).ToUserInfoDto();

                // Validamos el id del medico
                if (userInfo.IdMedico > 0 && !string.IsNullOrWhiteSpace(userInfo.UserLogin) && !string.IsNullOrWhiteSpace(userInfo.Nombre) &&
                    !string.IsNullOrWhiteSpace(userInfo.Apellidos) && !string.IsNullOrWhiteSpace(userInfo.Rol)) {

                    // Obtenemos el refresh token de BD
                    UserRefreshTokens? refreshToken = _jwtManager.ObtenerRefreshToken(userInfo.IdMedico, tokenExpirado.RefreshToken);

                    // Validamos que el refresh token de la peticion sea el mismo que el de BD
                    if (refreshToken is not null && refreshToken.RefreshToken == tokenExpirado.RefreshToken) {

                        // Genereamos nuevo token y nuevo refresh token
                        nuevoToken = _jwtManager.GenerateJWTTokens(userInfo);
                    }
                }

                // Si los claims del usuario y el refreshToken eran validos se crea nuevo token y refreshToken
                return nuevoToken;
            } catch (Exception) {
                return null;
            }
        }

        // Eliminar refreshTokens del usuario
        public void CerrarSesion(Tokens tokens, UserInfoDto userInfo) {
            if (userInfo.IdMedico > 0 && !string.IsNullOrWhiteSpace(tokens.RefreshToken)) {
                _jwtManager.DeleteRefreshTokens(userInfo.IdMedico, tokens.RefreshToken);
            }
        }

        // Eliminar refreshTokens del usuario
        public void CerrarSesion(Tokens tokens) {
            UserInfoDto userInfo = _jwtManager.GetClaimsFromExpiredToken(tokens.RefreshToken).ToUserInfoDto();

            if (userInfo.IdMedico > 0 && !string.IsNullOrWhiteSpace(tokens.RefreshToken)) {
                _jwtManager.DeleteRefreshTokens(userInfo.IdMedico, tokens.RefreshToken);
            }
        }
    }
}

using Neuromorfismo.BackEnd.Dto;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.Shared.Dto.Usuarios;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Neuromorfismo.BackEnd.Service {
    public class UserAccountService : IUserAccountService  {
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;
        private readonly IJWTManagerRepository _jwtManager;
        private readonly UserManager<UserModel> _userManager;
        private readonly NeuromorfismoContext _context;

        public UserAccountService(IIdentityService identityService, NeuromorfismoContext context, IAdminsService adminService, IJWTManagerRepository jwtManager,
            UserManager<UserModel> userManager) {
            _adminService = adminService;
            _identityService = identityService;
            _context = context;
            _jwtManager = jwtManager;
            _userManager = userManager;
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

        /// <summary>
        /// Validar contraseña login usuario y devolver nuevo token
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
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
                return _jwtManager.GenerateJWTTokens(medico.ToUserInfoDto());
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

        /// <summary>
        /// Eliminar refreshTokens del usuario
        /// </summary>
        /// <param name="tokens"></param>
        /// <param name="userInfo"></param>
        public void CerrarSesion(Tokens tokens, UserInfoDto userInfo) {
            if (userInfo.IdMedico > 0 && !string.IsNullOrWhiteSpace(tokens.RefreshToken)) {
                _jwtManager.DeleteRefreshTokens(userInfo.IdMedico, tokens.RefreshToken);
            }
        }

        /// <summary>
        /// Eliminar refreshTokens del usuario
        /// </summary>
        /// <param name="tokens"></param>
        public void CerrarSesion(Tokens tokens) {
            UserInfoDto userInfo = _jwtManager.GetClaimsFromExpiredToken(tokens.RefreshToken).ToUserInfoDto();

            if (userInfo.IdMedico > 0 && !string.IsNullOrWhiteSpace(tokens.RefreshToken)) {
                _jwtManager.DeleteRefreshTokens(userInfo.IdMedico, tokens.RefreshToken);
            }
        }

        /// <summary>
        /// Reestablecer contraseña del usuario teniendo la contraseña actual y la nueva
        /// </summary>
        /// <returns></returns>
        public async Task<CodigosErrorChangePass[]> CambiarContrasena(ChangePasswordDto contrasenas, UserInfoDto userinfo) {
            // Si la respuesta es null devolvemos mensaje de error predeterminado
            List<CodigosErrorChangePass> responseMensage = new();

            // Obtenemos el usuario
            UserModel? usuario = await _userManager.FindByNameAsync(userinfo.UserLogin);

            if(usuario is not null) {
                IdentityResult? respuesta = await _userManager.ChangePasswordAsync(usuario, contrasenas.OldPassword, contrasenas.ConfirmNewPassword);

                if(respuesta is null || respuesta.Succeeded) {
                    responseMensage = new();
                } else {
                    responseMensage = respuesta.GetResponseMensage();
                }
            }


            // Devolvemos los tipos de error o un array vacio si esta OK
            return responseMensage.ToArray();
        }
    }
}

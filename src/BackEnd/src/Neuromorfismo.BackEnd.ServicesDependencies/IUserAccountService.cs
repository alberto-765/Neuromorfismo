using Neuromorfismo.BackEnd.Dto;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.ServicesDependencies {
    public interface IUserAccountService {
        Task<EstadoCrearUsuario> CrearUsuarioYMedico(UserRegistroDto model);
        Tokens? ObtenerTokenLogin(UserLoginDto userLogin);

        // Cerrar sesion del usuario
        void CerrarSesion(Tokens tokens, UserInfoDto userInfo);
        void CerrarSesion(Tokens tokens);

        // Token de autentencicacion
        Tokens? RefreshAccesToken(Tokens tokenExpirado);

        // Contraseña
        Task<CodigosErrorChangePass[]> CambiarContrasena(ChangePasswordDto contrasenas, UserInfoDto user);
    }
}

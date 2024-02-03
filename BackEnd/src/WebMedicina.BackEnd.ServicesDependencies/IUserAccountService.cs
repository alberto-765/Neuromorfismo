
using WebMedicina.BackEnd.Dto;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IUserAccountService {
        Task<EstadoCrearUsuario> CrearUsuarioYMedico(UserRegistroDto model);
        Tokens? ObtenerTokenLogin(UserLoginDto userLogin);

        // Cerrar sesion del usuario
        void CerrarSesion(Tokens tokens, UserInfoDto userInfo);
        void CerrarSesion(Tokens tokens);

        Tokens? RefreshAccesToken(Tokens tokenExpirado);
    }
}

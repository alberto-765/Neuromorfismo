using Microsoft.AspNetCore.Identity;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IIdentityService  {

        Task<MedicosModel?> ObtenerUsuarioYRol(int idMedico);
        Task<MedicosModel?> ObtenerUsuarioYRolLogin(string userName); // Obtener info del usuario en el login
        Task<bool> CrearUser(UserModel user, UserRegistroDto model);
        Task<bool> ComprobarContraseña(UserLoginDto userLogin);
        Task<bool> ComprobarUserName(string userName);
        Task<bool> ActualizarRol(string userLogin, string nuevoRol);
        Task<(bool userNameInvalido, string userNameGenerado)> GenerarUserName(string[] nomYApell);
        bool ValidarNomYApellUser(string nombre, string apellidos);

    }
}

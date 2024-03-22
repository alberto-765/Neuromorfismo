using Microsoft.AspNetCore.Identity;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.ServicesDependencies {
    public interface IIdentityService  {

        /// <summary>
        /// Obtener info del usuario en el login
        /// </summary>
        /// <param name="idMedico"></param>
        /// <returns></returns>
        Task<MedicosModel?> ObtenerUsuarioYRolLogin(string userName); 

        /// <summary>
        /// Crear nuevo usuario solo administradores
        /// </summary>
        /// <param name="user"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> CrearUser(UserModel user, UserRegistroDto model);

        /// <summary>
        /// Autenticar contraseña login usuario
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        Task<bool> ComprobarContraseña(UserLoginDto userLogin);

        /// <summary>
        /// LLamamos a BBDD y devolvemos si existe o no el userName
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        Task<bool> ComprobarUserName(string userName);


        /// <summary>
        /// Actualizamos rol del usuario
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="nuevoRol"></param>
        /// <returns></returns>
        Task<bool> ActualizarRol(string userLogin, string nuevoRol);

        /// <summary>
        /// Genereamos un username valido para un nuevo medico
        /// </summary>
        /// <param name="nomYApell"></param>
        /// <returns>UserName Invalido y UserName generado</returns>
        Task<(bool userNameInvalido, string userNameGenerado)> GenerarUserName(string[] nomYApell);

        /// <summary>
        /// Validamos si el nombre y apellidos del nuevo usuario son validos 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellidos"></param>
        /// <returns></returns>
        bool ValidarNomYApellUser(string nombre, string apellidos);

        /// <summary>
        /// Realiza el reestablecimiento de la contraseña por parte de un administrador
        /// </summary>
        /// <param name="userLogin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<bool> RestablecerPassword(RestartPasswordDto resetPass);
    }
}

using Microsoft.AspNetCore.Components;
using System.Security.Claims; 
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IAdminsService {
        /// <summary>
        /// se genera contraseña aleatorio para el nuevo usuario
        /// </summary>
        /// <returns></returns>
        Task<string> GenerarContraseñaAleatoria(); 

        /// <summary>
        /// se genera el tooltip para pantalla de gestion usuarios
        /// </summary>
        /// <param name="user"></param>
        /// <param name="tooltipInfoUser"></param>
        /// <param name="mostrarTooltip"></param>
        void GenerarTooltipInfoUser(ClaimsPrincipal user, ref MarkupString tooltipInfoUser, ref bool mostrarTooltip);

        /// <summary>
        /// Validar nombre mutacion, farmaco y epilepsia
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        string ValidarNuevoNombre(string nombre); 

        /// <summary>
        /// Validamos si el nombre y apellidos del nuevo usuario son validos 
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="apellidos"></param>
        /// <returns>True si nombre y apellidos cumplen sus data anotations</returns>
        bool ValidarNomYApellUser(string nombre, string apellidos);

        /// <summary>
        /// Obtener todos los medicos filtrando por rango del usuario
        /// </summary>
        /// <returns></returns>
        Task<List<UserUploadDto>> ObtenerAllMedicos(FiltradoTablaDefaultDto? camposFiltrado = null);

        /// <summary>
        /// Resetea contraseña del usuario seleccionado
        /// </summary>
        /// <param name="restartPass"></param>
        /// <returns></returns>
        Task<bool> ResetearContrasena(RestartPasswordDto restartPass);
    }
}

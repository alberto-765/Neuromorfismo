using Microsoft.AspNetCore.Components;
using System.Security.Claims; 
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IAdminsService {
        Task<string> GenerarContraseñaAleatoria(); // se genera contraseña aleatorio para el nuevo usuario
        void GenerarTooltipInfoUser(ClaimsPrincipal user, ref MarkupString tooltipInfoUser, ref bool mostrarTooltip);// se genera el tooltip para pantalla de gestion usuarios
        Dictionary<string, string> CrearDiccionarioFiltros();
        string ValidarNuevoNombre(string nombre); // Validar nombre mutacion, farmaco y epilepsia
        bool ValidarNomYApellUser(string nombre, string apellidos);
        Task<List<UserUploadDto>> ObtenerAllMedicos(FiltradoTablaDefaultDto? camposFiltrado = null);


        /// <summary>
        /// Resetea contraseña del usuario seleccionado
        /// </summary>
        /// <param name="restartPass"></param>
        /// <returns></returns>
        Task<bool> ResetearContrasena(RestartPasswordDto restartPass);
    }
}

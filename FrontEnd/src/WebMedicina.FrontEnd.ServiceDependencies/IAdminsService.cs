using Microsoft.AspNetCore.Components;
using System.Security.Claims; 
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IAdminsService {
        Task<string> GenerarContraseñaAleatoria(); // se genera contraseña aleatorio para el nuevo usuario
        void GenerarTooltipInfoUser(ClaimsPrincipal user, ref MarkupString tooltipInfoUser, ref bool mostrarTooltip);// se genera el tooltip para pantalla de gestion usuarios
        //Task<ReadOnlyDictionary<string, string>> ObtenerFiltrosSession(); // Obtener los filtros seleccionados
        Dictionary<string, string> CrearDiccionarioFiltros();
        string ValidarNuevoNombre(string nombre); // Validar nombre mutacion, farmaco y epilepsia
        bool ValidarNomYApellUser(string nombre, string apellidos);
        Task<List<UserUploadDto>> ObtenerAllMedicos(FiltradoTablaDefaultDto? camposFiltrado = null);
    }
}

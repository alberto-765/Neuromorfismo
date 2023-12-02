using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IAdminsService {
        Task<string> GenerarContraseñaAleatoria(); // se genera contraseña aleatorio para el nuevo usuario
        void GenerarTooltipInfoUser(ClaimsPrincipal user, ref MarkupString tooltipInfoUser, ref bool mostrarTooltip);// se genera el tooltip para pantalla de gestion usuarios
        Task<ReadOnlyDictionary<string, string>> ObtenerFiltrosSession(); // Obtener los filtros seleccionados
        Dictionary<string, string> CrearDiccionarioFiltros();
        string? ValidarNuevoNombre(string nombre); // Validar nombre mutacion, farmaco y epilepsia
        bool ValidarNomYApellUser(string nombre, string apellidos);

    }
}

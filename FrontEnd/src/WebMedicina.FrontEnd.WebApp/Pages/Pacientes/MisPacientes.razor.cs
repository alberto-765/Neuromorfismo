using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class MisPacientes {
        // DEPENDECNIAS
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;

        // PARAMETROS
        [Parameter] public List<CrearPacienteDto>? ListaPacientes { get; set; } // Lista pacientes para mostrar
        [Parameter] public EventCallback<int> EliminarPaciente { get; set; } // Evento callback para eliminar paciente
        public bool MostrarOverlay { get; set; } = true; // Bool para mostrar overlay de carga
        private ClaimsPrincipal? user { get; set; } // Datos del usuario


        /// <summary>
        /// Detectamos cuando cambie el valor de la lista
        /// </summary>
        protected override async Task OnParametersSetAsync() {
            try {
                if (ListaPacientes is not null) {

                    // Filtramos los pacientes para mostrar unicamente los del usuario en caso de ser SuperAdmin y Admin
                    if(authenticationState is not null) {
                        var authState = await authenticationState;
                        user = authState?.User;
                    }

                    // Filtramos el listado de pacientes
                    if (ListaPacientes.Any()) {
                        ListaPacientes = _pacientesService.FiltrarMisPacientes(ListaPacientes, user);
                    }
                    MostrarOverlay = false;
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}

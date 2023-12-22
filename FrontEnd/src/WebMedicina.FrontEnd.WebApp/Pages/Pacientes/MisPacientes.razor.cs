using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Security.Claims;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class MisPacientes {
        // DEPENDECNIAS
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] private ExcepcionPersonalizada _excepcionPersonalizada { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; }

        // Lista pacientes para mostrar
        [Parameter] public List<CrearPacienteDto>? ListaPacientes { get; set; }

        // Bool para mostrar overlay de carga
        public bool MostrarOverlay { get; set; } = true;

        // Datos del usuario
        private ClaimsPrincipal? user { get; set; }


        /// <summary>
        /// Detectamos cuando cambie el valor de la lista
        /// </summary>
        protected override async Task OnParametersSetAsync() {
            try {
                if (ListaPacientes is not null && ListaPacientes.Any()) {

                    // Filtramos los pacientes para mostrar unicamente los del usuario en caso de ser SuperAdmin y Admin
                    if(authenticationState is not null) {
                        var authState = await authenticationState;
                        user = authState?.User;
                    }

                    // Filtramos el listado de pacientes
                    ListaPacientes = await _pacientesService.FiltrarMisPacientes(ListaPacientes, user);
                    MostrarOverlay = false;
                }
            } catch (Exception ex) {
                _excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }
    }
}

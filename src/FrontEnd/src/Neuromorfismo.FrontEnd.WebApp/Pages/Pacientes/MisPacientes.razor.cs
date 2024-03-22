using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Collections.Immutable;
using System.Security.Claims;
using Neuromorfismo.FrontEnd.Dto;
using Neuromorfismo.FrontEnd.Service;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class MisPacientes {
        // DEPENDECNIAS
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;

        // PARAMETROS
        [Parameter] public ImmutableList<CrearPacienteDto>? PacientesFiltrados { get; set; }

        // Pacientes mostrados actualmente, seran descargados en el excel
        [Parameter] public ExcelPacientesDto ExcelPacientes { get; set; } = default!;
        [Parameter] public EventCallback<ExcelPacientesDto> ExcelPacientesChanged { get; set; }
        [Parameter] public EventCallback<int> EliminarPaciente { get; set; } // Evento callback para eliminar paciente
        [Parameter] public EventCallback<CrearPacienteDto> MostrarLineaTemp { get; set; } // Evento callback para mostrar la linea temporal de un paciente

        public bool MostrarOverlay { get; set; } = true; // Bool para mostrar overlay de carga
        private ClaimsPrincipal? user { get; set; } // Datos del usuario



        protected override async Task OnInitializedAsync() {
            await base.OnInitializedAsync();

            // Filtramos los pacientes para mostrar unicamente los del usuario en caso de ser SuperAdmin y Admin
            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;
            }
        }


        /// <summary>
        /// Detectamos cuando cambie el valor de la lista
        /// </summary>
        protected override void OnParametersSet() {
            base.OnParametersSet();

            if (PacientesFiltrados is not null) {

                // Filtramos el listado de pacientes
                ExcelPacientes.Pacientes = PacientesFiltrados.Any() ? _pacientesService.FiltrarMisPacientes(PacientesFiltrados, user) : ImmutableList<CrearPacienteDto>.Empty;
                ExcelPacientes.NombrePaginaExcel = "Mis Pacientes";
                MostrarOverlay = false;
            }
        }
    }
}

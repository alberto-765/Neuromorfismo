using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.Dto;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class AllPacientes {
        // DEPENDENCIAS
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;

        // PARAMETROS
        [Parameter] public EventCallback<int> EliminarPaciente { get; set; } // Evento callback para eliminar paciente
        [Parameter] public EventCallback<CrearPacienteDto> MostrarLineaTemp { get; set; } // Evento callback para mostrar la linea temporal de un paciente
        [Parameter] public ImmutableList<CrearPacienteDto>? PacientesFiltrados { get; set; }

        // Pacientes mostrados actualmente, seran descargados en el excel
        [Parameter] public ExcelPacientesDto ExcelPacientes { get; set; } = default!;
        [Parameter] public EventCallback<ExcelPacientesDto> ExcelPacientesChanged { get; set; }



        public bool MostrarOverlay { get; set; } = true;


        /// <summary>
        /// Detectamos cuando cambie el valor de la lista
        /// </summary>
        protected override void OnParametersSet() {
            if (PacientesFiltrados is not null) {
                MostrarOverlay = false;
                ExcelPacientes.Pacientes = PacientesFiltrados;
                ExcelPacientes.NombrePaginaExcel = "Todos los Pacientes";
            }
        }
    }
}

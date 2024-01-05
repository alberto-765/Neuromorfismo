using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class AllPacientes {
        [Parameter] public List<CrearPacienteDto>? ListaPacientes { get; set; }
        [Parameter] public EventCallback<int> EliminarPaciente { get; set; } // Evento callback para eliminar paciente

        public bool MostrarOverlay { get; set; } = true;


        /// <summary>
        /// Detectamos cuando cambie el valor de la lista
        /// </summary>
        protected override void OnParametersSet() {
            if (ListaPacientes is not null) {
                MostrarOverlay = false;
            }
        }
    }
}

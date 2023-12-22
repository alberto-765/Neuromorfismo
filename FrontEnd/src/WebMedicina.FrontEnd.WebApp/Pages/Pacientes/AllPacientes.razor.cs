using Microsoft.AspNetCore.Components;
using System.Runtime.CompilerServices;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class AllPacientes {
        [Parameter] public List<CrearPacienteDto>? ListaPacientes { get; set; }
        public bool MostrarOverlay { get; set; } = true;


        /// <summary>
        /// Detectamos cuando cambie el valor de la lista
        /// </summary>
        protected override void OnParametersSet() {
            if (ListaPacientes is not null && ListaPacientes.Any()) {
                MostrarOverlay = false;
            }
        }
    }
}

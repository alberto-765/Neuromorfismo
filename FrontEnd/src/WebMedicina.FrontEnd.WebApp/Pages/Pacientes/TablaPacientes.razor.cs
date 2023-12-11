using Microsoft.AspNetCore.Components;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class TablaPacientes {
        [Parameter] public List<PacienteDto>? ListaPacientes { get; set; }
    }
}

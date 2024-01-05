using Microsoft.AspNetCore.Components;
using System.Collections.Immutable;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal {
    public partial class LineaTemporal {
        [CascadingParameter(Name = "Etapas")] private ImmutableSortedDictionary<int, EtapasDto>? etapasLineaTemporal { get; set; }




    }
}

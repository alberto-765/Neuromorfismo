using Microsoft.AspNetCore.Components;
using System.Collections.Immutable;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal
{
    public partial class LineaTemporal {
        // Injecciones

        // Parametros
        [CascadingParameter(Name = "Etapas")] private ImmutableSortedDictionary<int, EtapaLTDto>? EtapasLineaTemporal { get; set; }
        [CascadingParameter(Name = "Evolucion")] private SortedList<int, EvolucionLTDto> Evolucion { get; set; } = null!;




    }
}

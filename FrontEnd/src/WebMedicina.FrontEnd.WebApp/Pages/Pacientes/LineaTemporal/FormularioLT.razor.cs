using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal
{
    public partial class FormularioLT {
        // Injecciones
        [Inject] private IDialogService _dialogoService { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;
        [Inject] private ILineaTemporalService _lineaTemporalService { get; set; } = null!;

        // Parametros
        [Parameter] public ImmutableSortedDictionary<int, EtapaLTDto>? EtapasLineaTemporal { get; set; }
        [Parameter] public SortedList<int, EvolucionLTDto> Evolucion { get; set; } = null!;
        [Parameter] public EventCallback<SortedList<int, EvolucionLTDto>> EvolucionChanged { get; set; }


    }
}

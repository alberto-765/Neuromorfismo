using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.Dto;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal
{
    public partial class LineaTemporal {
        // Injecciones
        [Inject] private IOptions<ImagenesServerDto> _imgOptions { get; set; } = null!;
        [Inject] private IConfiguration _configuration { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;
        [Inject] private ILineaTemporalService _lineaTemporalService { get; set; } = null!;

        // Parametros
        [CascadingParameter(Name = "Etapas")] private ImmutableSortedDictionary<int, EtapaLTDto>? EtapasLineaTemporal { get; set; }
        [CascadingParameter(Name = "Evolucion")] private SortedList<int, EvolucionLTDto> Evolucion { get; set; } = null!;

        // Ultima etapa la cual debe pintarse en el timeline
        private int? UltimaEtapa {  get; set; }

        // Creamos linea temporal
        protected override void OnInitialized() {
            try {
                UltimaEtapa = EtapasLineaTemporal?.LastOrDefault().Key;
                EtapasLineaTemporal = null;
            } catch (Exception) {
                throw;
            }
        }
       
    }
}

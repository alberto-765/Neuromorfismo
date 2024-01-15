using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Options;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.Dto;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using static WebMedicina.FrontEnd.Dto.EstadosEtapasLTDto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal {
    public partial class LineaTemporal {
        // Injecciones
        [Inject] private IOptions<ImagenesServerDto> _imgOptions { get; set; } = null!;
        [Inject] private IConfiguration _configuration { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;
        [Inject] private ILineaTemporalService _lineaTemporalService { get; set; } = null!;

        // Parametros
        [Parameter] public ImmutableSortedDictionary<int, EtapaLTDto>? EtapasLineaTemporal { get; set; }
        [Parameter] public SortedList<int, EvolucionLTDto> Evoluciones { get; set; } = null!;

        // Ultima etapa de la evolucion del paciente
        private int? UltimaEtapaPaciente { get; set; }

     


        // Creamos linea temporal
        protected override void OnInitialized() {
            try {
                UltimaEtapaPaciente = Evoluciones.LastOrDefault().Key;
            } catch (Exception) {
                throw;
            }
        }

        // Calcular estado de una etapa
        private EstadoEtapa CalcularEstadoEtapa(KeyValuePair<int, EtapaLTDto> etapa, int indice, EvolucionLTDto? evolucionPintar) {
            try {
                EstadoEtapa estadoEtapa = EstadoEtapa.Pasada;

                // Si el paciente aun no tiene evolucion 
                if (evolucionPintar is null) {

                    // Validamos si es la primera etapa que debe ser rellenada
                    if(indice == 0) {
                        estadoEtapa = EstadoEtapa.Presente;
                    } else {
                        estadoEtapa = EstadoEtapa.Futura;
                    }

                } else if(etapa.Key == UltimaEtapaPaciente) {
                    estadoEtapa = EstadoEtapa.Presente;
                } else if (etapa.Key > UltimaEtapaPaciente) {
                     estadoEtapa = EstadoEtapa.Futura;
                }

                return estadoEtapa;
            } catch (Exception) {
                throw;
            }
        }
    }
}

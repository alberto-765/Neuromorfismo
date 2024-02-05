using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using static WebMedicina.FrontEnd.Dto.EstadosEtapasLT;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal {
    public partial class LineaTemporal {
        // Injecciones
        [Inject] private ISnackbar _snackbar { get; set; } = default!;
        [Inject] private ILineaTemporalService _lineaTemporalService { get; set; } = default!;
        [Inject] private IDocumentacionService _documentacionService { get; set; } = null!;


        // Parametros
        [Parameter] public ImmutableSortedDictionary<int, EtapaLTDto> EtapasLineaTemporal { get; set; } = default!;
        [Parameter] public SortedList<int, EvolucionLTDto> Evoluciones { get; set; } = default!;
        [Parameter] public EventCallback<SortedList<int, EvolucionLTDto>> EvolucionesChanged { get; set; }
        [Parameter] public int IdPaciente { get; set; }
        [Parameter] public string IdContenedor { get; set; } = default!;

        // Ultima etapa de la evolucion del paciente
        private int UltimaEtapaPaciente { get; set; }
        private EvolucionLTDto? evolucionPintar  = default!;
        private EstadoEtapa estadoEtapa { get; set; } = EstadoEtapa.Pasada;

        // Etapa de fin de evolutivo
        private int EtapaFinEvolutivo { get; set; } 

        protected override void OnInitialized() {
            EtapaFinEvolutivo = EtapasLineaTemporal.Keys.LastOrDefault();
        }

        // Creamos linea temporal
        protected override void OnAfterRender(bool firstRender) {
            try {
                int ultEvoPac = Evoluciones.Keys.LastOrDefault();

                // Si el paciente ya acabado la evolucion asignamos el id de la etpa de fin evolutivo
                UltimaEtapaPaciente = (ultEvoPac == EtapaFinEvolutivo ? EtapaFinEvolutivo : EtapasLineaTemporal.Keys.FirstOrDefault(q => q > ultEvoPac));
            } catch (Exception) {
                _snackbar.Add("No ha sido posible cargar la linea temporal", Severity.Error);
            }
        }

        // Calcular estado de una etapa
        private EstadoEtapa CalcularEstadoEtapa(KeyValuePair<int, EtapaLTDto> etapa) {
            EstadoEtapa estadoEtapa = EstadoEtapa.Pasada;

            if (etapa.Key == EtapaFinEvolutivo && UltimaEtapaPaciente == EtapaFinEvolutivo) {
                estadoEtapa = EstadoEtapa.FinEtapas;

                // Si la ultima etapa es 0 es porque no hay etapas en BD
            } else if (UltimaEtapaPaciente > 0) {
                if (etapa.Key == UltimaEtapaPaciente) {
                    estadoEtapa = EstadoEtapa.Presente;
                } else if (etapa.Key > UltimaEtapaPaciente) {
                    estadoEtapa = EstadoEtapa.Futura;
                }
            } 

            return estadoEtapa;
        }

        /// <summary>
        /// Actualizar una etapa de la evolucion del paciente o añadirla si es nueva
        /// </summary>
        /// <param name="nuevaEvolucion"></param>
        public async Task ActualizarEvolucionPaciente(EditarEvolucionLTDto nuevaEvolucion) {
            try {
                LLamadaEditarEvoDto evoEditada = new(nuevaEvolucion, UltimaEtapaPaciente, IdPaciente);
                Evoluciones = await _lineaTemporalService.ActEvoPac(evoEditada);
                StateHasChanged();

                // Realizamos envio del correo 
                Console.Write("LLega a enviar corrreo");
                if (Evoluciones.Any()) {
                    await _documentacionService.EnviarEmailEvoActu(Evoluciones.Last().Value, IdPaciente, IdContenedor);
                }

                Console.Write("Sale de enviar corrreo");

            } catch (Exception ex) {
                _snackbar.Add("No ha sido posible actualizar la etapa de la evolución del paciente", Severity.Error);
                Console.Write(ex.ToString());
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal {
    public partial class ContenedorLineaTemp {
        // Injecciones
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;
        [Inject] private ILineaTemporalService _lineaTemporalService { get; set; } = null!;

        // Clase Contenedor Linea Temporal
        private string SelectorScroll { get; set; } = string.Empty; // Id fila del paciente en la tabla
        private string IdContenedorLT { get; set; } = "contenedor-lineaTemporal";
        private string ClaseContenedor { get; set; } = string.Empty;

        // Mostrar u ocultar contenedor
        private bool LineaTemporalExpanded = false;
        private bool LineaTemporalExpandedProp { 
            get => LineaTemporalExpanded;
            set {
                LineaTemporalExpanded = value;
                ClaseContenedor = IdContenedorLT + (LineaTemporalExpanded ? "-expandido" : "-hidden");
            }
        } 

        // Listas etapas y evolucion paciente
        private SortedList<int, EvolucionLTDto> Evolucion = new(); // Evoluciones del paciente
        private ImmutableSortedDictionary<int, EtapaLTDto>? EtapasLineaTemporal { get; set; } // Etapas para la linea temporal

        protected override async Task OnInitializedAsync() {
            try {
                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 300;
                _snackbar.Configuration.HideTransitionDuration = 300;
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
                _snackbar.Configuration.ShowCloseIcon = false;
                _snackbar.Configuration.VisibleStateDuration = 7000;

                await ObtenerEtapasLT();
            } catch (Exception) {
                throw;
            }
        }

        // Obtenemos todas las etapas de la linea temporal
        private async Task ObtenerEtapasLT() {
            try {
                EtapasLineaTemporal = await _lineaTemporalService.ObtenerEtapas();
            } catch (Exception) {
                _snackbar.Add("No ha sido posible cargar la linea temporal");
                throw;
            }
        }

        // Cerramos cuadro linea temporal y resetear datos
        private async Task CerrarLineaTemporal() {
            try {
                // Reseteamos datos
                LineaTemporalExpandedProp = false;
                Evolucion = new();
                SelectorScroll = string.Empty;

                StateHasChanged();
                await Task.Delay(1000);
            } catch (Exception) {
                throw;
            }
        }

        // Obtenemos evolucion del paciente, abrimos contenedor linea temporal y hacemos scroll al contenedor
        public async Task MostrarLineaTemp(int idPaciente) {
            try {
                // Mostramos linea temporal y configuramos el selector para el scroll top
                LineaTemporalExpandedProp = true;

                // Hacemos scroll al contenedor linea temporal
                await _comun.ScrollHaciaElemento(IdContenedorLT, "end");
                StateHasChanged();

                // Obtenemos evolucion del paciente
                Evolucion = await _lineaTemporalService.ObtenerEvolucionPaciente(idPaciente);
                SelectorScroll = $"#Paciente{idPaciente}";
                StateHasChanged();


            } catch (Exception) {
                throw;
            }
        }
    }
}

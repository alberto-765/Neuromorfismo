using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
using MudBlazor;
using System.Collections.Immutable;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal {
    public partial class ContenedorLineaTemp {
        // Injecciones
        [Inject] private IDialogService _dialogoService { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;
        [Inject] private ILineaTemporalService _lineaTemporalService { get; set; } = null!;

        // Parametros 
        private string SelectorScroll { get; set; } = string.Empty;

        private bool LineaTemporalExpanded { get; set; } = false; // Mostrar u ocultar contenedor
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



        /// <summary>
        /// Obtener todas las etapas de la linea temporal
        /// </summary>
        /// <returns></returns>
        private async Task ObtenerEtapasLT() {
            try {
                EtapasLineaTemporal = await _lineaTemporalService.ObtenerEtapas();
            } catch (Exception) {
                _snackbar.Add("No ha sido posible cargar la linea temporal. Notifiquelo a un administrador");
                throw;
            }
        }

        private async Task CerrarLineaTemporal() {
            try {
                LineaTemporalExpanded = false;
                Evolucion = new();
                StateHasChanged();
                await Task.Delay(1000);
            } catch (Exception) {
                throw;
            }
        }

        public async Task MostrarLineaTemp(int idPaciente) {
            try {
                LineaTemporalExpanded = true;
                SelectorScroll = $"#Paciente{idPaciente}";
                Evolucion = await _lineaTemporalService.ObtenerEvolucionPaciente(idPaciente);
                StateHasChanged();
            } catch (Exception) {
                throw;
            }
        }
    }
}

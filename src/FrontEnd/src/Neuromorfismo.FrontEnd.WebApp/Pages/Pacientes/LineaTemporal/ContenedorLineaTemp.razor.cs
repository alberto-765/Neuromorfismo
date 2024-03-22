using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.LineaTemporal;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal {
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
        private bool _lineaTemporalExpanded = false;
            // Animacion Fade-In o Fade-Out
        async Task OcultarMostrarLT (bool value){
            if(value) {
                ClaseContenedor = string.Concat(IdContenedorLT, "-expandido");
                await _comun.FadeIn($"#{IdContenedorLT}");
                _lineaTemporalExpanded = true;
            } else {
                await _comun.FadeOut($"#{IdContenedorLT}");
                ClaseContenedor = string.Empty;


                await Task.Delay(500).ContinueWith(t => {
                    _lineaTemporalExpanded = false;
                });
            }
            StateHasChanged();
        }

        // Listas etapas y evolucion paciente
        private SortedList<short, EvolucionLTDto> Evoluciones = new(); // Evoluciones del paciente
        private ImmutableSortedDictionary<short, EtapaLTDto>? EtapasLineaTemporal { get; set; } // Etapas para la linea temporal

        // Idpaciente del que se muestran las evoluciones
        private CrearPacienteDto Paciente { get; set; } = new();

        protected override async Task OnInitializedAsync() {
            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.ShowTransitionDuration = 300;
            _snackbar.Configuration.HideTransitionDuration = 300;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
            _snackbar.Configuration.ShowCloseIcon = false;
            _snackbar.Configuration.VisibleStateDuration = 7000;

            await ObtenerEtapasLT(); 
        }

        // Obtenemos todas las etapas de la linea temporal
        private async Task ObtenerEtapasLT() {
            try {
                EtapasLineaTemporal = await _lineaTemporalService.ObtenerEtapas();
            } catch (Exception) {
                _snackbar.Add("No ha sido posible cargar la linea temporal");
            }
        }

        // Cerramos cuadro linea temporal y resetear datos
        private async Task CerrarLineaTemporal() { 
            await OcultarMostrarLT(false);
            // Reseteamos datos
            Evoluciones = new();
            await _comun.ScrollHaciaElemento(SelectorScroll);
            SelectorScroll = string.Empty;

        }

        // Obtenemos evolucion del paciente, abrimos contenedor linea temporal y hacemos scroll al contenedor
        public async Task MostrarLineaTemp(CrearPacienteDto paciente) {
            try {
                // Realizamos la tarea de evolución del paciente
                Task<SortedList<short, EvolucionLTDto>> GetEvo = _lineaTemporalService.ObtenerEvolucionPaciente(paciente.IdPaciente);

                // Mostramos linea temporal y configuramos el selector para el scroll top
                SelectorScroll = $"#Paciente{paciente.NumHistoria}";
                Paciente = paciente;

                // Obtenemos evoluciones
                Evoluciones = await GetEvo;

                await OcultarMostrarLT(true);
                await _comun.ScrollBottom();
            } catch (Exception) {
                await CerrarLineaTemporal();
                _snackbar.Add("No ha sido posible cargar la linea temporal", Severity.Error);
            } 
        }
    }
}

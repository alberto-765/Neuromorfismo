using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Immutable;
using System.Threading;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

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
                ClaseContenedor = string.Concat(IdContenedorLT, (LineaTemporalExpanded ? "-expandido" : "-hidden"));
            }
        } 

        // Listas etapas y evolucion paciente
        private SortedList<short, EvolucionLTDto> Evoluciones = new(); // Evoluciones del paciente
        private ImmutableSortedDictionary<short, EtapaLTDto>? EtapasLineaTemporal { get; set; } // Etapas para la linea temporal

        // Idpaciente del que se muestran las evoluciones
        private CrearPacienteDto Paciente { get; set; } = new();

        protected override async Task OnInitializedAsync() {
            // Clase contenedor ocultada por default
            ClaseContenedor = IdContenedorLT + "-hidden";

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
        private void CerrarLineaTemporal() { 
                // Reseteamos datos
                LineaTemporalExpandedProp = false;
                Evoluciones = new();
                SelectorScroll = string.Empty;
        }

        // Obtenemos evolucion del paciente, abrimos contenedor linea temporal y hacemos scroll al contenedor
        public async Task MostrarLineaTemp(CrearPacienteDto paciente) {
            try {
                // Mostramos linea temporal y configuramos el selector para el scroll top
                LineaTemporalExpandedProp = true;
                SelectorScroll = $"#Paciente{paciente.NumHistoria}";
                Paciente = paciente;
                Evoluciones = await _lineaTemporalService.ObtenerEvolucionPaciente(Paciente.IdPaciente);


                // Obtenemos evolucion del paciente
                await _comun.ScrollBottom();
                StateHasChanged();
            } catch (Exception ex) {
                Console.WriteLine(ex);
                Evoluciones = new();
                _snackbar.Add("No ha sido posible cargar la linea temporal", Severity.Error);
            }
        }

    }
}

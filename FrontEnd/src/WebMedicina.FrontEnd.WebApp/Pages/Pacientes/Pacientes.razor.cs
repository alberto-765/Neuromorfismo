using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.Dto;
using WebMedicina.FrontEnd.Service;
using System.Runtime.CompilerServices;
using WebMedicina.Shared.Dto;
using MudBlazor;
using System.Drawing;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class Pacientes {
        // Dependencias
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        private ClaimsPrincipal? user { get; set; } // datos usuario logueado
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private IDialogService _dialogoService { get; set; }

        // Panel de filtros
        public bool OrdenarTalla { get; set; } // Mostrar un icono u otro en ordenar por talla
        private bool FiltrosAbierto { get; set; }



        // Valores seleccionados para mostrar en los filtros
        private FiltroPacienteDto FilrosPacientes { get; set; } = new(); // Dto con los filtros seleccionados
        private IEnumerable<string>? ListaMedicos { get; set; } = null;

        //private IEnumerable<FarmacosDto>? ListaFarmacos { get; set; } = null;
        //private string farmacoFiltrado { get; set; }
        private IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        private IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;
        private string epilepsiaFiltrado { get; set; }
        private string mutacionFiltrado { get; set; }
        private string medicoFiltrado { get; set; }
        private string otraOpcFarmaco { get; set; }

        // Pop up crear paciente
        DialogOptions opcionesDialogo { get; set; } = new DialogOptions{ FullWidth=true, CloseButton=true, DisableBackdropClick=true, Position=DialogPosition.Center, CloseOnEscapeKey=true};


        protected override async Task OnInitializedAsync() {
            try {
                if (AuthenticationState is not null) {
                    var authState = await AuthenticationState;
                    user = authState?.User;
                }
                await ObtenerFiltrosSelects();
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        // Buscador para autocomplete de medicos
        private async Task<IEnumerable<string>> BuscarMedPac(string medico) {
            try {
                // Si la lista es null se obtiene por primera vez de BD
                ListaMedicos ??= await _pacientesService.ObtenerAllMed();

                // Si hay medicos en la lista se realiza la busqueda
                if (ListaMedicos != null && ListaMedicos.Any()) {
                    return ListaMedicos.Where(q => q.Contains(medico, StringComparison.InvariantCultureIgnoreCase));
                } else {
                    return Enumerable.Empty<string>();
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Obtener listas de filtros
        private async Task ObtenerFiltrosSelects() {
            var opcionesSelects = await _pacientesService.ObtenerFiltros();

            // Asignamos la lista de epilepsias
            ListaEpilepsias = opcionesSelects.ListaEpilepsias;
            // Asignamos la lista de mutaciones
            ListaMutaciones = opcionesSelects.ListaMutaciones;
            // Asignamos la lista de farmacos
            //ListaFarmacos = opcionesSelects.ListaFarmacos;
        }

        // Mostrar dialogo para crear paciente nuevo
        private async Task MostrarCrearPac() {
            var dialog = _dialogoService.Show<CrearPaciente>("titulo", opcionesDialogo);
            var result = await dialog.Result;
            if (!result.Cancelled) {
                Console.WriteLine("hola");
           }
        }

        // Cambiar los valores seleccionados para mostrarlos bonitos
        private async Task CambiarValorFarmaco(string t) {
        }
    }
}

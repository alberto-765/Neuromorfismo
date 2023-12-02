using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.Service;
using System.Runtime.CompilerServices;
using WebMedicina.Shared.Dto;
using MudBlazor;
using System.Drawing;
using Microsoft.AspNetCore.Components.Forms;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class Pacientes {
        // Dependencias
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private IDialogService _dialogoService { get; set; }

        // Panel de filtros
        public bool OrdenarTalla { get; set; } // Mostrar un icono u otro en ordenar por talla
        private bool FiltrosAbierto { get; set; } = false;


        // Formulario filtrado
        private IEnumerable<PacienteDto>? ListaPacientes { get; set; }

        // Valores seleccionados para mostrar en los filtros
        private FiltroPacienteDto FiltrosPacientes { get; set; } = new(); // Dto con los filtros seleccionados
        private IEnumerable<string>? ListaMedicos { get; set; } = null;
        private IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        private IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;

        //private IEnumerable<FarmacosDto>? ListaFarmacos { get; set; } = null;
        //private string farmacoFiltrado { get; set; }

        // String que se puestra en el select
        private string epilepsiaFiltrado { get; set; } 
        private string mutacionFiltrado { get; set; }


        // Pop up crear paciente
        DialogOptions opcionesDialogo { get; set; } = new DialogOptions{ FullWidth=true, CloseButton=true, DisableBackdropClick=true, Position=DialogPosition.Center, CloseOnEscapeKey=true};


        protected override async Task OnInitializedAsync() {
            try {
                await ObtenerFiltrosSelects();

                // Obtenemos el listado de pacientes
                ListaPacientes = await _pacientesService.ObtenerPacientes();
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
            try {
                var opcionesSelects = await _pacientesService.ObtenerFiltros();

                // Asignamos la lista de epilepsias
                ListaEpilepsias = opcionesSelects.ListaEpilepsias;
                // Asignamos la lista de mutaciones
                ListaMutaciones = opcionesSelects.ListaMutaciones;
                // Asignamos la lista de farmacos
                //ListaFarmacos = opcionesSelects.ListaFarmacos;
            } catch (Exception) {
                throw;
            }
        }

        // Mostrar dialogo para crear paciente nuevo
        private async Task MostrarCrearPac() {
            try {
                var dialog = _dialogoService.Show<CrearPaciente>("titulo", opcionesDialogo);
                var result = await dialog.Result;
                if (result.Cancelled == false) {
                    Console.WriteLine(result.Data);
                }                //ListaPacientes = await _pacientesService.ObtenerPacientesFiltr(FiltrosPacientes);
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Obtener pacientes de BD
        private async Task ObtenerPacientesFiltrados() {
            try {

            } catch (Exception) {
                throw;
            }
        }
    }
}

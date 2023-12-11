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
using Microsoft.Extensions.Configuration;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes {
    public partial class Pacientes {
        // Dependencias
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; }
        [Inject] private IDialogService _dialogoService { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }
        [Inject] private IConfiguration _configuracion { get; set; }

        // Panel de filtros
        public bool OrdenarTalla { get; set; } // Mostrar un icono u otro en ordenar por talla
        private bool FiltrosAbierto { get; set; } = false;


        // Formulario filtrado
        private List<PacienteDto>? ListaPacientes { get; set; }

        // Valores seleccionados para mostrar en los filtros
        private FiltroPacienteDto FiltrosPacientes { get; set; } = new(); // Dto con los filtros seleccionados
        private bool cargandoPacientes{ get; set; } = true;  // Cargando pacientes
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

        // Url imagenes server
        private string? UrlImagenes { get; set; }


        protected override async Task OnInitializedAsync() {
            try {
                // Obtenemos url imagenes del server
                UrlImagenes = _configuracion.GetSection("ApiSettings")["ImgUrl"];

                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 300;
                _snackbar.Configuration.HideTransitionDuration = 300;
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
                _snackbar.Configuration.ShowCloseIcon = false;


                await ObtenerFiltrosSelects();
                await ObtenerPacientes();
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
                DialogParameters parametros = new() {
                    { "ListaEpilepsias", ListaEpilepsias },
                    { "ListaMutaciones", ListaMutaciones },
                };        
                var dialog = _dialogoService.Show<CrearPaciente>("titulo", parametros ,opcionesDialogo);
                var result = await dialog.Result;

                // Validamos que el dialogo haya devuelto el nuevo paciente creado
                if (result.Cancelled == false && result.Data is PacienteDto) {
                    ListaPacientes?.Add((PacienteDto)result.Data);
                }          
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Obtener pacientes de BD
        private async Task ObtenerPacientesFiltrados() {
            try {
                ListaPacientes = await _pacientesService.FiltrarPacientes(FiltrosPacientes);
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        private async Task ObtenerPacientes() {
            try {
                // Obtenemos el listado de pacientes
                ListaPacientes = await _pacientesService.ObtenerPacientes();
                cargandoPacientes = false;

                // Validamos si la lista de pacientes no es null
                if (ListaPacientes == null) {
                    _snackbar.Add("No se han encontrado pacientes para mostrar.", Severity.Error);
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}

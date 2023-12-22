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
        private bool FiltrosAbierto { get; set; } = false;

        // Listado de objetos
        private List<CrearPacienteDto>? ListaPacientes { get; set; } = null;
        private IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        private IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;


        // Pop up crear paciente
        DialogOptions OpcionesDialogo { get; set; } = new DialogOptions{ FullWidth=true, CloseButton=true, DisableBackdropClick=true, Position=DialogPosition.Center, CloseOnEscapeKey=true};

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
                _snackbar.Add("Ha surgido un error los filtros.", Severity.Error);
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
                var dialog = _dialogoService.Show<CrearPaciente>("Nuevo Paciente", parametros ,OpcionesDialogo);
                var result = await dialog.Result;

                // Validamos que el dialogo haya devuelto el nuevo paciente creado y actualizamos la lista
                if (result.Cancelled == false && result.Data is CrearPacienteDto) {
                    ListaPacientes = await _pacientesService.AnadirPacienteALista((CrearPacienteDto)result.Data);
                }          
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Obtener pacientes de BD
        private async Task ObtenerPacientes() {
            try {
                // Obtenemos el listado de pacientes
                ListaPacientes = await _pacientesService.ObtenerPacientes();

                // Validamos si la lista de pacientes no es null
                if (ListaPacientes == null) {
                    _snackbar.Add("No se han encontrado pacientes para mostrar.", Severity.Error);
                }
            } catch (Exception) {
                _snackbar.Add("Ha surgido un error al obtener los pacientes.", Severity.Error);
                throw;
            }
        }
    }
}

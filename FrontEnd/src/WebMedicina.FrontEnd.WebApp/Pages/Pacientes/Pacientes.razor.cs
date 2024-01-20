using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.ServiceDependencies;
using MudBlazor;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal;

namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class Pacientes {
        // Dependencias
        [CascadingParameter] private Task<AuthenticationState>? AuthenticationState { get; set; }
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private IDialogService _dialogoService { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IConfiguration _configuracion { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;



        //  FILTROS
        public bool FiltrosAbierto { get; set; } = false;


        // Listado de objetos
        public List<CrearPacienteDto>? ListaPacientes { get; set; } = null;
        private IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        private IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;


        // Pop up crear paciente
        DialogOptions OpcionesDialogo { get; set; } = new DialogOptions{ FullWidth=true, CloseButton=true, DisableBackdropClick=true, Position=DialogPosition.Center, CloseOnEscapeKey=true};

        // Url imagenes server PARA LOS ICONOS
        private string? UrlImagenes { get; set; }


        // LINEA TEMPORAL
        private ContenedorLineaTemp _contenedorLineaTempRef { get; set; } = null!;


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
                _snackbar.Configuration.VisibleStateDuration = 7000;

                await ObtenerListas();
                await ObtenerPacientes();
            } catch (Exception) {
                // Creamos lista de pacientes vacia para no mostrar cargando en la tabla de pacientes
                ListaPacientes = new List<CrearPacienteDto>();
                throw;
            }
        }

        // Obtener listas de mutaciones y epilepsias
        private async Task ObtenerListas() {
            try {
                var listas = await _pacientesService.ObtenerListas();

                // Asignamos la lista de epilepsias
                ListaEpilepsias = listas.ListaEpilepsias;
                // Asignamos la lista de mutaciones
                ListaMutaciones = listas.ListaMutaciones;
                // Asignamos la lista de farmacos
                //ListaFarmacos = opcionesSelects.ListaFarmacos;
            } catch (Exception) {
                _snackbar.Add("No ha sido posible obtener los filtros.", Severity.Error);
                throw;
            }
        }

        // Mostrar dialogo para crear paciente nuevo
        private async Task MostrarCrearPac() { 
            DialogParameters parametros = new() {
                { "ListaEpilepsias", ListaEpilepsias },
                { "ListaMutaciones", ListaMutaciones },
            };        
            var dialog = _dialogoService.Show<CrearPaciente>("Nuevo Paciente", parametros ,OpcionesDialogo);
            var result = await dialog.Result;

            // Validamos que el dialogo haya devuelto el nuevo paciente creado y actualizamos la lista
            if (result.Canceled == false && result.Data is int idPaciente && idPaciente > 0) {
                ListaPacientes = await _pacientesService.AnadirPacienteALista(idPaciente);
            } 
        }

        /// <summary>
        /// Obtener pacientes de BD
        /// </summary>
        /// <returns></returns>
        private async Task ObtenerPacientes() {
            try {
                // Obtenemos el listado de pacientes
                ListaPacientes = await _pacientesService.ObtenerPacientes();

                // Validamos si la lista de pacientes no es null
                if (ListaPacientes == null) {
                    ListaPacientes = new List<CrearPacienteDto>();
                    _snackbar.Add("No se han encontrado pacientes para mostrar.", Severity.Normal);
                }
            } catch (Exception) {
                _snackbar.Add("No ha sido posible obtener los pacientes. Contacte con un administrador", Severity.Error);
                throw;
            }
        }

        /// <summary>
        /// Eliminar un paciente de la lista de pacientes
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public async Task EliminarPacienteLista(int idPaciente) { 
            ListaPacientes = await _pacientesService.EliminarPacienteLista(idPaciente); 
        }
    }
}

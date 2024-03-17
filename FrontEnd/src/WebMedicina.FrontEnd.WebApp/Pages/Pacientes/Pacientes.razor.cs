using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.ServiceDependencies;
using MudBlazor;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.FrontEnd.WebApp.Pages.Pacientes.LineaTemporal;
using System.Collections.Immutable;


namespace WebMedicina.FrontEnd.WebApp.Pages.Pacientes
{
    public partial class Pacientes {
        // Dependencias
        [CascadingParameter(Name = "modoOscuro")] bool IsDarkMode { get; set; } // Modo oscuro
        [Inject] private IPacientesService _pacientesService { get; set; } = null!;
        [Inject] private IDialogService _dialogoService { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IConfiguration _configuracion { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;
        [Inject] private IDocumentacionService _documentacionService { get; set; } = null!;



        //  FILTROS
        public bool FiltrosAbierto { get; set; } = false;
        public FiltrosPaciente RefFiltrosPac { get; set; } = new();

        // Lista pacientes mostrados y nombre de la pagina mostrada (todos o mis pacientes)
        public ExcelPacientesDto ExcelPacientes { get;  set; } = new();

        // Listado de objetos
        public ImmutableList<CrearPacienteDto>? PacientesFiltrados { get; set; }
        private IEnumerable<EpilepsiasDto>? ListaEpilepsias { get; set; } = null;
        private IEnumerable<MutacionesDto>? ListaMutaciones { get; set; } = null;


        // Pop up crear paciente
        DialogOptions OpcionesDialogo { get; set; } = new DialogOptions{ FullWidth=true, CloseButton=true, DisableBackdropClick=true, Position=DialogPosition.Center, CloseOnEscapeKey=true};
        
        private string? UrlImagenes { get; set; } // Url imagenes server PARA LOS ICONOS
        private ContenedorLineaTemp _contenedorLineaTempRef { get; set; } = null!;  // LINEA TEMPORAL


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

            // Validamos que el dialogo haya devuelto el nuevo paciente creado
            if (result.Canceled == false && result.Data is int idPaciente && idPaciente > 0) {

                // Validamos si el nuevo paciente ha sido añadido a la lista
                if(await _pacientesService.AnadirPacienteALista(idPaciente)) {

                    // Volvemos a filtrar la lista
                    await RefFiltrosPac.ObtenerPacientesFiltrados();
                }
            } 
        }

        /// <summary>
        /// Obtener pacientes de BD
        /// </summary>
        /// <returns></returns>
        private async Task ObtenerPacientes() {
            try {
                // Obtenemos el listado de pacientes
                PacientesFiltrados = await _pacientesService.ObtenerPacientes();


                // Mostramos error si no ha podido obtenerse la lista
                if (PacientesFiltrados is null) {
                    PacientesFiltrados = ImmutableList<CrearPacienteDto>.Empty;
                    _snackbar.Clear();
                    _snackbar.Add("No se han encontrado pacientes para mostrar.", Severity.Info);
                }
            } catch (Exception) {
                PacientesFiltrados = ImmutableList<CrearPacienteDto>.Empty;
                _snackbar.Add("No ha sido posible obtener los pacientes. Contacte con un administrador", Severity.Error);
            }
        }

        /// <summary>
        /// Eliminar un paciente de la lista de pacientes
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns></returns>
        public async Task EliminarPacienteLista(int idPaciente) {

            // Eliminamos el paciente de la lista
            if (await _pacientesService.EliminarPacienteLista(idPaciente)) {

                // Volvemos a filtrar la lista
                await RefFiltrosPac.ObtenerPacientesFiltrados();
            }
        }


        /// <summary>
        /// Descargar excel de pacientes
        /// </summary> 
        public async Task DescargarExcel() {
            try {
                // Validamos que haya al menos un paciente para generar el excel
                if (ExcelPacientes.Pacientes.Any()) {
                    if (!await _documentacionService.DescargarExcelPacientes(ExcelPacientes)) {
                        throw new Exception();
                    }
                } else {
                    _snackbar.Clear();
                    _snackbar.Add("Debe disponer de al menos un paciente en el listado para generar el excel", Severity.Info, config => { config.ShowCloseIcon = true; });
                }


            } catch (Exception) {
                _snackbar.Clear();
                _snackbar.Add("Error al descargar excel. Contacte con un administrador", Severity.Error);
            }
        }
    }
}

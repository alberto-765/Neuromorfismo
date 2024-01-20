using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Net;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.WebApp.Pages.Admins.PopUpCrear;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class Mutaciones {
        [Inject] private IDialogService DialogService { get; set; } = null!; // Pop up eliminar mutacion
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; } = null!;

        private MudTable<MutacionesDto> tabla = null!;
        private HttpClient Http { get; set; } = null!;
        private bool mostrarTabla { get; set; } = true; // mostrar o no la tabla de mutaciones
        private bool mostrarMutacion { get; set; } = false; // mostrar o no la formulario para editar mutacion
        private bool mostrarCargandoTabla { get; set; } = true; // mostrar cargando en la tabla
        private bool mostrarCargandoInicial { get; set; } = true; // mostrar cargando inicial mientras se obtienen datos
        private MutacionesDto mutacionSeleccionada { get; set; } = new();
        private IEnumerable<MutacionesDto> MutacionesTabla { get; set; } = null!;


        protected override async Task OnInitializedAsync() {
            try {
                Http = _crearHttpClient.CrearHttpApi(); // creamos http

                // Recargamos los elementos de la tabla 
                await RecargarElementos();

                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 300;
                _snackbar.Configuration.HideTransitionDuration = 300;
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;

                mostrarCargandoInicial = false;
            } catch (Exception) {
                mostrarCargandoInicial = false;
                mostrarTabla = false;
                throw;
            }
        }

        // Asignamos clase para la fila seleccionada
        private string SelectedRowClassFunc(MutacionesDto elemento, int row) {
            if (mostrarMutacion && mutacionSeleccionada.Equals(elemento)) {
                return "selected";
            } else {
                return string.Empty;
            }
        }
        private void RowClickEvent(TableRowClickEventArgs<MutacionesDto> elemento) {
            // Deseleccionamos si ya ha sido seleccionada anteriormente
            if (elemento.Item.Equals(mutacionSeleccionada)) {
                mutacionSeleccionada = new();
                mostrarMutacion = false;
            } else if (tabla.SelectedItem != null && tabla.SelectedItem.Equals(elemento.Item)) {
                mutacionSeleccionada = (MutacionesDto)elemento.Item.Clone() ?? new();
                mostrarMutacion = true;
            } else {
                mutacionSeleccionada = new();
                mostrarMutacion = false;
            }
        }

        // Obtenemos las mutaciones disponibles
        private async Task<IEnumerable<MutacionesDto>> ObtenerMutaciones() { 
            HttpResponseMessage respuesta = await Http.GetAsync("administracion/getmutaciones");
            if (respuesta.IsSuccessStatusCode) {
                List<MutacionesDto>? listaMutaciones = await respuesta.Content.ReadFromJsonAsync<List<MutacionesDto>>();
                if (listaMutaciones != null && listaMutaciones.Any()) {
                    return listaMutaciones;
                }
            }
            return Enumerable.Empty<MutacionesDto>(); 
        }

        // Recarga los elementos de la tabla y controla la barra de cargando
        private async Task RecargarElementos() { 
            mostrarCargandoTabla = true;
            MutacionesTabla = await ObtenerMutaciones();

            // Reseteamos la mutacion seleccionada
            mutacionSeleccionada = new();
            mostrarMutacion = false;

            if (MutacionesTabla != null && MutacionesTabla.Any()) {
                mostrarCargandoTabla = false;
                mostrarTabla = true;
            } else {
                mostrarCargandoTabla = false;
                mostrarTabla = false;
            } 
        }

        // Se abre Dialogo para crear una mutacion
        private async Task crearMutacion() {
            try { 
                // Creamos el dialogo pasandole el tipo de formulario que debe crear
                var dialogo = await DialogService.ShowAsync<DialogoCrear>("Crear Mutación");
                var resultado = await dialogo.Result;

                // Validamos que se haya creado y los campos esté correctos
                if (resultado.Canceled == false && resultado.Data != null) {
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Nueva mutación creada exitosamente";

                    string? nuevoNombre = resultado.Data.ToString();
                    if (!string.IsNullOrWhiteSpace(nuevoNombre)) {

                        // Realizamos llamada httpget para creaer la nueva mutacion
                        HttpResponseMessage respuesta = await Http.PostAsJsonAsync("administracion/crearmutacion", resultado.Data);

                        // Validamos si la respuesta es OK y ha podido ser creado
                        if (respuesta.IsSuccessStatusCode) {
                            bool recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();
                            if (recargarElementos) {
                                await RecargarElementos();
                            } else {
                                tipoSnackBar = Severity.Warning;
                                mensajeSnackBar = "La mutación no ha podido ser creada";
                            }
                        } else {
                            tipoSnackBar = Severity.Error;
                            mensajeSnackBar = await respuesta.Content.ReadAsStringAsync() ?? "Error interno del servidor";
                        }
                    } else {
                        tipoSnackBar = Severity.Info;
                        mensajeSnackBar = "El nuevo nombre no puede estar vacío";
                    }
                    _snackbar.Add(mensajeSnackBar, tipoSnackBar);
                }
            } catch (Exception) {
                mostrarCargandoInicial = false;
                mostrarTabla = false;
                throw;
            }
        }

        // Eliminar mutacion
        private async Task EliminarMutacion() {
            try { 
                bool? result = await DialogService.ShowMessageBox(
                    "Eliminar Mutacion",
                    (MarkupString)$"¿Estás seguro de eliminar el tipo: <b>{mutacionSeleccionada.Nombre}</b>?",
                    yesText: "Sí", cancelText: "No");

                if (result == true) {
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Mutacion eliminada exitosamente";

                    // Realizamos llamada httpget 
                    HttpResponseMessage respuesta = await Http.DeleteAsync($"administracion/eliminarmutacion/{mutacionSeleccionada.IdMutacion}");

                    // Validamos si la respuesta es OK y ha podido ser eliminado
                    if (respuesta.IsSuccessStatusCode) {
                        bool recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();
                        if (recargarElementos) {
                            await RecargarElementos();
                        } else {
                            tipoSnackBar = Severity.Warning;
                            mensajeSnackBar = "La mutación no ha podido ser eliminada";
                        }
                    } else {
                        tipoSnackBar = Severity.Error;
                        mensajeSnackBar = await respuesta.Content.ReadAsStringAsync() ?? "Error interno del servidor";
                    }
                    _snackbar.Add(mensajeSnackBar, tipoSnackBar);
                }
            } catch (Exception) {
                mostrarCargandoInicial = false;
                mostrarTabla = false;
                throw;
            }
        }


        //Editar mutacion
        private async Task EditarMutacion(EditContext context) {
            try { 
                if (context.IsModified()) {
                    MutacionesDto mut = (MutacionesDto)context.Model;
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Mutacion editada exitosamente";

                    // Realizamos llamada httpget 
                    HttpResponseMessage respuesta = await Http.PutAsJsonAsync("administracion/updatemutacion", mut);

                    // Validamos si la respuesta es OK y ha podido ser editado
                    if (respuesta.IsSuccessStatusCode) {
                        bool recargarElementos = false;

                        // Validamos si es de tipo NoContent
                        if (respuesta.StatusCode != HttpStatusCode.NoContent) {
                            recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();
                            if (recargarElementos) {
                                await RecargarElementos();
                            } 
                        } 

                        // Mostramos snackbar con mensaje
                        if (!recargarElementos) {
                            tipoSnackBar = Severity.Warning;
                            mensajeSnackBar = "La mutación no ha podido ser editada";
                        }
                    } else {
                        tipoSnackBar = Severity.Error;
                        mensajeSnackBar = await respuesta.Content.ReadAsStringAsync() ?? "Error interno del servidor";
                    }
                    _snackbar.Add(mensajeSnackBar, tipoSnackBar);
                }
            } catch (Exception) {
                mostrarCargandoInicial = false;
                mostrarTabla = false;
                throw;
            }
        }
    }
}


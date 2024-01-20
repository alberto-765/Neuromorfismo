using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.Net;
using System.Net.Http.Json;
using System.Security.Claims;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.WebApp.Pages.Admins.PopUpCrear;
using WebMedicina.Shared.Dto.Tipos;
using static MudBlazor.CategoryTypes;
using static System.Net.WebRequestMethods;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class Epilepsias {
        [Inject] private IDialogService DialogService { get; set; } = null!; // Pop up eliminar epilepsia
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; } = null!;

        private MudTable<EpilepsiasDto> tabla = null!;
        private HttpClient Http { get; set; } = null!;
        private bool mostrarTabla { get; set; } = true; // mostrar o no la tabla de epilepsias
        private bool mostrarEpilepsia { get; set; } = false; // mostrar o no la formulario para editar epilepsia
        private bool mostrarCargandoTabla { get; set; } = true; // mostrar cargando en la tabla
        private bool mostrarCargandoInicial { get; set; } = true; // mostrar cargando inicial mientras se obtienen datos
        private EpilepsiasDto epilepsiaSeleccionada { get; set; } = new();
        private IEnumerable<EpilepsiasDto> EpilepsiasTabla { get; set; } = null!;


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
        private string SelectedRowClassFunc(EpilepsiasDto elemento, int row) { 
            if (mostrarEpilepsia && epilepsiaSeleccionada.Equals(elemento)) {
                return "selected";
            } else {
                return string.Empty;
            } 
        }
        private void RowClickEvent(TableRowClickEventArgs<EpilepsiasDto> elemento) { 
            // Deseleccionamos si ya ha sido seleccionada anteriormente
            if (elemento.Item.Equals(epilepsiaSeleccionada)) {
                epilepsiaSeleccionada = new();
                mostrarEpilepsia = false;
            } else if (tabla.SelectedItem != null && tabla.SelectedItem.Equals(elemento.Item)) {
                epilepsiaSeleccionada = (EpilepsiasDto)elemento.Item.Clone() ?? new();
                mostrarEpilepsia = true;
            } else {
                epilepsiaSeleccionada = new();
                mostrarEpilepsia = false;
            } 
        }

        // Obtenemos las epilepsias disponibles
        private async Task<IEnumerable<EpilepsiasDto>> ObtenerEpilepsias() { 
            HttpResponseMessage respuesta = await Http.GetAsync("administracion/getepilepsias");
            if(respuesta.IsSuccessStatusCode) {
                List<EpilepsiasDto>? listaEpilepsias = await respuesta.Content.ReadFromJsonAsync<List<EpilepsiasDto>>();
                if(listaEpilepsias != null && listaEpilepsias.Any()) {
                    return listaEpilepsias;
                }
            }
            return Enumerable.Empty<EpilepsiasDto>(); 
        }

        // Recarga los elementos de la tabla y controla la barra de cargando
        private async Task RecargarElementos() { 
            mostrarCargandoTabla = true;
            EpilepsiasTabla = await ObtenerEpilepsias();

            // Reseteamos la epilepsia seleccionada
            epilepsiaSeleccionada = new();
            mostrarEpilepsia = false;

            if (EpilepsiasTabla != null && EpilepsiasTabla.Any()) {
                mostrarCargandoTabla = false;
                mostrarTabla = true;
            }else {
                mostrarCargandoTabla = false;
                mostrarTabla = false;
            } 
        }

        // Se abre Dialogo para crear una epilepsia
        private async Task crearEpilepsia() {
            try {
                // Creamos el dialogo pasandole el tipo de formulario que debe crear
                var dialogo = await DialogService.ShowAsync<DialogoCrear>("Crear Epilepsia");
                var resultado = await dialogo.Result;

                // Validamos que se haya creado y los campos esté correctos
                if (resultado.Canceled == false && resultado.Data != null) {
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Nueva epilepsia creada exitosamente";

                    string? nuevoNombre = resultado.Data.ToString();
                    if (!string.IsNullOrWhiteSpace(nuevoNombre)) {

                        // Realizamos llamada httpget para creaer la nueva epilepsia
                        HttpResponseMessage respuesta = await Http.PostAsJsonAsync("administracion/crearepilepsia", resultado.Data);

                        // Validamos si la respuesta es OK y ha podido ser creado
                        if (respuesta.IsSuccessStatusCode) {
                            if (await respuesta.Content.ReadFromJsonAsync<bool>()) {
                                await RecargarElementos();
                            } else {
                                tipoSnackBar = Severity.Warning;
                                mensajeSnackBar = "La epilepsia no ha podido ser creada";
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
                mostrarCargandoTabla = false;
                mostrarTabla = false;
                throw;
            }
        }

        // Eliminar epilepsia
        private async Task EliminarEpilepsia() {
            try { 
                bool? result = await DialogService.ShowMessageBox(
                    "Eliminar Epilepsia",
                    (MarkupString) $"¿Estás seguro de eliminar <b>{epilepsiaSeleccionada.Nombre}</b>?",
                    yesText: "Sí", cancelText: "No");

                if (result == true) {
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Epilepsia eliminada exitosamente";

                    // Realizamos llamada httpget 
                    HttpResponseMessage respuesta = await Http.DeleteAsync($"administracion/eliminarepilepsia/{epilepsiaSeleccionada.IdEpilepsia}");

                    // Validamos si la respuesta es OK y ha podido ser eliminado
                    if (respuesta.IsSuccessStatusCode) {
                        bool recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();
                        if (recargarElementos) {
                            await RecargarElementos();
                        } else { 
                            tipoSnackBar = Severity.Warning;
                            mensajeSnackBar = "La epilepsia no ha podido ser eliminada";
                        }
                    } else {
                        tipoSnackBar = Severity.Error;
                        mensajeSnackBar = await respuesta.Content.ReadAsStringAsync() ?? "Error interno del servidor";
                    }
                    _snackbar.Add(mensajeSnackBar, tipoSnackBar);
                }
            } catch (Exception) {
                mostrarCargandoTabla = false;
                mostrarTabla = false;
                throw;
            }
        }

        // Editar epilepsia
        private async Task EditarEpilepsia(EditContext context) {
            try { 
                if (context.IsModified()) {
                    EpilepsiasDto ep = (EpilepsiasDto) context.Model;
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Epilepsia editada exitosamente";

                    // Realizamos llamada httpget 
                    HttpResponseMessage respuesta = await Http.PutAsJsonAsync("administracion/updateepilepsia", ep);

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
                            mensajeSnackBar = "La epilepsia no ha podido ser editada";
                        }
                    } else {
                        tipoSnackBar = Severity.Error;
                        mensajeSnackBar = await respuesta.Content.ReadAsStringAsync() ?? "Error interno del servidor";
                    }
                    _snackbar.Add(mensajeSnackBar, tipoSnackBar);
                }
            } catch (Exception) {
                mostrarCargandoTabla = false;
                mostrarTabla = false;
                throw;
            }
        }
    }
}

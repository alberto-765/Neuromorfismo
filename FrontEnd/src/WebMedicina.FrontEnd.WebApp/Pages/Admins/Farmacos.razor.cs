using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Net;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.FrontEnd.WebApp.Pages.Admins.PopUpCrear;
using WebMedicina.FrontEnd.Service;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class Farmacos {
        [Inject] private IDialogService DialogService { get; set; } = null!;// Pop up eliminar farmaco
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; } = null!;

        private MudTable<FarmacosDto> tabla = null!;
        private HttpClient Http { get; set; } = null!;
        private bool mostrarTabla { get; set; } = true; // mostrar o no la tabla de farmacos
        private bool mostrarFarmaco { get; set; } = false; // mostrar o no la formulario para editar Mutacion
        private bool mostrarCargandoTabla { get; set; } = true; // mostrar cargando en la tabla
        private FarmacosDto farmacoSeleccionado { get; set; } = new();
        private IEnumerable<FarmacosDto> FarmacosTabla { get; set; } = Enumerable.Empty<FarmacosDto>();

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
            } catch (Exception) {
                mostrarTabla = false;
                throw;
            }
        }

        // Asignamos clase para la fila seleccionada
        private string SelectedRowClassFunc(FarmacosDto elemento, int row) {
            if (mostrarFarmaco && farmacoSeleccionado.Equals(elemento)) {
                return "selected";
            } else {
                return string.Empty;
            }
        }
        private void RowClickEvent(TableRowClickEventArgs<FarmacosDto> elemento) { 
            // Deseleccionamos si ya ha sido seleccionada anteriormente
            if (elemento.Item.Equals(farmacoSeleccionado)) {
                farmacoSeleccionado = new();
                mostrarFarmaco = false;
            } else if (tabla.SelectedItem != null && tabla.SelectedItem.Equals(elemento.Item)) {
                farmacoSeleccionado = (FarmacosDto)elemento.Item.Clone() ?? new();
                mostrarFarmaco = true;
            } else {
                farmacoSeleccionado = new();
                mostrarFarmaco = false;
            } 
        }

        // Obtenemos las farmacos disponibles
        private async Task<IEnumerable<FarmacosDto>> ObtenerFarmacos() { 
            HttpResponseMessage respuesta = await Http.GetAsync("administracion/getfarmacos");
            if (respuesta.IsSuccessStatusCode) {
                List<FarmacosDto>? listaFarmacos = await respuesta.Content.ReadFromJsonAsync<List<FarmacosDto>>();
                if (listaFarmacos != null && listaFarmacos.Any()) {
                    return listaFarmacos;
                }
            }
            return Enumerable.Empty<FarmacosDto>(); 
        }

        // Recarga los elementos de la tabla y controla la barra de cargando
        private async Task RecargarElementos() {
            // Mostramos la tabla y el loading
            mostrarCargandoTabla = true;
            mostrarTabla = true;

            // Obtenemos las epilepsias
            FarmacosTabla = await ObtenerFarmacos();

            // Reseteamos la epilepsia seleccionada
            farmacoSeleccionado = new();
            mostrarFarmaco = false;

            // Ocultamos la tabla si no contiene elementos
            if (FarmacosTabla is null || !FarmacosTabla.Any()) {
                mostrarTabla = false;
            }

            // Desactivamos el cargando tabla
            mostrarCargandoTabla = false;
        }

        // Se abre Dialogo para crear una farmaco
        private async Task crearFarmaco() {
            try {
                // Creamos el dialogo pasandole el tipo de formulario que debe crear
                var dialogo = await DialogService.ShowAsync<DialogoCrear>("Crear Farmaco");
                var resultado = await dialogo.Result;

                // Validamos que se haya creado y los campos esté correctos
                if (resultado.Canceled == false && resultado.Data != null) {
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Nuevo fármaco creada exitosamente";

                    string? nuevoNombre = resultado.Data.ToString();
                    if (!string.IsNullOrWhiteSpace(nuevoNombre)) {

                        // Realizamos llamada httpget para creaer la nueva farmaco
                        HttpResponseMessage respuesta = await Http.PostAsJsonAsync("administracion/crearfarmaco", resultado.Data);

                        // Validamos si la respuesta es OK y ha podido ser creado
                        if (respuesta.IsSuccessStatusCode) {
                            bool recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();

                            if (recargarElementos) {
                                await RecargarElementos();
                            } else {
                                tipoSnackBar = Severity.Warning;
                                mensajeSnackBar = "El fármaco no ha podido ser crado";
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

        // Eliminar farmaco
        private async Task EliminarFarmaco() {
            try {
                bool? result = await DialogService.ShowMessageBox(
                    "Eliminar Fármaco",
                    (MarkupString)$"¿Estás seguro de eliminar <b>{farmacoSeleccionado.Nombre}</b>?",
                    yesText: "Sí", cancelText: "No");

                if (result == true) {
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Fármaco eliminada exitosamente";

                    // Realizamos llamada httpget 
                    HttpResponseMessage respuesta = await Http.DeleteAsync($"administracion/eliminarfarmaco/{farmacoSeleccionado.IdFarmaco}");

                    // Validamos si la respuesta es OK y ha podido ser eliminado
                    if (respuesta.IsSuccessStatusCode) {
                        bool recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();
                        if (recargarElementos) {
                            await RecargarElementos();
                        } else {
                            tipoSnackBar = Severity.Warning;
                            mensajeSnackBar = "La epilepsia no ha podido ser eliminado";
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


        //Editar farmaco
        private async Task EditarMutacion(EditContext context) {
            try {
                if (context.IsModified()) {
                    FarmacosDto far = (FarmacosDto)context.Model;
                    Severity tipoSnackBar = Severity.Success; // Tipo de snackbar para mensaje
                    string mensajeSnackBar = "Fármaco editada exitosamente";
                    
                    // Realizamos llamada httpget 
                    HttpResponseMessage respuesta = await Http.PutAsJsonAsync("administracion/updatefarmaco", far);

                    // Validamos si la respuesta es OK y ha podido ser editado
                    if (respuesta.IsSuccessStatusCode) {
                        bool recargarElementos = false;

                        // Validamos si es de tipo NoContent
                        if (respuesta.StatusCode != HttpStatusCode.NoContent) {
                            recargarElementos = await respuesta.Content.ReadFromJsonAsync<bool>();
                            if (recargarElementos) {
                                await RecargarElementos();
                                recargarElementos = true;
                            } 
                        } 

                        // Mostramos snackbar con mensaje
                        if (!recargarElementos) {
                            tipoSnackBar = Severity.Warning;
                            mensajeSnackBar = "El farmaco no ha podido ser editado";
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


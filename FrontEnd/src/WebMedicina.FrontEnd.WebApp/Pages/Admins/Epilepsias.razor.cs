using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using System.Net.Http.Json;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
using static MudBlazor.CategoryTypes;
using static System.Net.WebRequestMethods;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins {
    public partial class Epilepsias {
        private int numeroFilaSeleccionada = -1;
        private MudTable<EpilepsiasDto> tabla;
        private EpilepsiasDto? epilepsiaSeleccionada { get; set; } = null;
        private IEnumerable<EpilepsiasDto> EpilepsiasTabla { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }
        private bool cargando { get; set; } = false;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionDto excepcionPersonalizada { get; set; }
        [Inject] IDialogService DialogService { get; set; }


        protected override async Task OnInitializedAsync() {
            try {
                Http = _crearHttpClient.CrearHttp(); // creamos http

                EpilepsiasTabla = await ObtenerEpilepsias();


                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 300;
                _snackbar.Configuration.HideTransitionDuration = 300;

            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        // Asignamos clase para la fila seleccionada
        private string SelectedRowClassFunc(EpilepsiasDto elemento, int numFila) {
            if (numeroFilaSeleccionada == numFila) {
                numeroFilaSeleccionada = -1;
                epilepsiaSeleccionada = null;
                return string.Empty;
            } else if (tabla.SelectedItem != null && tabla.SelectedItem.Equals(elemento)) {
                numeroFilaSeleccionada = numFila;
                epilepsiaSeleccionada = elemento;
                return "selected";
            } else {
                epilepsiaSeleccionada = null;
                return string.Empty;
            }
        }

        // Obtenemos las epilepsias disponibles
        private async Task<IEnumerable<EpilepsiasDto>> ObtenerEpilepsias() {
            try {
                HttpResponseMessage respuesta = await Http.GetAsync("administracion/getEpilepsias");
                if(respuesta.IsSuccessStatusCode) {
                    List<EpilepsiasDto>? listaEpilepsias = await respuesta.Content.ReadFromJsonAsync<List<EpilepsiasDto>>();
                    if(listaEpilepsias != null && listaEpilepsias.Any()) {
                        return listaEpilepsias;
                    }
                }
                return Enumerable.Empty<EpilepsiasDto>();
            } catch (Exception ex) {
                return Enumerable.Empty<EpilepsiasDto>();
                throw;
            }
        }

        // Se abre Dialogo para crear una epilepsia
        private async Task crearEpilepsia() {

            CrearEpilepsia crearEpilepsia = new CrearEpilepsia();
            var parameters = new DialogParameters<DialogoCrear> { { "formularioCrear", crearEpilepsia } };
            var dialogo = await DialogService.ShowAsync<DialogoCrear>("Delete", parameters);
            var resutlado = await dialogo.Result;
            //if(resutlado.Data) { 
            //}
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins {
    public partial class GestionUsers {
        // Tooltip de info para el usuario, donde podra ver los permisos que tiene
        private MarkupString tooltipInfoUser { get; set; }
        private bool mostrarTooltip { get; set; }
        [Inject] IRedirigirManager redirigirManager { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionDto excepcionPersonalizada { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        private ClaimsPrincipal? user { get; set; }
        private ReadOnlyDictionary<string, string> _filtros { get; set; }
        [Inject] IJSRuntime js { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }

        protected override async Task OnInitializedAsync() {
            Http = _crearHttpClient.CrearHttp();

            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;

                // Generamos el texto para el tooltip
                await GenerarTooltip();
            }

            // rellenamos los filtros
            _filtros = await ObtenerFiltros();

            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.VisibleStateDuration = 5000;
            _snackbar.Configuration.MaximumOpacity = 0;
        }


        private async Task GenerarTooltip() {

            // Por defecto el rol será medico
            string role = "medico";
            if (user is not null) {
                if (user.IsInRole("superAdmin")) {
                    role = "superAdmin";
                } else if(user.IsInRole("admin")) {
                    role = "admin";
                }
            }

            switch (role) {
                case "superAdmin":
                tooltipInfoUser = new MarkupString($"<div style='text-align: left;'> Usted tiene permisos de Super Admin. <br />" +
                    $"Puede <b>crear, editar y eliminar</b> usuarios de tipo: <br/>" +
                    $"<ul style='padding-left: 15px;'><li>- Administradores</li><li>- Médicos</li></ul></div>");
                    mostrarTooltip = true;
                break;
                case "admin":
                tooltipInfoUser = new MarkupString($"<div style='text-align: left;'> Usted tiene permisos de Administrador. <br />" +
                 $"Puede <b>crear, editar y eliminar</b> usuarios de tipo: <br/>" +
                 $"<ul style='padding-left: 15px;'><li>- Médicos</li></ul></div>");
                mostrarTooltip = true;
                break;
                default:
                    mostrarTooltip = false;
                break;
            }
        }

        // FUNCIONES PARA TABLA TIPO SERVER
        private IEnumerable<UserInfoDto> pagedData { get; set; }
        private MudTable<UserInfoDto> table { get; set; }

        private int totalItems;
        private string searchString = null;

        /// <summary>
        /// Here we simulate getting the paged, filtered and ordered data from the server
        /// </summary>
        private async Task<TableData<UserInfoDto>> ServerReload(TableState state) {
            var responseMessage = await Http.PostAsJsonAsync("gestionUsers/obtenerUsuariosFiltrados", _filtros);
            List<UserInfoDto>? list = new List<UserInfoDto>();
            if(responseMessage.IsSuccessStatusCode) {
                list = await responseMessage.Content.ReadFromJsonAsync<List<UserInfoDto>>();

                if(list is not null && list.Count > 0) {
                    totalItems = list.Count;
                    pagedData = list.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
                } else {
                    totalItems = 0;
                    pagedData = Enumerable.Empty<UserInfoDto>();
                }
            } else {
                _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                _snackbar.Add(await responseMessage.Content.ReadAsStringAsync(), Severity.Error);
            }

            // Saltamos los items de la paginación y obtenemos el maximo que se puede mostrar
            return new TableData<UserInfoDto>() { TotalItems = totalItems, Items = pagedData };
        }

        private void OnSearch(string text) {
            searchString = text;
            table.ReloadServerData();
        }


        // FILTROS PARA LA BUSQUEDA

        // Buscamos los filtros en la sesion y si no hay devolvemos unos nuevos
        public async Task<ReadOnlyDictionary<string, string>> ObtenerFiltros() {
            try {
                // Obtenemos los filtros de session
                var filtrosSession = await js.GetFromSessionStorage("filtrosGestionUsers");

                // Comprobamos que hay filtros almacenados y si no devolvemos los filtros por defecto
                if(!string.IsNullOrEmpty(filtrosSession)) {
                    ReadOnlyDictionary<string, string> filros = JsonSerializer.Deserialize<ReadOnlyDictionary<string, string>>(filtrosSession);

                    if(filros is not null && filros.Count > 0) {
                        return filros;
                    }
                }
                return new ReadOnlyDictionary<string, string>(await CrearDiccionarioFiltros());
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                return new ReadOnlyDictionary<string, string>(await CrearDiccionarioFiltros());
            }
        }

        private async Task<Dictionary<string, string>> CrearDiccionarioFiltros() {
            return new Dictionary<string, string> {
                { "busqueda" , "" },
                {"campoOrdenar" , "" },
                {"direccionOrdenar", ""} ,
                {"rol" , "" }
            };
        }
    }
}

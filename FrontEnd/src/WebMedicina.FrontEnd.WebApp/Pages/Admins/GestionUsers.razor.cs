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
        private IReadOnlyDictionary<string, string> _filtros { get; set; } = ObtenerFiltros();
        [Inject] IJSRuntime js { get; set; }

        protected override async Task OnInitializedAsync() {
            Http = _crearHttpClient.CrearHttp();

            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;

                // Generamos el texto para el tooltip
                await GenerarTooltip();
            }
        }


        private async Task GenerarTooltip() {

            // Por defecto el rol será medico
            string role = "medico";
            if (user.IsInRole("superAdmin")) {
                role = "superAdmin";
            } else if(user.IsInRole("admin")) {
                role = "admin";
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
            IEnumerable<UserInfoDto> data = await Http.PostAsJsonAsync<List<UserInfoDto>>("webapi/periodictable");
            data = data.Where(UserInfoDto => {
                if (string.IsNullOrWhiteSpace(searchString))
                    return true;
                if (UserInfoDto.NumHistoria.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if (UserInfoDto.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    return true;
                if ($"{UserInfoDto.Number} {UserInfoDto.Position} {UserInfoDto.Molar}".Contains(searchString))
                    return true;
                return false;
            }).ToArray();
            totalItems = data.Count();
            switch (state.SortLabel) {
                case "nr_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Number);
                break;
                case "sign_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Sign);
                break;
                case "name_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Name);
                break;
                case "position_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Position);
                break;
                case "mass_field":
                data = data.OrderByDirection(state.SortDirection, o => o.Molar);
                break;
            }

            pagedData = data.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
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
                return new ReadOnlyDictionary<string, string>(CrearDiccionarioFiltros());
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        private Dictionary<string, string> CrearDiccionarioFiltros() {
            return new Dictionary<string, string> {   { "busqueda", "" },
                    { "busqueda", "" },
                    { "campoOrdenar", "" },
                    { "direccionOrdenar", "" },
                    { "tipoUser", "" },
            };
        }
    }
}

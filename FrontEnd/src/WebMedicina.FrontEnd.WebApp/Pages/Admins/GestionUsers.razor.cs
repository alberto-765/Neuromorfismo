using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using MudBlazor;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Security.Claims;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class GestionUsers {
        // DEPENDENCIAS
        [Inject] private IAdminsService _adminsService { get; set; } = null!;
        [Inject] private IRedirigirManager redirigirManager { get; set; } = null!;
        [CascadingParameter(Name = "modoOscuro")] private bool _isDarkMode { get; set; } // Modo oscuro
        [Inject] private ICrearHttpClient _crearHttpClient { get; set; } = null!;
        [Inject] private ISnackbar _snackbar { get; set; } = null!;     
        [Inject] private IJSRuntime js { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private IComun _comun { get; set; } = null!;


        private HttpClient Http { get; set; } = null!;
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }

        private MarkupString tooltipInfoUser;  // Tooltip de info para el usuario, donde podra ver los permisos que tiene
        private bool mostrarTooltip; // Mostrar o no el tooltip de informacion
        private ClaimsPrincipal? user { get; set; } // datos del usuario logueado

        // TABLAS
        private IEnumerable<UserUploadDto> pagedData { get; set; } = null!; // datos que se muestran en la tabla
        private MudTable<UserUploadDto> table { get; set; } = null!; // referencia de la tabla
        private int totalItems; // ((UserUploadDto) item)s totales obtenidos
        private string searchString = string.Empty; // texto por el que se está buscando
        private UserUploadDto copiaSeguridadUsuario { get; set; } = null!; // copia de seguridad de un elemento editado

        protected override async Task OnInitializedAsync() { 
            Http = _crearHttpClient.CrearHttpApi();

            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;

                // Generamos el texto para el tooltip
                if (user is not null) { 
                    _adminsService.GenerarTooltipInfoUser(user, ref tooltipInfoUser, ref mostrarTooltip);
                }
            }

            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
            _snackbar.Configuration.VisibleStateDuration = 5000;
            _snackbar.Configuration.ShowCloseIcon = false; 
        }

        // FUNCIONES PARA TABLA TIPO SERVER
        private async Task<TableData<UserUploadDto>> ServerReload(TableState state) { 
            // Rellenamos campos para fitrado
            FiltradoTablaDefaultDto camposFiltrado = new() {
                Page = state.Page,
                PageSize = state.PageSize,
                SortDirection = (int)state.SortDirection,
                SearchString = searchString,
                SortLabel = state.SortLabel
            };

            // Llamamos a la api para obtener de BBDD los usuarios con los filtros
            HttpResponseMessage responseMessage = await Http.PostAsJsonAsync("gestionusers/obtenerusuariosfiltrados", camposFiltrado);
            List<UserUploadDto>? list;
            if(responseMessage.IsSuccessStatusCode) {
                if (responseMessage.StatusCode != HttpStatusCode.NoContent) {
                    list = await responseMessage.Content.ReadFromJsonAsync<List<UserUploadDto>>();

                    // Comprobamos que la lista no sea nula
                    if (list is not null && list.Any()) {
                        totalItems = list.Count;
                        pagedData = list;
                    } else {
                        totalItems = 0;
                        pagedData = Enumerable.Empty<UserUploadDto>();
                    }
                } else {
                    totalItems = 0;
                    pagedData = Enumerable.Empty<UserUploadDto>();
                }
            } 

            // Saltamos los ((UserUploadDto) item)s de la paginación y obtenemos el maximo que se puede mostrar
            return new TableData<UserUploadDto>() { TotalItems = totalItems, Items = pagedData }; 
        }


        // FUNCIONES PARA TABLA EDITABLE

        // Creamos copia de seguridad del userInfo que se esta editando 
        private void CopiaSeguridadItem(object item) {
            copiaSeguridadUsuario = new() {
                IdMedico = ((UserUploadDto) item).IdMedico,
                UserLogin = ((UserUploadDto) item).UserLogin,
                Nombre = ((UserUploadDto) item).Nombre,
                Apellidos = ((UserUploadDto) item).Apellidos,
                FechaNac = ((UserUploadDto) item).FechaNac,
                FechaCreac = ((UserUploadDto) item).FechaCreac,
                FechaUltMod = ((UserUploadDto) item).FechaUltMod,
                Rol = ((UserUploadDto) item).Rol,
                Sexo = ((UserUploadDto) item).Sexo
            };
        }

        // Reseteamos el userInfo a sus valores por defecto
        private void ResetearUserInfo(object item) {
            item = new UserUploadDto() {
                IdMedico = copiaSeguridadUsuario.IdMedico,
                UserLogin = copiaSeguridadUsuario.UserLogin,
                Nombre = copiaSeguridadUsuario.Nombre,
                Apellidos = copiaSeguridadUsuario.Apellidos,
                FechaNac = copiaSeguridadUsuario.FechaNac,
                Rol = copiaSeguridadUsuario.Rol,
                Sexo = copiaSeguridadUsuario.Sexo
            };
        }

        // Actualizamos los datos del usuario editado
        private async void ActualizarUsuario(object item) {
            try {
                // Generamos el usuario actualizado
                UserUploadDto userInfo = new() {
                    IdMedico = ((UserUploadDto)item).IdMedico,
                    UserLogin = ((UserUploadDto)item).UserLogin,
                    Nombre = ((UserUploadDto)item).Nombre,
                    Apellidos = ((UserUploadDto)item).Apellidos,
                    FechaNac = ((UserUploadDto)item).FechaNac,
                    Rol = ((UserUploadDto)item).Rol,
                    Sexo = ((UserUploadDto)item).Sexo
                };

                // Validamos los campos del nuevo usuario
                List<ValidationResult> errores = new ();
                Validator.TryValidateObject(userInfo, new ValidationContext(userInfo), errores, true);

                // Validamos si el usuario actualizado es correcto
                if (errores.Any()) {
                    MostrarMensajeError(errores);
                } else {
                    LLamadaUploadUserDto usuarioHttp = new() {
                        usuario = userInfo
                    };

                    // Comprobamos si se ha modificado el rol
                    if (userInfo.Rol != copiaSeguridadUsuario.Rol) {
                        usuarioHttp.rolModificado = true;
                    }

                    HttpResponseMessage httpResponseMessage = await Http.PutAsJsonAsync("gestionusers/actualizarusuario", usuarioHttp);

                    // Verificamos si el medico se ha editado correctamente
                    if (httpResponseMessage.IsSuccessStatusCode) {
                        _snackbar.Add("Médico editado correctamente", Severity.Success);
                    } else {
                        // Obtenemos el error del server
                        string error = await httpResponseMessage.Content.ReadAsStringAsync();

                        if(error.Length < 46) {
                            _snackbar.Add(error, Severity.Warning);
                        } else {
                            _snackbar.Add("Error al editar al médico", Severity.Warning);
                        }
                    }
                }

            } catch (Exception) {
                _snackbar.Add("Error al editar al médico", Severity.Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="errores"></param>
        private async void MostrarMensajeError(List<ValidationResult> errores) {
            await DialogService.ShowMessageBox(
                "Error al editar usuario",
                new MarkupString(_comun.GenerarHtmlErrores(errores)),
                yesText: "Entendido");
        }        

        // Filtrar por search
        private void OnSearch(string text) {
            searchString = text;
            table.ReloadServerData();
        }
    }
}

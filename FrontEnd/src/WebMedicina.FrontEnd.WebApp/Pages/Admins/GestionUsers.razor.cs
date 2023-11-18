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
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins {
    public partial class GestionUsers {
        // DEPENDENCIAS
        [Inject] IAdminsService _adminsService { get; set; }
        [Inject] IRedirigirManager redirigirManager { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        [Inject] private ISnackbar _snackbar { get; set; }
        [Inject] IJSRuntime js { get; set; }
        [Inject] private IDialogService DialogService { get; set; }


        private HttpClient Http { get; set; }
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }

        private MarkupString tooltipInfoUser;  // Tooltip de info para el usuario, donde podra ver los permisos que tiene
        private bool mostrarTooltip; // Mostrar o no el tooltip de informacion
        private ClaimsPrincipal? user { get; set; } // datos del usuario logueado

        // TABLAS
        private ReadOnlyDictionary<string, string> _filtros { get; set; } // filtros aplicados
        private IEnumerable<UserUploadDto> pagedData { get; set; } // datos que se muestran en la tabla
        private MudTable<UserUploadDto> table { get; set; } // referencia de la tabla
        private int totalItems; // ((UserUploadDto) item)s totales obtenidos
        private string searchString = string.Empty; // texto por el que se está buscando
        private UserUploadDto copiaSeguridadUsuario { get; set; } // copia de seguridad de un elemento editado
        private MudDatePicker _picker { get; set; }

        protected override async Task OnInitializedAsync() {
            try {
                Http = _crearHttpClient.CrearHttp();

                if (authenticationState is not null) {
                    var authState = await authenticationState;
                    user = authState?.User;

                    // Generamos el texto para el tooltip
                    _adminsService.GenerarTooltipInfoUser(user, ref tooltipInfoUser, ref mostrarTooltip);
                }

                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.VisibleStateDuration = 5000;
                _snackbar.Configuration.ShowCloseIcon = false;
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        // FUNCIONES PARA TABLA TIPO SERVER
        private async Task<TableData<UserUploadDto>> ServerReload(TableState state) {
            try {
                if (_filtros is null || _filtros.Count == 0) {
                    // rellenamos los filtros
                    _filtros = await _adminsService.ObtenerFiltrosSession();
                }

                // Llamamos a la api para obtener de BBDD los usuarios con los filtros
                var responseMessage = await Http.PostAsJsonAsync("gestionUsers/obtenerUsuariosFiltrados", _filtros);
                List<UserUploadDto>? list = new ();
                if(responseMessage.IsSuccessStatusCode) {
                    if (responseMessage.StatusCode != HttpStatusCode.NoContent) {
                        list = await responseMessage.Content.ReadFromJsonAsync<List<UserUploadDto>>();

                        // Comprobamos que la lista no sea nula
                        if (list is not null && list.Any()) {
                            totalItems = list.Count;
                            pagedData = list.Skip(state.Page * state.PageSize).Take(state.PageSize).ToArray();
                        } else {
                            totalItems = 0;
                            pagedData = Enumerable.Empty<UserUploadDto>();
                        }
                    } else {
                        totalItems = 0;
                        pagedData = Enumerable.Empty<UserUploadDto>();
                    }
                } else {
                    throw new  Exception();
                }

                // Saltamos los ((UserUploadDto) item)s de la paginación y obtenemos el maximo que se puede mostrar
                return new TableData<UserUploadDto>() { TotalItems = totalItems, Items = pagedData };
            } catch (Exception ex) {
                _snackbar.Add("Error al obtener filtrado de usuarios", Severity.Error);

                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                return new TableData<UserUploadDto>();
            }
        }

        private void OnSearch(string text) {
            searchString = text;
            table.ReloadServerData();
        }


        // FUNCIONES PARA TABLA EDITABLE

        // Creamos copia de seguridad del userInfo que se esta editando 
        private void CopiaSeguridadItem(object item) {
            copiaSeguridadUsuario = new() {
                NumHistoria = ((UserUploadDto) item).NumHistoria,
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
            ((UserUploadDto)item).NumHistoria = copiaSeguridadUsuario.NumHistoria;
            ((UserUploadDto)item).Nombre = copiaSeguridadUsuario.Nombre;
            ((UserUploadDto)item).Apellidos = copiaSeguridadUsuario.Apellidos;
            ((UserUploadDto)item).FechaNac = copiaSeguridadUsuario.FechaNac;
            ((UserUploadDto)item).FechaCreac = copiaSeguridadUsuario.FechaCreac;
            ((UserUploadDto)item).FechaUltMod = copiaSeguridadUsuario.FechaUltMod;
            ((UserUploadDto)item).Rol = copiaSeguridadUsuario.Rol;
            ((UserUploadDto)item).Sexo = copiaSeguridadUsuario.Sexo;
        }

        // Actualizamos los datos del usuario editado
        private async void ActualizarUsuario(object item) {
            try {
                // Generamos el usuario actualizado
                UserUploadDto userInfo = new() {
                    NumHistoria = ((UserUploadDto)item).NumHistoria,
                    Nombre = ((UserUploadDto)item).Nombre,
                    Apellidos = ((UserUploadDto)item).Apellidos,
                    FechaNac = ((UserUploadDto)item).FechaNac,
                    FechaCreac = ((UserUploadDto)item).FechaCreac,
                    FechaUltMod = ((UserUploadDto)item).FechaUltMod,
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
                    LLamadaUploadUser usuarioHttp = new() {
                        usuario = userInfo
                    };

                    // Comprobamos si se ha modificado el rol
                    if (userInfo.Rol != copiaSeguridadUsuario.Rol) {
                        usuarioHttp.rolModificado = true;
                    }

                    HttpResponseMessage httpResponseMessage = await Http.PutAsJsonAsync("gestionUsers/actualizarUsuario", usuarioHttp);

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

            } catch (Exception ex) {
                _snackbar.Add("Error al editar al médico", Severity.Error);
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }


        private async void MostrarMensajeError(List<ValidationResult> errores) {
            bool? result = await DialogService.ShowMessageBox(
                "Error al editar usuario",
                new MarkupString(generarHtmlErrores(errores)),
                yesText: "Entendido");
        }

        private string generarHtmlErrores(List<ValidationResult>  errores) {
            string listaErrores = "<p><b>Algunos campos no son correctos y no es posible editar el usuario</b></p><ol>";

            foreach (ValidationResult validacion in errores) {
                listaErrores += $"<li>{validacion.ErrorMessage}</li>";
            }
            listaErrores += "</ol>";

            return listaErrores;
        }
    }
}

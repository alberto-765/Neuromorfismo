using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class CrearUsuario
    {
        [Inject] private ISnackbar _snackbar { get; set; }
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        private ClaimsPrincipal? user { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        [Inject] IAdminsService _adminsService { get; set; }

        // CAMPOS EDITFORM
        private bool cargando { get; set; } = false;
        private UserRegistroDto userRegistro = new();
        private EditContext formContext;


        protected override async Task OnInitializedAsync()
        {
            try {
                // Obtenemos los datos del usuario
                if (authenticationState is not null) {
                    var authState = await authenticationState;
                    user = authState?.User;    
                }

                Http = _crearHttpClient.CrearHttp(); // creamos http

                // Configuracion default snackbar
                _snackbar.Configuration.PreventDuplicates = true;
                _snackbar.Configuration.ShowTransitionDuration = 300;
                _snackbar.Configuration.HideTransitionDuration = 300;

                // Creamos contraseña aleatoria
                userRegistro.Password = await _adminsService.GenerarContraseñaAleatoria();

                // Crear contexto del editform
                formContext = new(userRegistro);
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        private async Task Crear()
        {
            try
            {
                cargando = true;
                HttpResponseMessage respuesta = await Http.PostAsJsonAsync("cuentas/crear", userRegistro);
                if (respuesta.IsSuccessStatusCode)
                {
                    _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                    _snackbar.Add(await respuesta.Content.ReadAsStringAsync(), Severity.Success, config => {
                        config.ShowCloseIcon = false;
                        config.VisibleStateDuration = 5000;
                    });

                    // Reiniciamos el objeto de nuevo usuario
                    userRegistro = new();
                } else {
                    cargando = false;
                    _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                    _snackbar.Add(await respuesta.Content.ReadAsStringAsync(), Severity.Error, config => {
                        config.ShowCloseIcon = false;
                        config.VisibleStateDuration = 3000;
                    });
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // LLamada a backend para generar un nombre de usuario valido
        private async Task ValidarUserName() {
            try {
                if (_adminsService.ValidarNomYApellUser(userRegistro.Nombre, userRegistro.Apellidos)) {
                    bool userNameCorrecto = false; // almacenaremos si ha podido generarse el nuevo nombre del usuario
                    string[] nomYApell = { userRegistro.Nombre, userRegistro.Apellidos };
                    HttpResponseMessage respuesta = await Http.PostAsJsonAsync($"cuentas/generarUserName", nomYApell);
                    if(respuesta.IsSuccessStatusCode) {

                        // Obtenemos el nuevo username
                        string userName = await respuesta.Content.ReadAsStringAsync();
                        if (!string.IsNullOrEmpty(userName)) {
                            userRegistro.UserLogin = userName;
                            userNameCorrecto = true;
                        } 
                    }

                    // Validamos si se ha generado el nuevo nombre
                    if (userNameCorrecto == false) {
                        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                        _snackbar.Add(@"<div>No ha sido posible generar el nombre de inicio de sesión</div>
                                        <div>Inténtelo de nuevo o contacte con un administrador.</div>", Severity.Error, config => { config.VisibleStateDuration = 5 * 1000;});
                    }
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }

        // Actualizar fecha de un datepicker
        private void ActFecha((DateTime?, string) tupla) {
            try {
                if (!string.IsNullOrEmpty(tupla.Item2)) {
                    switch (tupla.Item2) {
                        case "fechaNac":
                        userRegistro.FechaNac = tupla.Item1;
                        break;
                    }
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
                throw;
            }
        }
    }
}

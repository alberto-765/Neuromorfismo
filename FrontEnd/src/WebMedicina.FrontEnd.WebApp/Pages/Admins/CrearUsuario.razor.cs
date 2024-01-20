using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class CrearUsuario
    {
        // Dependecias
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; } = null!;
        [Inject] IAdminsService _adminsService { get; set; } = null!;

        // Parametros
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }

        // Configuracion
        private ClaimsPrincipal? user { get; set; }
        private HttpClient Http { get; set; } = null!;

        // CAMPOS EDITFORM
        private bool cargando { get; set; } = false;
        private UserRegistroDto userRegistro = new();
        private EditContext formContext = null!;

        protected override async Task OnInitializedAsync() {
            // Creamos contraseña aleatoria
            Task<string> taskCrearContra = _adminsService.GenerarContraseñaAleatoria();

            // Obtenemos los datos del usuario
            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;    
            }

            Http = _crearHttpClient.CrearHttpApi(); // creamos http

            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.ShowTransitionDuration = 300;
            _snackbar.Configuration.HideTransitionDuration = 300;

            // Crear contexto del editform
            formContext = new(userRegistro); 

            userRegistro.Password = await taskCrearContra; 
        }

        private async Task Crear() {
            try {
                cargando = true;
                HttpResponseMessage respuesta = await Http.PostAsJsonAsync("cuentas/crear", userRegistro);
                if (respuesta.IsSuccessStatusCode)
                {
                    _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                    _snackbar.Add(await respuesta.Content.ReadAsStringAsync(), Severity.Success, config => {
                        config.ShowCloseIcon = false;
                        config.VisibleStateDuration = 5000;
                    });

                    ReiniciarDatos();
                } else {
                    cargando = false;
                    _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                    _snackbar.Add(await respuesta.Content.ReadAsStringAsync(), Severity.Error, config => {
                        config.ShowCloseIcon = false;
                        config.VisibleStateDuration = 3000;
                    });
                }
            } catch (Exception) {
                ReiniciarDatos();
                throw;
            }
        }

        // LLamada a backend para generar un nombre de usuario valido
        private async Task ValidarUserName() { 
            if (_adminsService.ValidarNomYApellUser(userRegistro.Nombre, userRegistro.Apellidos)) {
                bool userNameCorrecto = false; // almacenaremos si ha podido generarse el nuevo nombre del usuario
                string[] nomYApell = { userRegistro.Nombre, userRegistro.Apellidos };
                HttpResponseMessage respuesta = await Http.PostAsJsonAsync($"cuentas/generarusername", nomYApell);
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
        }

        // Reiniciar objeto de nuevo usuario y boton de crear
        private void ReiniciarDatos() { 
            // Reiniciamos el objeto de nuevo usuario
            userRegistro = new();
            cargando = false; 
        }
    }
}

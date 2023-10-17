using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using MudBlazor;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class CrearUsuario
    {
        [Inject] private ISnackbar _snackbar { get; set; }
        private UserRegistroDto userRegistro = new();
        private bool cargando { get; set; } = false;
        private DateTime? fechaNac { get; set; }
        private MudDatePicker _picker { get; set; }
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        private ClaimsPrincipal? user { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionDto excepcionPersonalizada { get; set; }
        [Inject] IRedirigirManager _redirigirManager { get; set; }

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
                userRegistro.Password = await GenerarContraseñaAleatoria();
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
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
                    await _redirigirManager.RedirigirPagAnt();
                } else {
                    cargando = false;
                    _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                    _snackbar.Add(await respuesta.Content.ReadAsStringAsync(), Severity.Error, config => {
                        config.ShowCloseIcon = false;
                        config.VisibleStateDuration = 3000;
                    });
                }
            }
            catch (Exception ex)
            {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        // LLamada a BBDD para validar si el username está disponible
        private async Task validarUserName() {
            try {
                // Validamos que el campo del numeroHistoria cumpla las validaciones del dto
                var validationErrors = new List<ValidationResult>();
                bool esValido = Validator.TryValidateProperty(userRegistro.NumHistoria,
                                                new ValidationContext(userRegistro) { MemberName = nameof(userRegistro.NumHistoria) },
                                                validationErrors);
                if (esValido) {
                    HttpResponseMessage respuesta = await Http.PostAsJsonAsync($"cuentas/comprobarUser", userRegistro.NumHistoria);

                    if(respuesta.IsSuccessStatusCode) {

                        // Devuelve true si el usuario ya existe
                        if(await respuesta.Content.ReadFromJsonAsync<bool>()) {
                            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopStart;
                            _snackbar.Add("El número de historia ya está en uso", Severity.Error, config => {
                                config.VisibleStateDuration = int.MaxValue;
                            });
                        } else {
                            _snackbar.Clear();
                        }
                    }
                } 
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }


        public async Task<string> GenerarContraseñaAleatoria() {
            // Generamos constantes para la contraseña
            const string letrasMin = "abcdefghijklmnopqrstuvwxyz";
            const string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "1234567890";
            const string especiales = "!@#$%^&*()_+";

            // Generamos objeto random y contraseña la cual se rellenará
            Random random = new Random();
            StringBuilder constra = new StringBuilder();

            // Añadimos 1 letra minuscula
            constra.Append(letrasMay[random.Next(letrasMay.Length)]);

            // Añadimos 1 letra mayuscula
            constra.Append(letrasMin[random.Next(letrasMin.Length)]);
            // Añadimos 3 numeros
            for (int i = 0; i < 5; i++) {
                constra.Append(numeros[random.Next(numeros.Length)]);
            }
            // Añadimos 1 caracter especial
            constra.Append(especiales[random.Next(especiales.Length)]);

            return constra.ToString();
        }
    }
}

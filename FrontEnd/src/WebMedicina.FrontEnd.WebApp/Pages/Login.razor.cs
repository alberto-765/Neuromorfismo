using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;
using static System.Net.WebRequestMethods;

namespace WebMedicina.FrontEnd.WebApp.Pages {
    public partial class Login {
        private UserLoginDto userLogin = new();
        private bool mostrado { get; set; } = false;
        private bool cargando { get; set; } = false;
        private string iconoPassword { get; set; } = Icons.Material.Filled.VisibilityOff;
        private string mensajeErrorLogin { get; set; } = String.Empty;
        // Tipo de input para el 
        private InputType tipoInputPass { get; set; } = InputType.Password;
        [Inject] JWTAuthenticationProvider _jwtAuthenticationProvider { get; set; }
        [Inject] IRedirigirManager redirigirManager { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionDto excepcionPersonalizada { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }

        protected override async Task OnInitializedAsync() {
            Http = _crearHttpClient.CrearHttp();
        }

        private void clickPassword() {
            try {

                if(mostrado)
                {
                    mostrado = false;
                    iconoPassword = Icons.Material.Filled.VisibilityOff;
                    tipoInputPass = InputType.Password;
                }
                else {
                    mostrado = true;
                    iconoPassword = Icons.Material.Filled.Visibility;
                    tipoInputPass = InputType.Text;
                }
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        private async Task hacerLogin() {
            try {
                HttpResponseMessage respuesta;

                // validamos el formulario
                cargando = true;
                respuesta = await Http.PostAsJsonAsync("cuentas/login", userLogin);
                if (respuesta.IsSuccessStatusCode) {
                    UserToken token = await respuesta.Content.ReadFromJsonAsync<UserToken>();
                    if (token != null && token.Expiration >= DateTime.Now) {
                        await _jwtAuthenticationProvider.Login(token.Token);
                        redirigirManager.RedirigirDefault();
                    } else {
                        throw new Exception("Error en el token de autenticación");
                    }
                } else {
                    mensajeErrorLogin = await respuesta.Content.ReadAsStringAsync();
                    cargando = false;
                }
            } catch (Exception ex) {
                cargando = false;
                mensajeErrorLogin = "Error inesperado de autenticación, inténtelo de nuevo";
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }
    }
}

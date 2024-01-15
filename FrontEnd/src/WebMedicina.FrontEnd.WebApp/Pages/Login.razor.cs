using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Data;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Usuarios;
using static System.Net.WebRequestMethods;

namespace WebMedicina.FrontEnd.WebApp.Pages
{
    public partial class Login {
        private UserLoginDto userLogin = new();
        private bool mostrado { get; set; } = false;
        private bool cargando { get; set; } = false;
        private string iconoPassword { get; set; } = Icons.Material.Filled.VisibilityOff;
        private string mensajeErrorLogin { get; set; } = String.Empty;
        // Tipo de input para el 
        private InputType tipoInputPass { get; set; } = InputType.Password;
        [Inject] JWTAuthenticationProvider _jwtAuthenticationProvider { get; set; } = null!;
        [Inject] IRedirigirManager redirigirManager { get; set; } = null!;
        [Inject] ICrearHttpClient _crearHttpClient { get; set; } = null!;
        private HttpClient Http { get; set; } = null!;

        protected override void OnInitialized() {
            Http = _crearHttpClient.CrearHttp();
        }

        private void clickPassword() {
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
        }

        private async Task hacerLogin() {
            try {
                HttpResponseMessage respuesta;

                // validamos el formulario
                cargando = true;
                respuesta = await Http.PostAsJsonAsync("cuentas/login", userLogin);
                if (respuesta.IsSuccessStatusCode) {
                    UserToken? token = await respuesta.Content.ReadFromJsonAsync<UserToken>();
                    if (token is not null && token.Expiration >= DateTime.Now && !string.IsNullOrWhiteSpace(token.Token)) {
                        await _jwtAuthenticationProvider.Login(token.Token);
                        await redirigirManager.RedirigirDefault();
                    } else {
                        throw new NoNullAllowedException();
                    }
                } else {
                    mensajeErrorLogin = await respuesta.Content.ReadAsStringAsync();
                    cargando = false;
                }
            } catch (NoNullAllowedException) {
                cargando = false;
                mensajeErrorLogin = "Error en el token de autenticación";
                throw;
            } catch (Exception) {
                cargando = false;
                mensajeErrorLogin = "Error inesperado de autenticación, inténtelo de nuevo";
            }
        }
    }
}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages {
    public partial class Login {
        private UserLoginDto userLogin = new();
        private bool mostrado { get; set; } = false;
        private bool cargando { get; set; } = false;
        private string iconoPassword { get; set; } = Icons.Material.Filled.VisibilityOff;
        private string mensajeErrorLogin { get; set; } = String.Empty;
        // Tipo de input para el 
        private InputType tipoInputPass { get; set; } = InputType.Password;

        [Inject]
        private ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http;
        private HttpResponseMessage respuesta;
        [Inject]
        protected ExcepcionDto _excepcionPersonalizada { get; set; }
        [Inject]
        JWTAuthenticationProvider _jwtAuthenticationProvider { get; set; }

        protected override void OnInitialized() {
            try {
                    Http = _crearHttpClient.CrearHttp();
            } catch (Exception ex) {
                _excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
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
            // validamos el formulario
            cargando = true;
            respuesta = await Http.PostAsJsonAsync("Login", userLogin);
            if (respuesta.IsSuccessStatusCode) {
                _jwtAuthenticationProvider.Login(await respuesta.Content.ReadAsStringAsync());
            } else {
                mensajeErrorLogin = await respuesta.Content.ReadAsStringAsync();
            }
    }
    }
}

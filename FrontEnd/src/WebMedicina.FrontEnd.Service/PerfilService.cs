using Microsoft.JSInterop;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.UserAccount;

namespace WebMedicina.FrontEnd.Service {
    public class PerfilService : IPerfilService {
        private readonly IRedirigirManager _redirigirManager;
        private readonly JWTAuthenticationProvider _jwtAuthenticationProvider;
        private readonly HttpClient _httpClient;


        public PerfilService(JWTAuthenticationProvider jwtAuthenticationProvider, IRedirigirManager redirigirManager, ICrearHttpClient crearHttpClient) { 
            _jwtAuthenticationProvider = jwtAuthenticationProvider;
            _redirigirManager = redirigirManager;
            _httpClient = crearHttpClient.CrearHttpApi();
		}

        /// <summary>
        /// Realizar cambio de contraseña dle usuario
        /// </summary>
        /// <param name="changePass"></param>
        /// <returns></returns>
        public async Task<CodigosErrorChangePass[]> CambiarContrasena(ChangePasswordDto changePass) {
            CodigosErrorChangePass[]? contrasenaCambiada = null;
            HttpResponseMessage respuesta = await _httpClient.PatchAsJsonAsync("cuentas/cambiarpassword", changePass);

            if (respuesta.IsSuccessStatusCode) {
                contrasenaCambiada = await respuesta.Content.ReadFromJsonAsync<CodigosErrorChangePass[]>();
            }

            return contrasenaCambiada ?? Array.Empty<CodigosErrorChangePass>();
        }

        /// <summary>
        /// Cerrar sesion 
        /// </summary>
        /// <returns></returns>
        public async Task CerrarSesion() {
            // Cerramos sesion
            await _jwtAuthenticationProvider.Logout();

            // Redirigimos al login
            await _redirigirManager.RedirigirDefault("login");
        }
    }
}

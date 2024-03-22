using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using System.IdentityModel.Tokens.Jwt;
using Neuromorfismo.Shared.Dto.UserAccount;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Net.Http.Json;

namespace Neuromorfismo.FrontEnd.Service {
    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService {
        // INJECCIONES
        private readonly IJSRuntime js;
        private readonly HttpClient _httpClient;

        
        private JwtSecurityTokenHandler handler = new ();
        private readonly string KeyTokenSession; // Clave del localStorage en session
        private AuthenticationState Anonimo => new(new ClaimsPrincipal(new ClaimsIdentity())); // Identidad anonima por si el usuario no está autenticado

        public JWTAuthenticationProvider(IJSRuntime js, IConfiguration configuration, ICrearHttpClient clientFactory) {
            try {
                _httpClient = clientFactory.CrearHttpApi();
                this.js = js;

                // Obtenemos key token para localStorage
                KeyTokenSession = configuration["TokenKey"] ?? throw new NullReferenceException();
            } catch (Exception) {
                throw;
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync() {
            try {         
                // Revisamos si tenemos un token en localstorage para autentical al usuario
                string tokenSession = await js.GetFromLocalStorage(KeyTokenSession);
                Tokens? token = (!string.IsNullOrWhiteSpace(tokenSession) ? JsonSerializer.Deserialize<Tokens>(tokenSession) : null);

                if (token is not null && !string.IsNullOrWhiteSpace(token.AccessToken) && !string.IsNullOrWhiteSpace(token.RefreshToken)) {
                    // Llamamos a la API para autenticar al usuario
                    HttpResponseMessage respuesta = await _httpClient.PostAsJsonAsync("cuentas/autenticarportoken", token);

                    if (respuesta.IsSuccessStatusCode) {
                        AutenticarPorTokenDto? autenticarPorToken = await respuesta.Content.ReadFromJsonAsync<AutenticarPorTokenDto>();

                        if (autenticarPorToken is not null && autenticarPorToken.Tokens is not null) {

                            // Actuaizamos el token de session si es necesario
                            if (autenticarPorToken.ActualizarSession) {
                                await js.SetInLocalStorage(KeyTokenSession, JsonSerializer.Serialize(autenticarPorToken.Tokens));
                            }

                            // Construimos autenticacion con nuevo acces token
                            return ConstruirAuthenticationState(autenticarPorToken.Tokens.AccessToken);
                        }
                    }
                }

                // Limpiamos token si no es valido o no se ha autenticado al usuario
                await js.RemoveItemlocalStorage(KeyTokenSession);
                return Anonimo;
            } catch (Exception) {
                return Anonimo;
            }
        }


        // Generamos el token del usuario
        private AuthenticationState ConstruirAuthenticationState(string token) {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(handler.ReadJwtToken(token).Claims, "jwt")));
        }

        // Guardamos en localStorage los tokens del usuario
        public async Task Login (Tokens tokens) {
            await js.SetInLocalStorage(KeyTokenSession, JsonSerializer.Serialize(tokens));

            // Actualizar el estado de los authorizationView
            AuthenticationState authState = ConstruirAuthenticationState(tokens.AccessToken);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        // El usuario a pedido cerrar sesión
        public async Task Logout () {
            // Obtenemos los tokens de sesion
            string tokenSession = await js.GetFromLocalStorage(KeyTokenSession);
            Tokens? tokens = (!string.IsNullOrWhiteSpace(tokenSession) ? JsonSerializer.Deserialize<Tokens>(tokenSession) : null);

            // Si los tokens están en session cerramos sesión
            if (tokens is not null) { 
                await _httpClient.PostAsJsonAsync("cuentas/cerrarsesion", tokens);
            }

            // Tras haber eliminado el refreshToken, limpiados localStorage
            await js.RemoveItemlocalStorage(KeyTokenSession);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }
    }
}

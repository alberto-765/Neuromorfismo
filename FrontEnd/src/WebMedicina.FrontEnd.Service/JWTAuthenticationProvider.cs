using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using System.IdentityModel.Tokens.Jwt;
using WebMedicina.Shared.Dto.UserAccount;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Net.Http.Json;

namespace WebMedicina.FrontEnd.Service {
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

        public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
            try {         
                // Revisamos si tenemos un token en localstorage para autentical al usuario
                string tokenSession = await js.GetFromLocalStorage(KeyTokenSession);
                Tokens? token = (!string.IsNullOrWhiteSpace(tokenSession) ? JsonSerializer.Deserialize<Tokens>(tokenSession) : null);

                if (token is null || string.IsNullOrWhiteSpace(token.AccessToken) || string.IsNullOrWhiteSpace(token.RefreshToken)) {
                    return Anonimo;
                } else {
                    JwtSecurityToken jsonToken = handler.ReadJwtToken(token.AccessToken);

                    // Validamos la expiracion del token
                    if (jsonToken.ValidTo < DateTime.Now) {
                        // Intentamos refrescar el token
                        var respuesta = await _httpClient.PostAsJsonAsync("cuentas/refrescartoken", token);
                        token = await respuesta.Content.ReadFromJsonAsync<Tokens?>();            
                    }

                    if (token is not null && !string.IsNullOrWhiteSpace(token.AccessToken)) {
                        return ConstruirAuthenticationState(token.AccessToken);
                    } else {
                        await Logout();
                        return Anonimo;
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
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

        // Eliminamos token y colocamos el perfil del usuario como anonimo
        public async Task Logout () {
            // Llamamos a API para cerrar sesion
            Tokens? tokens =  JsonSerializer.Deserialize<Tokens>(await js.GetFromLocalStorage(KeyTokenSession));

            if (tokens is not null) { 
                await _httpClient.PostAsJsonAsync("cuentas/cerrarsesion", tokens);
            }

            // Tras haber eliminado el refreshToken, limpiados localStorage
            await js.RemoveItemlocalStorage(KeyTokenSession);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }
    }
}

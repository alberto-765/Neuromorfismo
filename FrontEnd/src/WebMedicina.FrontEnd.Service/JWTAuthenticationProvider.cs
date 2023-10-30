using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using System.Net.Http.Headers;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;

namespace WebMedicina.FrontEnd.Service {
    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService {
        private readonly IJSRuntime js;
        public const string TOKENKEY = "OIJWRGU8G28238U2GIUG2H2VUHUIVWU89WEVIU";
        private readonly HttpClient httpClient;
        private JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        // Identidad anonima por si el usuario no está autenticado
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

         public JWTAuthenticationProvider(IJSRuntime js, ICrearHttpClient httpClient) {
            this.js = js;
            this.httpClient = httpClient.CrearHttp();
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
            // Revisamos si tenemos un token en localstorage para autentical al usuario
            var token = await js.GetFromLocalStorage(TOKENKEY);
            if (token == null) {
                return Anonimo;
            } else {
                JwtSecurityToken jsonToken = handler.ReadJwtToken(token);

                // Validamos la expiracion del token
                if (jsonToken != null && jsonToken.ValidTo < DateTime.UtcNow) {
                    await Logout();
                    return Anonimo;
                } else {
                    return ConstruirAuthenticationState(token);
                }
            }
        }

        // Generamos el token del usuario
        private AuthenticationState ConstruirAuthenticationState(string token) {
            // creamos la cabecera para que cada vez que hagamos llamadas http mandemos el token del usuario
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(handler.ReadJwtToken(token).Claims, "jwt")));
        }

        // Guardamos en localStorage el token del usuario
        public async Task Login (string token) {
            await js.SetInLocalStorage(TOKENKEY, token);

            // Actualizar el estado de los authorizationView
            var authState = ConstruirAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));

        }

        // Guardamos en localStorage el token del usuario
        public async Task Logout () {
            // Quitamos del header el token del usuario
            httpClient.DefaultRequestHeaders.Authorization = null;

            // Eliminamos y colocamos el perfil del usuario como anonimo
            await js.RemoveItemlocalStorage(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }
    }
}

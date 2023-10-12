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

namespace WebMedicina.FrontEnd.Service {
    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService {
        private readonly IJSRuntime js;
        public static readonly string TOKENKEY = "OIJWRGU8G28238U2GIUG2H2VUHUIVWU89WEVIU";
        private readonly HttpClient httpClient;

        // Identidad anonima por si el usuario no está autenticado
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

         public JWTAuthenticationProvider(IJSRuntime js, HttpClient httpClient) {
            this.js = js;
            this.httpClient = httpClient;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
            // Revisamos si tenemos un token en localstorage para autentical al usuario
            var token = await js.GetFromLocalStorage(TOKENKEY);
            if (token == null) {
                return Anonimo;
            }
            return ConstruirAuthenticationState(token);

        }

        // Generamos el token del usuario
        private AuthenticationState ConstruirAuthenticationState(string token) {
            // creamos la cabecera para que cada vez que hagamos llamadas http mandemos el token del usuario
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
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

        public static IEnumerable<Claim> ParseClaimsFromJwt(string jwt) {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64) {
            switch (base64.Length % 4) {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}

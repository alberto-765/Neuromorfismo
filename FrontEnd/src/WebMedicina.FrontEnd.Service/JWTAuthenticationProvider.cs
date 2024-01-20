using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using System.IdentityModel.Tokens.Jwt;

namespace WebMedicina.FrontEnd.Service {
    public class JWTAuthenticationProvider : AuthenticationStateProvider, ILoginService {
        private readonly IJSRuntime js;
        public const string TOKENKEY = "OIJWRGU8G28238U2GIUG2H2VUHUIVWU89WEVIU";
        private JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        // Identidad anonima por si el usuario no está autenticado
        private AuthenticationState Anonimo => new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

         public JWTAuthenticationProvider(IJSRuntime js) {
            this.js = js;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync() {
            // Revisamos si tenemos un token en localstorage para autentical al usuario
            string token = await ObtenerTokenSession();
            if (string.IsNullOrWhiteSpace(token)) {
                return Anonimo;
            } else {
                JwtSecurityToken jsonToken = handler.ReadJwtToken(token);

                // Validamos la expiracion del token
                if (jsonToken is null || jsonToken.ValidTo < DateTime.UtcNow) {
                    await Logout();
                    return Anonimo;
                } else {
                    return ConstruirAuthenticationState(token);
                }
            }
        }

        /// <summary>
        /// Devolver token de session, creado para el handler de crear http
        /// </summary>
        /// <returns></returns>
        public async Task<string> ObtenerTokenSession() {
            try {
                return await js.GetFromLocalStorage(TOKENKEY);
            } catch (Exception) {
                return string.Empty;
            }
        }


        // Generamos el token del usuario
        private AuthenticationState ConstruirAuthenticationState(string token) {
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(handler.ReadJwtToken(token).Claims, "jwt")));
        }

        // Guardamos en localStorage el token del usuario
        public async Task Login (string token) {
            await js.SetInLocalStorage(TOKENKEY, token);

            // Actualizar el estado de los authorizationView
            var authState = ConstruirAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));

        }

        // Eliminamos token y colocamos el perfil del usuario como anonimo
        public async Task Logout () {
            await js.RemoveItemlocalStorage(TOKENKEY);
            NotifyAuthenticationStateChanged(Task.FromResult(Anonimo));
        }
    }
}

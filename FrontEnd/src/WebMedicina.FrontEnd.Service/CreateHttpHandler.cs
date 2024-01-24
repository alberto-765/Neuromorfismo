using Microsoft.Extensions.Configuration;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Text.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.UserAccount;

namespace WebMedicina.FrontEnd.Service {
    public class CreateHttpHandler : DelegatingHandler {
        private readonly IJSRuntime _js;
        private readonly string KeyTokenSession; // Clave del localStorage en session


        public CreateHttpHandler(IJSRuntime js, IConfiguration configuration) {
            _js = js;

            // Obtenemos key token para localStorage
            KeyTokenSession = configuration["TokenKey"] ?? throw new NullReferenceException();
        }

        /// <summary>
        /// Asignar al header del httpClient los servicios de autenticacion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            string tokenSession = await _js.GetFromLocalStorage(KeyTokenSession);
            Tokens? token = (!string.IsNullOrWhiteSpace(tokenSession) ? JsonSerializer.Deserialize<Tokens>(tokenSession) : null);

            // Si el token es valido lo asignamos al header
            if (token is null || string.IsNullOrWhiteSpace(token.AccessToken)) {
                request.Headers.Authorization = null;
            } else {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

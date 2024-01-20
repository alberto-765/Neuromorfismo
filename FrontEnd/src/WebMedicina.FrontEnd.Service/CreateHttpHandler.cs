using System.Net.Http.Headers;

namespace WebMedicina.FrontEnd.Service {
    public class CreateHttpHandler : DelegatingHandler {
        private readonly JWTAuthenticationProvider _JWTAuthenticationProvider;

        public CreateHttpHandler(JWTAuthenticationProvider JWTAuthenticationProvider) {
            _JWTAuthenticationProvider = JWTAuthenticationProvider;
        }

        /// <summary>
        /// Asignar al header del httpClient los servicios de autenticacion
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            string token = await _JWTAuthenticationProvider.ObtenerTokenSession();

            // Si el token es valido lo asignamos al header
            if (string.IsNullOrWhiteSpace(token)) {
                request.Headers.Authorization = null;
            } else {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}

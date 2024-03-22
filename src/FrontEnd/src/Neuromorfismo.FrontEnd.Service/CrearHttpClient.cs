using Neuromorfismo.FrontEnd.ServiceDependencies;

namespace Neuromorfismo.FrontEnd.Service {
    public class CrearHttpClient : ICrearHttpClient{
        private readonly IHttpClientFactory _httpClientFactory;

        public CrearHttpClient(IHttpClientFactory httpClientFactory) { 
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CrearHttpApi() {
            return _httpClientFactory.CreateClient("HttpAPI");
        }
    }
}

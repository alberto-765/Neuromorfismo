using System.Net.Http;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
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

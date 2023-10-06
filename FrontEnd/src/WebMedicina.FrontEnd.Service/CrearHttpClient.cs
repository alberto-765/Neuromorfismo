using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Service {
    public class CrearHttpClient {
        private readonly HttpClient _HttpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public CrearHttpClient(IHttpClientFactory httpClientFactory) {
            try {
            _httpClientFactory = httpClientFactory;
            _HttpClient = _httpClientFactory.CreateClient("HttpAPI");
            } catch (Exception e) {
                throw e;
            }
        }
        public HttpClient ObtenerCliente() {
            return _HttpClient;
        }
    }
}

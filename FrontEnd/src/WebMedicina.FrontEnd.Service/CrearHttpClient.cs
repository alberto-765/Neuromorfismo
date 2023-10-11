using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class CrearHttpClient : ICrearHttpClient{
        private HttpClient _HttpClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public CrearHttpClient(IHttpClientFactory httpClientFactory) { 
            _httpClientFactory = httpClientFactory;
        }

        public HttpClient CrearHttp() {
            try {
                _HttpClient = _httpClientFactory.CreateClient("HttpAPI");
                return _HttpClient;
            } catch (Exception ) {
                throw ;
            }
        }
        public HttpClient ObtenerCliente() {
            return _HttpClient;
        }
    }
}

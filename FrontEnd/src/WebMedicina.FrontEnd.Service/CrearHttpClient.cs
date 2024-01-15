using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class CrearHttpClient : ICrearHttpClient{
        private readonly HttpClient _HttpClientAPI;
        private readonly IHttpClientFactory _httpClientFactory;

        public CrearHttpClient(IHttpClientFactory httpClientFactory) { 
            _httpClientFactory = httpClientFactory;
            _HttpClientAPI = _httpClientFactory.CreateClient("HttpAPI");
        }

        public HttpClient CrearHttp() {
            return _HttpClientAPI;
        }
    }
}

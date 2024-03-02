using System.Net.Http.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.FrontEnd.Service {
    public class EstadisticasService : IEstadisticasService{
        private readonly HttpClient _http;


        public EstadisticasService(ICrearHttpClient crearHttpClient) {
            _http = crearHttpClient.CrearHttpApi();
        }

        /// <summary>
        /// Obtener estadísticas de pacientes y evoluciones
        /// </summary>
        /// <returns></returns>
        public async Task<EstadisticasDto> ObtenerEstadisitcas() {
            return await _http.GetFromJsonAsync<EstadisticasDto>("estadisticas") ?? new();
        }
    }
}

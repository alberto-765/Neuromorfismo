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
            try {
                HttpResponseMessage respuesta = await _http.GetAsync("estadisticas");
                EstadisticasDto? estadisticas = null;

                if (respuesta.IsSuccessStatusCode) {
                    estadisticas = await respuesta.Content.ReadFromJsonAsync<EstadisticasDto>();
                }

                return estadisticas ?? new();
            } catch(Exception) {
                return new();
            }
        }
    }
}

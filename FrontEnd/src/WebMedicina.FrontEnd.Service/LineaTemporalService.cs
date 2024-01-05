using Microsoft.JSInterop;
using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.Service {
    public class LineaTemporalService : ILineaTemporalService {
        private readonly HttpClient Http;
        private readonly IJSRuntime _js;
        private readonly string keyEtapas = Guid.NewGuid().ToString();

        public LineaTemporalService(ICrearHttpClient crearHttpClient, IJSRuntime js) {
            this.Http = crearHttpClient.CrearHttp();
            _js = js;
        }

        // Obtener etapas disponibles para la linea temporal
        public async Task<ImmutableSortedDictionary<int, EtapasDto>?> ObtenerEtapas() {
			try {
                // Primero validamos si las etapas ya está en sessioStorage
                ImmutableSortedDictionary<int, EtapasDto>? etapas = null;
                string jsonEtapas = await _js.GetFromSessionStorage(keyEtapas);

                // Si estapas es null significa que es la primera vez que se obtiene
                if (string.IsNullOrWhiteSpace(jsonEtapas)) {
                    etapas = await Http.GetFromJsonAsync<ImmutableSortedDictionary<int,EtapasDto>>("LineaTemporal/obtenerEtapas");

                    // guardamos las etapas en session
                    await _js.SetInSessionStorage(keyEtapas, JsonSerializer.Serialize(etapas));
                } else {
                   etapas = JsonSerializer.Deserialize<ImmutableSortedDictionary<int, EtapasDto>>(jsonEtapas);
                }

                return etapas;
			} catch (Exception ex) {
                Console.WriteLine(ex.ToString());
				throw;
			}
        }
    }
}

using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.Service
{
    public class LineaTemporalService : ILineaTemporalService {
        private readonly HttpClient Http;
        private readonly IPacientesService _pacienteService;
        private readonly IJSRuntime _js;
        private readonly string keyEtapas = "EtapasLineaTemp";

        public LineaTemporalService(ICrearHttpClient crearHttpClient, IJSRuntime js, IPacientesService pacienteService) {
            this.Http = crearHttpClient.CrearHttp();
            _js = js;
            _pacienteService = pacienteService;
        }

        // Obtener etapas disponibles para la linea temporal
        public async Task<ImmutableSortedDictionary<int, EtapaLTDto>?> ObtenerEtapas() {
			try {
                // Primero validamos si las etapas ya está en sessioStorage
                ImmutableSortedDictionary<int, EtapaLTDto>? etapas = null;
                string jsonEtapas = await _js.GetFromSessionStorage(keyEtapas);

                // Si estapas es null significa que es la primera vez que se obtiene
                if (string.IsNullOrWhiteSpace(jsonEtapas)) {
                    etapas = await Http.GetFromJsonAsync<ImmutableSortedDictionary<int, EtapaLTDto>>("LineaTemporal/ObtenerTodasEtapas");

                    // guardamos las etapas en session
                    await _js.SetInSessionStorage(keyEtapas, JsonSerializer.Serialize(etapas));
                } else {
                   etapas = JsonSerializer.Deserialize<ImmutableSortedDictionary<int, EtapaLTDto>>(jsonEtapas);
                }

                return etapas;
			} catch (Exception ex) {
                Console.WriteLine(ex.ToString());
				throw;
			}
        }

        /// <summary>
        /// Verificamos si los datos del paciente estan en session y sino los obtenemos de la api
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>Datos linea temporal del paciente requerido</returns>
        public async Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucionPaciente(int idPaciente) {
            try {
                // Obtenemos todos los pacientes de session
                List<CrearPacienteDto> pacientes = await _pacienteService.ObtenerListaPacienteSession();
                CrearPacienteDto? paciente = null;

                // Obtenemos los datos de la etapa del cliente
                if (pacientes.Any()) {
                    paciente = pacientes.Find(q => q.IdPaciente == idPaciente);
                } else {
                    throw new Exception();
                }

                // Comprobamos si hay que obtener la evolucion del paciente
                if(paciente is not null) {

                    if(paciente.Evoluciones is null) {
                        HttpResponseMessage httpResponseMessage = await Http.GetAsync($"LineaTemporal/ObtenerEvolucionPaciente/{idPaciente}");

                        // Obtenemos la evolucion del paciente y lo guardamos en session
                        if (httpResponseMessage.IsSuccessStatusCode) {
                            paciente.Evoluciones = await httpResponseMessage.Content.ReadFromJsonAsync<SortedList<int, EvolucionLTDto>>();

                            await _pacienteService.GuardarPacientesSession(pacientes);
                        } 
                    }
                }

                // Si el paciente no tiene evolucion se recibiria una lista vacia, nunca null
                return paciente?.Evoluciones ?? throw new Exception();
            } catch (Exception) {
                throw;
            }
        }
    }
}

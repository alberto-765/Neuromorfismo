using Microsoft.JSInterop;
using System.Collections.Immutable;
using System.Net.Http.Json;
using System.Text.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.Service
{
    public class LineaTemporalService : ILineaTemporalService {
        private readonly HttpClient _http;
        private readonly IPacientesService _pacienteService;
        private readonly IJSRuntime _js;
        private readonly string keyEtapas = "EtapasLineaTemp";

        public LineaTemporalService(ICrearHttpClient crearHttpClient, IJSRuntime js, IPacientesService pacienteService) {
            _http = crearHttpClient.CrearHttpApi();
            _js = js;
            _pacienteService = pacienteService;
        }

        // Obtener etapas disponibles para la linea temporal
        public async Task<ImmutableSortedDictionary<short, EtapaLTDto>?> ObtenerEtapas() {
            // Primero validamos si las etapas ya está en sessioStorage
            ImmutableSortedDictionary<short, EtapaLTDto>? etapas = null;
            string jsonEtapas = await _js.GetFromSessionStorage(keyEtapas);

            // Si estapas es null significa que es la primera vez que se obtiene
            if (string.IsNullOrWhiteSpace(jsonEtapas)) {
                etapas = await _http.GetFromJsonAsync<ImmutableSortedDictionary<short, EtapaLTDto>>("lineatemporal/obtenertodasetapas");

                // guardamos las etapas en session
                await _js.SetInSessionStorage(keyEtapas, JsonSerializer.Serialize(etapas));
            } else {
                etapas = JsonSerializer.Deserialize<ImmutableSortedDictionary<short, EtapaLTDto>>(jsonEtapas);
            }

            return etapas;
        }

        /// <summary>
        /// Verificamos si los datos del paciente estan en session y sino los obtenemos de la api
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>Datos linea temporal del paciente requerido</returns>
        public async Task<SortedList<short, EvolucionLTDto>> ObtenerEvolucionPaciente(int idPaciente) {
         
            // Obtenemos todos los pacientes de session
            List<CrearPacienteDto> pacientes = await _pacienteService.ObtenerListaPacienteSession();
            CrearPacienteDto? paciente = null;

            // Obtenemos los datos de la etapa del cliente
            if (pacientes.Any()) {
                paciente = pacientes.Find(q => q.IdPaciente == idPaciente);
            } 

            // Comprobamos si hay que obtener la evolucion del paciente
            if(paciente is not null) {

                // Obtenemos las evoluciones del paciente si no estan en session
                if(paciente.Evoluciones is null) {
                    HttpResponseMessage httpResponseMessage = await _http.GetAsync($"lineatemporal/obtenerevolucionpaciente/{idPaciente}");

                    // Obtenemos la evolucion del paciente y lo guardamos en session
                    if (httpResponseMessage.IsSuccessStatusCode) {
                        paciente.Evoluciones = await httpResponseMessage.Content.ReadFromJsonAsync<SortedList<short, EvolucionLTDto>>();

                        await _pacienteService.GuardarPacientesSession(pacientes);
                    } 
                }
            }

            // Si paciente es null lanzamos excepcion
            return paciente?.Evoluciones ?? throw new Exception();
        }

        /// <summary>
        /// Actualizar una etapa de la evolucion del paciente o añadirla si es nueva
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>Datos linea temporal del paciente requerido</returns>
        public async Task<SortedList<short, EvolucionLTDto>> ActEvoPac(LLamadaEditarEvoDto evoEditada) {
            CrearPacienteDto? paciente = null;

            // LLamada API para actualizar evolucion
            HttpResponseMessage respuesta = await _http.PutAsJsonAsync("lineatemporal/actoinsertevolucionpaciente", evoEditada);
            SortedList<short, EvolucionLTDto>? evolucionesEditadas = await respuesta.Content.ReadFromJsonAsync<SortedList<short, EvolucionLTDto>>();

            // Actualizamos evolucion del paciente en session
            if (evolucionesEditadas is not null && evolucionesEditadas.Any()) {
                // Obtenemos todos los pacientes de session
                List<CrearPacienteDto> pacientes = await _pacienteService.ObtenerListaPacienteSession();

                // Obtenemos los datos de la etapa del cliente
                if (pacientes.Any()) {
                    paciente = pacientes.Find(q => q.IdPaciente == evoEditada.IdPaciente);

                    if (paciente is not null) {
                        paciente.Evoluciones = evolucionesEditadas;
                        await _pacienteService.GuardarPacientesSession(pacientes);
                    }
                }
            }

            // Si paciente es null o no tiene evoluciones se lanzara excepcion
            return paciente?.Evoluciones ?? throw new Exception();
        }
    }
}

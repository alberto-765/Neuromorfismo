using Microsoft.JSInterop;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes; 

namespace WebMedicina.FrontEnd.Service {
    public class DocumentacionService : IDocumentacionService {
        // DEPENDENCIAS
        private readonly HttpClient _httpClient;
        private readonly IJSRuntime _js;
        public DocumentacionService(ICrearHttpClient crearHttpClient, IJSRuntime js) { 
            _httpClient = crearHttpClient.CrearHttpApi();
            _js = js;
        }


        /// <summary>
        /// Generar excel con la lista de pacientes especificada
        /// </summary>
        /// <param name="pacientes"></param>
        /// <param name="nombrePagina"></param>
        /// <returns>Excel generado correctamente</returns>
        public async Task<bool> DescargarExcelPacientes(ExcelPacientesDto excelPacientes) {
            // Hacemos llamada a la API para cargar el excel
            var respuesta = await _httpClient.PostAsJsonAsync("documentacion/cargarexcel", excelPacientes);

            // Si la respuesta es correcta descargamos el excel
            if (respuesta.IsSuccessStatusCode) {
                byte[] bytesExcel = await respuesta.Content.ReadAsByteArrayAsync(); 
                await _js.InvokeVoidAsync("DescargarExcel", $"Pacientes_{DateTime.Today.ToString("yyyy_MM_dd")}", bytesExcel);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Enviar email sobre el nuevo estado de la evolucion del paciente
        /// </summary>
        /// <param name="datosEmail"></param>
        public async Task EnviarEmailEvoActu(EvolucionLTDto evolucion, int idPaciente, string idContenedor) {
            // Obtenemos base64 de la imagen
            string imgBase64 = await _js.InvokeAsync<string>("GenerarImagenDeHtml", idContenedor);
            await _httpClient.PostAsJsonAsync("documentacion/cargarexcel", new EmailEditarEvoDto(evolucion, idPaciente, imgBase64));
        }
    }
}

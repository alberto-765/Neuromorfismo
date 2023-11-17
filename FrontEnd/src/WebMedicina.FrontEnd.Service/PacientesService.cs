using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.Service {
    public class PacientesService : IPacientesService {
        private readonly ICrearHttpClient _crearHttpClient;
        private readonly HttpClient Http;
        private readonly IJSRuntime js;

        public PacientesService(ICrearHttpClient crearHttpClient, IJSRuntime js) {
            _crearHttpClient = crearHttpClient;
            Http = _crearHttpClient.CrearHttp();
            this.js = js;
        }

        // Obtener todos los médicos que tienen pacientes a su cargo
        public async Task<IEnumerable<string>> ObtenerAllMed() {
            try {
                IEnumerable<string>? medPac = await Http.GetFromJsonAsync<IEnumerable<string>>("pacientes/getMedicosPacientes");

                // Asignamos una lista vacia si el valor devuelto de la llamada es null
                medPac ??= Enumerable.Empty<string>();

                return medPac;
            } catch (Exception) {
                throw;
            }
        }

        // Obtener opciones para filtros farmacos, mutaciones y epilepsias
        public async Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerFiltros() {
            try {
                // Obtenemos lista de farmacos
                List<FarmacosDto>?  listaFarmacos = await Http.GetFromJsonAsync<List<FarmacosDto>>("pacientes/getFarmacos");

                // Obtenemos lista de epilepsias
                List<EpilepsiasDto>?  listaEpilepsias = await Http.GetFromJsonAsync<List<EpilepsiasDto>>("pacientes/getEpilepsias");

                // Obtenemos lista de mutaciones
                List<MutacionesDto>?  listaMutaciones = await Http.GetFromJsonAsync<List<MutacionesDto>>("pacientes/getMutaciones");

                //Devolvemos las tres listas
                return (listaFarmacos, listaEpilepsias, listaMutaciones);
            } catch (Exception) {
                throw;
            }
        }
    }
}

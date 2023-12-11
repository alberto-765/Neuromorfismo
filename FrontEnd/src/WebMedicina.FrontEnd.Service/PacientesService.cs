using Microsoft.JSInterop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
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
                //List<FarmacosDto>?  listaFarmacos = await Http.GetFromJsonAsync<List<FarmacosDto>>("pacientes/getFarmacos");
                List<FarmacosDto>? listaFarmacos = new();

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

        // Validar Numero de Historia de un paciente
        public async Task<bool> ValidarNumHistoria(string numHistoria) {
            try {
                return await Http.GetFromJsonAsync<bool>($"pacientes/validarNumHistoria/{numHistoria}");
            } catch (Exception) {
                throw;
            }
        }

        // LLamada http para crear paciente
        public async Task<HttpResponseMessage> CrearPaciente(CrearPacienteDto nuevoPaciente) {
            try {
                return await Http.PostAsJsonAsync("pacientes/crearPaciente", nuevoPaciente);
            } catch (Exception) {
                throw;
            }
        }

        // Obtener pacientes de la api y realizar filtrado
        public async Task<List<PacienteDto>?> ObtenerPacientes() {
            try {
                HttpResponseMessage respuesta = await Http.GetAsync("pacientes/obtenerPacientes");
                List<PacienteDto>? pacientes = null;

                // Guardamos el listado de todos los pacientes en session
                if(respuesta.IsSuccessStatusCode) {
                    pacientes = await respuesta.Content.ReadFromJsonAsync<List<PacienteDto>>();
                    await js.SetInSessionStorage("pacientes",  JsonSerializer.Serialize(pacientes));
                }
                return pacientes;
            } catch (Exception) {
                throw;
            }
        }

        // Filtramos los pacientes con los filtros seleccionados
        public async Task<List<PacienteDto>?> FiltrarPacientes(FiltroPacienteDto filtrsPacientes) {
            try {
                // Obtenemos la lista de pacientes de session
                List<PacienteDto>? listaPacientes = JsonSerializer.Deserialize<List<PacienteDto>>(await js.GetFromSessionStorage("pacientes"));

                // Comprobamos si alguna de las propiedades no es null o las que son una lista si contienen elementos
                if (filtrsPacientes.GetType().GetProperties().Any(prop => 
                {
                    var value = prop.GetValue(filtrsPacientes);
                    return value != null &&
                           (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) &&
                           ((IEnumerable)value).Cast<object>().Any(); // Verifica si la lista tiene al menos un elemento

                }))
                {

                    // Obtenemos el listado de pacientes
                    listaPacientes = (from q in listaPacientes where ((filtrsPacientes.Sexo == null || q.Sexo == filtrsPacientes.Sexo) &&
                                      (filtrsPacientes.Talla == null || q.Talla == filtrsPacientes.Talla) && ((filtrsPacientes.EnfermRaras && q.EnfermRaras == "S") || !filtrsPacientes.EnfermRaras && q.EnfermRaras == "N")
                                      ) select q).ToList();

                }
                return listaPacientes;
            } catch (Exception) {
                throw;
            }
        }
    }
}

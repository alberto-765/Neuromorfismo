using Microsoft.JSInterop;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.Service
{
    public class PacientesService : IPacientesService {
        private readonly HttpClient Http;
        private readonly IJSRuntime js;
        private const string clavePacientesSession = "ListadoPacientes";
        public PacientesService(ICrearHttpClient crearHttpClient, IJSRuntime js) {
            Http = crearHttpClient.CrearHttp();
            this.js = js;
        }

        // Obtener todos los médicos que tienen pacientes a su cargo
        public async Task<IEnumerable<UserInfoDto>> ObtenerAllMed() {
            try {
                IEnumerable<UserInfoDto>? medicos = await Http.GetFromJsonAsync<IEnumerable<UserInfoDto>>("pacientes/getMedicosPacientes");

                // Asignamos una lista vacia si el valor devuelto de la llamada es null
                medicos ??= Enumerable.Empty<UserInfoDto>();

                return medicos;
            } catch (Exception) {
                throw;
            }
        }

        // Obtener opciones para filtros farmacos, mutaciones y epilepsias
        public async Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerListas() {
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

        // LLamada http para editar paciente
        public async Task<HttpResponseMessage> EditarPaciente(CrearPacienteDto nuevoPaciente) {
            try {
                return await Http.PutAsJsonAsync("pacientes/editarPaciente", nuevoPaciente);
            } catch (Exception) {
                throw;
            }
        }


        // LLamada http para eliminar paciente
        public async Task<HttpResponseMessage> EliminarPaciente(int idPaciente) {
            try {
                return await Http.DeleteAsync($"pacientes/eliminarPaciente/{idPaciente}");
            } catch (Exception) {
                throw;
            }
        }

        // Obtener pacientes de la api y realizar filtrado
        public async Task<List<CrearPacienteDto>?> ObtenerPacientes() {
            try {
                HttpResponseMessage respuesta = await Http.GetAsync("pacientes/obtenerPacientes");
                List<CrearPacienteDto>? pacientes = null;

                // Guardamos el listado de todos los pacientes en session
                if(respuesta.IsSuccessStatusCode) {
                    pacientes = await respuesta.Content.ReadFromJsonAsync<List<CrearPacienteDto>>();
                }

                // Guardamos pacientes en session
                await GuardarPacientesSession(pacientes);
                return pacientes;
            } catch (Exception) {
                throw;
            }
        }

        // Filtramos los pacientes con los filtros seleccionados
        public async Task<List<CrearPacienteDto>?> FiltrarPacientes(FiltroPacienteDto? filtrsPacientes) {
            try {
                // Comprobamos si existe algun campo por el que filtrar
                List<CrearPacienteDto>? listaPacientes = JsonSerializer.Deserialize<List<CrearPacienteDto>?>(await js.GetFromSessionStorage(clavePacientesSession));
                if (filtrsPacientes is not null && listaPacientes is not null) {
                    // Obtenemos el listado de pacientes
                    listaPacientes = (from q in listaPacientes where (string.IsNullOrWhiteSpace(filtrsPacientes.Sexo) || q.Sexo == filtrsPacientes.Sexo) &&
                                      (filtrsPacientes.FiltrarEnfRara is null || filtrsPacientes.FiltrarEnfRara == q.EnfermRaras) && (filtrsPacientes.Medico is null ||
                                      (q.MedicosPacientes is not null && q.MedicosPacientes.ContainsKey(filtrsPacientes.Medico.IdMedico))) &&
                                      (!filtrsPacientes.TipoEpilepsias.Any() || filtrsPacientes.TipoEpilepsias.Any(t => t.IdEpilepsia == q.Epilepsia?.IdEpilepsia)) &&
                                      (!filtrsPacientes.TipoMutacion.Any() || filtrsPacientes.TipoMutacion.Any(t => t.IdMutacion == q.Mutacion?.IdMutacion))
                                      select q).ToList();
                }
                return listaPacientes;
            } catch (Exception) {
                throw;
            }
        }

        // Filtramos los pacientes para "Mis Pacientes" en caso de ser "SuperAdmin o Admin"
        public List<CrearPacienteDto>? FiltrarMisPacientes(List<CrearPacienteDto>? listaPacientes, ClaimsPrincipal? user) {
            try {

                // Devolvemos la lista porque en los medicos ya tienen filtrados solamente sus pacientes
                if (user == null || user.IsInRole("medico") || listaPacientes == null || listaPacientes.Any() == false) {
                    return listaPacientes;
                }

                // Obtenemos el id del medico solo para admins o superAdmins
                if (int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idMedico) == false || idMedico == 0) {
                    return listaPacientes;
                }

                return listaPacientes.Where(q => q.MedicosPacientes != null && q.MedicosPacientes.ContainsKey(idMedico)).ToList();
            } catch (Exception) {
                throw;
            }
        }

        // Añadir un nuevo paciente creado a la lista de todos los pacientes
        public async Task<List<CrearPacienteDto>?> AnadirPacienteALista(int idPaciente) {
            try {
                // Obtenemos el nuevo paciente creado
                CrearPacienteDto? nuevoPaciente = await Http.GetFromJsonAsync<CrearPacienteDto?>($"pacientes/obtenerPaciente/{idPaciente}");

                // Si el paciente es null creamos una lista vacia
                if (nuevoPaciente is null) {
                   return new();
                }

                string listaPacientesJSON = await js.GetFromSessionStorage(clavePacientesSession);
                List<CrearPacienteDto> listaPacientes = new();

                // Deserealizamos JSON de la lista si no es null y contiene pacientes
                if (!string.IsNullOrWhiteSpace(listaPacientesJSON)) {
                    listaPacientes = JsonSerializer.Deserialize<List<CrearPacienteDto>>(listaPacientesJSON) ?? new();
                }

                // Si el paciente no es null lo insertamos a la lista
                if(listaPacientes is not null) {
                    listaPacientes.Add(nuevoPaciente);
                } 

                // Guardamos listado de pacientes
                await GuardarPacientesSession(listaPacientes);

                return listaPacientes;
            } catch (Exception) {
                throw;
            }
        }

        // Eliminar un paciente de la lista
        public async Task<List<CrearPacienteDto>?> EliminarPacienteLista(int idPaciente) {
            try {

                // Si el paciente no es null lo insertamos a la lista
                List<CrearPacienteDto>? listaPacientes = JsonSerializer.Deserialize<List<CrearPacienteDto>>(await js.GetFromSessionStorage(clavePacientesSession));
                if (listaPacientes != null && listaPacientes.Any()) {
                    int indice = listaPacientes.FindIndex(q => q.IdPaciente == idPaciente);

                    if(indice >= 0) {
                        listaPacientes.RemoveAt(indice);
                    }
                } 

                // Guardamos listado de pacientes
                await GuardarPacientesSession(listaPacientes);

                return listaPacientes;
            } catch (Exception) {
                throw;
            }
        }


        // Guardar lista pacientes en session
        public async Task GuardarPacientesSession(List<CrearPacienteDto>? listaPacientes) {
            try {
                if(listaPacientes != null && listaPacientes.Any()){
                    await js.SetInSessionStorage(clavePacientesSession, JsonSerializer.Serialize(listaPacientes));
                }
            } catch (Exception) {
                throw;
            }
        }
    }
}

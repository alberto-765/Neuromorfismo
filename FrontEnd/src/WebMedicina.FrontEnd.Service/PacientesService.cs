using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics.Metrics;
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
            Http = crearHttpClient.CrearHttpApi();
            this.js = js;
        }

        /// <summary>
        /// Obtener todos los médicos que tienen pacientes a su cargo
        /// </summary>
        /// <param name="medicos"></param>
        /// <param name="busqueda"></param>
        /// <returns>Lista de medicos que cumplan la busqueda</returns>
        public async Task<IEnumerable<UserInfoDto>> ObtenerMedicosConPac(IEnumerable<UserInfoDto>? medicos, string? busqueda) {
            // Si la lista es null se obtiene por primera vez de BD
            medicos ??= await Http.GetFromJsonAsync<IEnumerable<UserInfoDto>>("pacientes/getmedicospacientes");

            // Si hay medicos en la lista se realiza la busqueda
            if (!string.IsNullOrWhiteSpace(busqueda) && medicos != null && medicos.Any()) {
                return medicos.Where(q => ($"{q.UserLogin} {q.Nombre} {q.Apellidos}").Contains(busqueda, StringComparison.OrdinalIgnoreCase));
            }

            // Asignamos una lista vacia si el valor devuelto de la llamada es null
            medicos ??= Enumerable.Empty<UserInfoDto>();

            return medicos; 
        }

        // Obtener opciones para filtros farmacos, mutaciones y epilepsias
        public async Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerListas() { 
            // Obtenemos lista de farmacos
            //List<FarmacosDto>?  listaFarmacos = await Http.GetFromJsonAsync<List<FarmacosDto>>("pacientes/getFarmacos");
            List<FarmacosDto>? listaFarmacos = new();

            // Obtenemos lista de epilepsias
            List<EpilepsiasDto>?  listaEpilepsias = await Http.GetFromJsonAsync<List<EpilepsiasDto>>("pacientes/getepilepsias");

            // Obtenemos lista de mutaciones
            List<MutacionesDto>?  listaMutaciones = await Http.GetFromJsonAsync<List<MutacionesDto>>("pacientes/getmutaciones");

            //Devolvemos las tres listas
            return (listaFarmacos, listaEpilepsias, listaMutaciones); 
        }

        // Validar Numero de Historia de un paciente
        public async Task<bool> ValidarNumHistoria(string numHistoria) { 
            return await Http.GetFromJsonAsync<bool>($"pacientes/validarnumhistoria/{numHistoria}"); 
        }

        // LLamada http para crear paciente
        public async Task<HttpResponseMessage> CrearPaciente(CrearPacienteDto nuevoPaciente) { 
            return await Http.PostAsJsonAsync("pacientes/crearpaciente", nuevoPaciente); 
        }

        // LLamada http para editar paciente
        public async Task<HttpResponseMessage> EditarPaciente(CrearPacienteDto nuevoPaciente) { 
            return await Http.PutAsJsonAsync("pacientes/editarpaciente", nuevoPaciente); 
        }


        // LLamada http para eliminar paciente
        public async Task<HttpResponseMessage> EliminarPaciente(int idPaciente) { 
            return await Http.DeleteAsync($"pacientes/eliminarpaciente/{idPaciente}"); 
        }

        /// <summary>
        /// Obtener pacientes de la api y realizar filtrado
        /// </summary>
        /// <returns>True si se ha obtenido la lista correctamente</returns>
        public async Task<ImmutableList<CrearPacienteDto>?> ObtenerPacientes() { 
            HttpResponseMessage respuesta = await Http.GetAsync("pacientes/obtenerpacientes");
            List<CrearPacienteDto>? pacientes = null;

            // Guardamos el listado de todos los pacientes en session
            if(respuesta.IsSuccessStatusCode) {
                pacientes = await respuesta.Content.ReadFromJsonAsync<List<CrearPacienteDto>>();
            }

            // Guardamos pacientes en session
            await GuardarPacientesSession(pacientes);
            return pacientes?.ToImmutableList();
        }

        // Filtramos los pacientes con los filtros seleccionados
        public async Task<ImmutableList<CrearPacienteDto>?> FiltrarPacientes(FiltroPacienteDto? filtrsPacientes = null) {
            // Comprobamos si existe algun campo por el que filtrar
            ImmutableList<CrearPacienteDto>? listaPacientes = JsonSerializer.Deserialize<ImmutableList<CrearPacienteDto>?>(await js.GetFromSessionStorage(clavePacientesSession));
            if (filtrsPacientes is not null && listaPacientes is not null) {
                // Obtenemos el listado de pacientes
                listaPacientes = (from q in listaPacientes where (string.IsNullOrWhiteSpace(filtrsPacientes.Sexo) || q.Sexo == filtrsPacientes.Sexo) &&
                                    (filtrsPacientes.FiltrarEnfRara is null || filtrsPacientes.FiltrarEnfRara == q.EnfermRaras) && (filtrsPacientes.Medico is null ||
                                    (q.MedicosPacientes is not null && q.MedicosPacientes.ContainsKey(filtrsPacientes.Medico.IdMedico))) &&
                                    (!filtrsPacientes.TipoEpilepsias.Any() || filtrsPacientes.TipoEpilepsias.Any(t => t.IdEpilepsia == q.Epilepsia?.IdEpilepsia)) &&
                                    (!filtrsPacientes.TipoMutacion.Any() || filtrsPacientes.TipoMutacion.Any(t => t.IdMutacion == q.Mutacion?.IdMutacion))
                                    select q).ToImmutableList();
            }
            return listaPacientes; 
        }

        // Filtramos los pacientes para "Mis Pacientes" en caso de ser "SuperAdmin o Admin"
        public ImmutableList<CrearPacienteDto> FiltrarMisPacientes(ImmutableList<CrearPacienteDto> listaPacientes, ClaimsPrincipal? user) { 
            // Devolvemos la lista porque en los medicos ya tienen filtrados solamente sus pacientes
            if (user is null || user.IsInRole("medico")) {
                return listaPacientes;
            }

            // Obtenemos el id del medico solo para admins o superAdmins
            if (int.TryParse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int idMedico) == false || idMedico == 0) {
                return listaPacientes;
            }

            return listaPacientes.Where(q => q.MedicosPacientes != null && q.MedicosPacientes.ContainsKey(idMedico)).ToImmutableList(); 
        }


        // Añadir un nuevo paciente creado a la lista de todos los pacientes
        public async Task<bool> AnadirPacienteALista(int idPaciente) { 
            // Obtenemos el nuevo paciente creado
            CrearPacienteDto? nuevoPaciente = await Http.GetFromJsonAsync<CrearPacienteDto?>($"pacientes/obtenerpaciente/{idPaciente}");

            // Si el paciente es null creamos una lista vacia
            if (nuevoPaciente is not null) {   
                List<CrearPacienteDto> listaPacientes = await ObtenerListaPacienteSession();

                // Si el paciente no es null lo insertamos a la lista
                listaPacientes.Add(nuevoPaciente); 

                // Guardamos listado de pacientes
                await GuardarPacientesSession(listaPacientes);
                return true;
            }

            return false;
        }

        // Eliminar un paciente de la lista
        public async Task<bool> EliminarPacienteLista(int idPaciente) { 
            // Si el paciente no es null lo insertamos a la lista
            List<CrearPacienteDto> listaPacientes = await ObtenerListaPacienteSession();

            if (listaPacientes.Any()) {
                int indice = listaPacientes.FindIndex(q => q.IdPaciente == idPaciente);

                if(indice >= 0) {
                    listaPacientes.RemoveAt(indice);
                }

                // Guardamos listado de pacientes actualizado
                await GuardarPacientesSession(listaPacientes);
                return true;
            }

            return false;
        }

        // Obtener lista de pacientes de session
        public async Task<List<CrearPacienteDto>> ObtenerListaPacienteSession() { 
            string listaPacientesJSON = await js.GetFromSessionStorage(clavePacientesSession);
            List<CrearPacienteDto>? pacientes = null;

            // Deserealizamos JSON de la lista si no es null
            if (!string.IsNullOrWhiteSpace(listaPacientesJSON)) {
                pacientes = JsonSerializer.Deserialize<List<CrearPacienteDto>>(listaPacientesJSON);
            }
            return pacientes ?? new(); 
        }

        // Guardar lista pacientes en session
        public async Task GuardarPacientesSession(List<CrearPacienteDto>? listaPacientes) {
            listaPacientes ??= new(); // si es null asignamos lista vacia
            await js.SetInSessionStorage(clavePacientesSession, JsonSerializer.Serialize(listaPacientes));
        }

        /// <summary>
        /// Reiniciar paciente al cancelar edicion
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <param name="copiaPaciente"></param>
        public void ReiniciarCopiaPaciente(ref CrearPacienteDto nuevoPaciente, CrearPacienteDto copiaPaciente) {
            nuevoPaciente.NumHistoria = copiaPaciente.NumHistoria;
            nuevoPaciente.FechaNac = copiaPaciente.FechaNac;
            nuevoPaciente.Sexo = copiaPaciente.Sexo;
            nuevoPaciente.Talla = copiaPaciente.Talla;
            nuevoPaciente.MedicosPacientes = copiaPaciente.MedicosPacientes;
            nuevoPaciente.FechaDiagnostico = copiaPaciente.FechaDiagnostico;
            nuevoPaciente.FechaFractalidad = copiaPaciente.FechaFractalidad;
            nuevoPaciente.Farmaco = copiaPaciente.Farmaco;
            nuevoPaciente.EnfermRaras = copiaPaciente.EnfermRaras;
            nuevoPaciente.DescripEnferRaras = copiaPaciente.DescripEnferRaras;
            nuevoPaciente.Mutacion = copiaPaciente.Mutacion;
            nuevoPaciente.Epilepsia = copiaPaciente.Epilepsia;
        }

        /// <summary>
        /// Obtener paciente de session
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>PacienteDto o null del Id pasado</returns>
        public async Task<CrearPacienteDto?> ObtenerPacienteSession(int idPaciente) {
            // Obtenemos todos los pacientes de session
            List<CrearPacienteDto> pacientes = await ObtenerListaPacienteSession();
            CrearPacienteDto? paciente = null;

            // Obtenemos los datos de la etapa del cliente
            if (pacientes.Any()) {
                paciente = pacientes.Find(q => q.IdPaciente == idPaciente);
            }

            return paciente;
        }
    }
}

using System.Collections.Immutable;
using System.Security.Claims;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IPacientesService {
        Task<IEnumerable<UserInfoDto>> ObtenerAllMed(IEnumerable<UserInfoDto>? medicos, string? busqueda);
        Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerListas();
        Task<bool> ValidarNumHistoria(string numHistoria);
        Task<HttpResponseMessage> CrearPaciente(CrearPacienteDto nuevoPaciente);
        Task<HttpResponseMessage> EditarPaciente(CrearPacienteDto nuevoPaciente);
        Task<HttpResponseMessage> EliminarPaciente(int idPaciente);
        Task<ImmutableList<CrearPacienteDto>?> ObtenerPacientes();
        Task<ImmutableList<CrearPacienteDto>?> FiltrarPacientes(FiltroPacienteDto? filtrsPacientes = null);
        ImmutableList<CrearPacienteDto> FiltrarMisPacientes(ImmutableList<CrearPacienteDto> listaPacientes, ClaimsPrincipal? user);
        Task<bool> AnadirPacienteALista(int idPaciente);
        Task<bool> EliminarPacienteLista(int idPaciente);
        void ReiniciarCopiaPaciente(ref CrearPacienteDto nuevoPaciente, CrearPacienteDto copiaPaciente);
        Task<List<CrearPacienteDto>> ObtenerListaPacienteSession();
        Task<CrearPacienteDto?> ObtenerPacienteSession(int idPaciente);
        Task GuardarPacientesSession(List<CrearPacienteDto>? listaPacientes);
    }
}

using System.Security.Claims;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IPacientesService {
        Task<IEnumerable<UserInfoDto>> ObtenerAllMed();
        Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerListas();
        Task<bool> ValidarNumHistoria(string numHistoria);
        Task<HttpResponseMessage> CrearPaciente(CrearPacienteDto nuevoPaciente);
        Task<HttpResponseMessage> EditarPaciente(CrearPacienteDto nuevoPaciente);
        Task<HttpResponseMessage> EliminarPaciente(int idPaciente);
        Task<List<CrearPacienteDto>?> ObtenerPacientes();
        Task<List<CrearPacienteDto>?> FiltrarPacientes(FiltroPacienteDto? filtrsPacientes);
        List<CrearPacienteDto>? FiltrarMisPacientes(List<CrearPacienteDto>? listaPacientes, ClaimsPrincipal? user);
        Task<List<CrearPacienteDto>?> AnadirPacienteALista(int idPaciente);
        Task<List<CrearPacienteDto>?> EliminarPacienteLista(int idPaciente);
        void ReiniciarCopiaPaciente(ref CrearPacienteDto nuevoPaciente, CrearPacienteDto copiaPaciente);
        Task<List<CrearPacienteDto>> ObtenerListaPacienteSession();
        Task GuardarPacientesSession(List<CrearPacienteDto>? listaPacientes);
    }
}

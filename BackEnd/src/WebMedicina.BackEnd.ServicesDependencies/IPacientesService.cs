using System.Security.Claims;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IPacientesService {
        Task<IEnumerable<UserInfoDto>> GetAllMed();
        List<FarmacosDto> ObtenerFarmacos();
        List<MutacionesDto> ObtenerMutaciones();
        List<EpilepsiasDto> ObtenerEpilepsias();
        bool ValidarNumHistoria(string numHistoria);
        Task<int> CrearPaciente(CrearPacienteDto nuevoPaciente, int idMedico);
        Task<bool> EditarPaciente(CrearPacienteDto nuevoPaciente, int idMedico);
        Task<bool> EliminarPaciente(int idPaciente);
        List<CrearPacienteDto> ObtenerPacientes(ClaimsPrincipal user);
        Task<bool> ValidarPermisosEdicYElim(ClaimsPrincipal? user, int idPaciente);
        Task<CrearPacienteDto?> GetUnPaciente(int idPaciente);
    }
}

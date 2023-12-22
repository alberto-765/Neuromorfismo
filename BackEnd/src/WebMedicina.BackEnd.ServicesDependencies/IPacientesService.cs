using System.Security.Claims;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IPacientesService {
        Task<IEnumerable<int>> GetAllMed();
        List<FarmacosDto> ObtenerFarmacos();
        List<MutacionesDto> ObtenerMutaciones();
        List<EpilepsiasDto> ObtenerEpilepsias();
        bool ValidarNumHistoria(string numHistoria);
        Task<bool> CrearPaciente(CrearPacienteDto nuevoPaciente, int idMedico);
        Task<bool> EditarPaciente(CrearPacienteDto nuevoPaciente, int idMedico);
        Task<bool> EliminarPaciente(int idPaciente, int idMedico);
        List<CrearPacienteDto> ObtenerPacientes(ClaimsPrincipal user);
        Task<bool> ValidarPermisosEdicYElim(int idMedico, int idPaciente);
    }
}

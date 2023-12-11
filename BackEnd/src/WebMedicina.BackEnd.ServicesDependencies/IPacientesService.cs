using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IPacientesService {
        Task<IEnumerable<int>> GetAllMed();
        List<FarmacosDto> ObtenerFarmacos();
        List<MutacionesDto> ObtenerMutaciones();
        List<EpilepsiasDto> ObtenerEpilepsias();
        bool ValidarNumHistoria(string numHistoria);
        Task<bool> CrearPaciente(CrearPacienteDto nuevoPaciente, int idMedico);
        List<PacienteDto> ObtenerPacientes();
    }
}

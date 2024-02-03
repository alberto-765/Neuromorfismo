using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IDocumentacionService {
        Task<bool> DescargarExcelPacientes(ExcelPacientesDto excelPacientes);
        Task EnviarEmailEvoActu(EvolucionLTDto evolucion, int idPaciente, string idContenedor);
    }
}

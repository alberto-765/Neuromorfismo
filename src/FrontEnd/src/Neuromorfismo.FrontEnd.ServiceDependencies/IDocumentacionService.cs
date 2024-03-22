using Neuromorfismo.Shared.Dto.LineaTemporal;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.FrontEnd.ServiceDependencies {
    public interface IDocumentacionService {
        Task<bool> DescargarExcelPacientes(ExcelPacientesDto excelPacientes);
        Task EnviarEmailEvoActu(EvolucionLTDto evolucion, int idPaciente, string idContenedor);
    }
}

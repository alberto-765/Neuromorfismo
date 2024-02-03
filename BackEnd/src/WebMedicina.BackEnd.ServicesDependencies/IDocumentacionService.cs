
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IDocumentacionService {
        MemoryStream GenerarExcelPacientes(List<PacienteExcelDto> pacientes, string nombrePaginaExcel);
        (string, string) GenerarCorreo(EmailEditarEvoDto datosEmail);
    }
}

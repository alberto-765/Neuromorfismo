
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IDocumentacionService {

        /// <summary>
        /// Generar excel de la lista de pacientes solicitiada
        /// </summary>
        /// <param name="listaPacientes"></param>
        /// <param name="nombrePagina"></param>
        /// <returns></returns>
        MemoryStream GenerarExcelPacientes(List<PacienteExcelDto> pacientes, string nombrePaginaExcel);

        /// <summary>
        /// Generamos asunto y mensaje del correo
        /// </summary>
        /// <param name="datosEmail"></param>
        /// <param name="userInfo"></param>
        /// <returns>Tupla con asunto y mensaje</returns>
        (string, string) GenerarCorreo(EmailEditarEvoDto datosEmail, UserInfoDto userInfo);
    }
}

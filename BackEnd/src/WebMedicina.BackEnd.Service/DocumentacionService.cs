using ClosedXML.Excel;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.Service {
    public class DocumentacionService : IDocumentacionService {
        private readonly PacientesDal _pacientesDal;
        private readonly LineaTemporalDal _lineaTemporalDal;

        public DocumentacionService(PacientesDal pacientesDal, LineaTemporalDal lineaTemporalDal) {
            _pacientesDal = pacientesDal;
            _lineaTemporalDal = lineaTemporalDal;
        }

        /// <summary>
        /// Generar excel de la lista de pacientes solicitiada
        /// </summary>
        /// <param name="listaPacientes"></param>
        /// <param name="nombrePagina"></param>
        /// <returns></returns>
        public MemoryStream GenerarExcelPacientes(List<PacienteExcelDto> pacientes, string nombrePaginaExcel) {
            // Creamos libro de excel
            using XLWorkbook libroExcel = new();

            // Creamos pagina
            var paginaExcel = libroExcel.AddWorksheet(nombrePaginaExcel);

            // Creamos tabla en la pagina e insertamos la lista de pacientes
            var tabla = paginaExcel.FirstCell().InsertTable(pacientes);

            // Activamos autofilter
            tabla.SetShowAutoFilter(true).SetEmphasizeFirstColumn(true);

            // Ajustamos el ancho de las columnas
            paginaExcel.Columns().AdjustToContents();

            // Fijar la primera fila
            paginaExcel.SheetView.FreezeRows(1);

            // Centramos todas las columnas
            paginaExcel.CellsUsed().Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            // Guardamos el excel en memoria RAM
            using MemoryStream memoryStream = new();
            libroExcel.SaveAs(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }


        /// <summary>
        /// Generamos asunto y mensaje del correo
        /// </summary>
        /// <param name="datosEmail"></param>
        /// <param name="userInfo"></param>
        /// <returns>Tupla con asunto y mensaje</returns>
        public (string, string) GenerarCorreo(EmailEditarEvoDto datosEmail, UserInfoDto userInfo) {
            // Obtenemos el numero de historia del paciente
            string? numHistoria = _pacientesDal.GetNumHistoria(datosEmail.IdPaciente);
            string asunto = string.Empty;
            string mensaje = string.Empty;
            // Obtenemos etaspa
            EtapaLTModel? etapa = _lineaTemporalDal.GetEtapa(datosEmail.Evolucion.IdEtapa);
            if (!string.IsNullOrWhiteSpace(numHistoria) && etapa is not null) {
                asunto = $"Actualización en la evolución del paciente {numHistoria}";
                mensaje = $@"<html>
                    <body>
                    <h2>{etapa.Titulo}.</h2>
                    <p>{etapa.Label}: <b>{(datosEmail.Evolucion.Confirmado ? "Sí" : "No")}.</b></p>
                    <p>Actualización registrada el {datosEmail.Evolucion.Fecha:D} a las {datosEmail.Evolucion.Fecha:t} por {userInfo.Nombre} {userInfo.Apellidos}.</p>
                    <img src='cid:capturaEvo' width='100%'/>
                    </body></html>
                    ";
            }

            return (asunto, mensaje);
        }
    }
}

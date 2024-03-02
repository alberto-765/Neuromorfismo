using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IEstadisticasService {

        /// <summary>
        /// Obtener estadísticas de pacientes y evoluciones
        /// </summary>
        /// <returns></returns>
        Task<EstadisticasDto> ObtenerEstadisitcas();
    }
}

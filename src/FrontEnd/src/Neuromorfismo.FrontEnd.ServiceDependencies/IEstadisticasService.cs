using Neuromorfismo.Shared.Dto.Estadisticas;

namespace Neuromorfismo.FrontEnd.ServiceDependencies {
    public interface IEstadisticasService {

        /// <summary>
        /// Obtener estadísticas de pacientes y evoluciones
        /// </summary>
        /// <returns></returns>
        Task<EstadisticasDto> ObtenerEstadisitcas();
    }
}

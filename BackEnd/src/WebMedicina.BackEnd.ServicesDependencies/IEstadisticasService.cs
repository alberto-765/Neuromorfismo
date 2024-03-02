using System.Collections.Immutable;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IEstadisticasService {

        /// <summary>
        /// Obtener 
        /// </summary>
        /// <returns></returns>
        Task<ImmutableList<TotalPacientesDto>> ObtenerTotalPaciente();
    }
}

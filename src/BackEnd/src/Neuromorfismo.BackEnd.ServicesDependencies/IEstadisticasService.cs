using System.Collections.Immutable;
using Neuromorfismo.Shared.Dto.Estadisticas;

namespace Neuromorfismo.BackEnd.ServicesDependencies {
    public interface IEstadisticasService {

        /// <summary>
        /// Devuelve objeto con todos los datos necesarios para las graficas
        /// </summary>
        /// <returns></returns>
        EstadisticasDto ObtenerDatosGraficas();
    }
}

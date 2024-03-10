using System.Collections.Immutable;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IEstadisticasService {

        /// <summary>
        /// Devuelve objeto con todos los datos necesarios para las graficas
        /// </summary>
        /// <returns></returns>
        EstadisticasDto ObtenerDatosGraficas();
    }
}

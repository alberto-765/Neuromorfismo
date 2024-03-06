using System.Collections.Immutable;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IEstadisticasService {

        /// <summary>
        /// Obtener diccionario con fechas de creación y número de pacientes creados en esa fecha
        /// </summary>
        /// <returns>TotalPacientes y TotalMedicos</returns>
        (ImmutableSortedDictionary<DateOnly, uint> TotalPacientes, ImmutableSortedDictionary<DateOnly, uint> TotalMedicos) ObtenerTotales();

        /// <summary>
        /// Obtener diccionario con las etapas y la cantidad de pacientes en dicha etapa
        /// </summary>
        /// <returns>ImmutableSortedDictionary<string, uint></returns>
        ImmutableDictionary<string, uint> ObtenerResumenEtapas();
    }
}

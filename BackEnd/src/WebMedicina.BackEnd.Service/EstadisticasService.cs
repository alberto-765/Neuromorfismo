using System.Collections.Immutable;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;


namespace WebMedicina.BackEnd.Service;
public class EstadisticasService : IEstadisticasService {
    private readonly EstadisticasDal _estadisticasDal;

    public EstadisticasService(EstadisticasDal estadisticasDal) {
        _estadisticasDal = estadisticasDal;
    }

    /// <summary>
    /// Obtener diccionario con las etapas y la cantidad de pacientes en dicha etapa
    /// </summary>
    /// <returns>ImmutableSortedDictionary<string, uint></returns>
    public ImmutableDictionary<string, uint> ObtenerResumenEtapas() {
        return _estadisticasDal.GetTotalEtapas();
    }

    /// <summary>
    /// Obtener diccionario con fechas de creación y número de pacientes creados en esa fecha
    /// </summary>
    /// <returns>TotalPacientes y TotalMedicos</returns>
    public (ImmutableSortedDictionary<DateOnly, uint> TotalPacientes, ImmutableSortedDictionary<DateOnly, uint> TotalMedicos) ObtenerTotales() {
        
        return (_estadisticasDal.GetTotalPacientes(), _estadisticasDal.GetTotalMedicos());
    }
}


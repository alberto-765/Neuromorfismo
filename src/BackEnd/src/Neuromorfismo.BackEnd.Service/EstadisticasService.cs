using System.Collections.Immutable;
using Neuromorfismo.BackEnd.Dal;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.Shared.Dto.Estadisticas;

namespace Neuromorfismo.BackEnd.Service;
public class EstadisticasService : IEstadisticasService {
    // DEPENDENCIAS
    private readonly EstadisticasDal _estadisticasDal;


    // PROPIEDADES
    private const float PromedioDiaPorMes = 30.4167F;
    private ImmutableSortedDictionary<DateTime, uint> TotalPacientes = ImmutableSortedDictionary<DateTime, uint>.Empty;   // Total pacientes en cada fecha
    private ImmutableSortedDictionary<DateTime, uint> TotalMedicos = ImmutableSortedDictionary<DateTime, uint>.Empty;   // Total medicos en cada fecha
    private EstadisticasDto Estadisticas = new();

    public EstadisticasService(EstadisticasDal estadisticasDal) {
        _estadisticasDal = estadisticasDal;
    }


    /// <summary>
    /// Devuelve objeto con todos los datos necesarios para las graficas
    /// </summary>
    /// <returns></returns>
    public EstadisticasDto ObtenerDatosGraficas() {
        // Total pacientes en cada etapa
        Estadisticas.TotalEtapas = _estadisticasDal.GetTotalEtapas();
        TotalPacientes = _estadisticasDal.GetTotalPacientes();
        TotalMedicos = _estadisticasDal.GetTotalPacientes();

        // Mapeamos y obtenemos el rango de fechas y el número de pacientes/medicos en cada una
        MapearLabelsYTotales();

        return Estadisticas;
    }


    /// <summary>
    /// Mapear los datos totales pasados por referencia, obtener el rango de fechas para la gráfica y el número de pacientes/medicos
    /// </summary>
    private void MapearLabelsYTotales () {
        // Calculamos el rango de meses optimo para los labels
        ImmutableArray<DateTime> AllFechas = TotalPacientes.Concat(TotalMedicos).Select(q => q.Key).ToImmutableArray();
        DateTime fechaMin = AllFechas.Min();
        DateTime fechaMax = AllFechas.Max();

        // Calculamos la diferencia entre la fecha maxima y minima
        TimeSpan difFechas = fechaMax - fechaMin;
        string formato = fechaMin.Year != fechaMax.Year ? "Y" : "MMM";

        // Calculamos los saltos en meses que habría que hacer para pintar todo el rango de fechas
        short saltosEnMeses = Convert.ToInt16((difFechas.TotalDays / PromedioDiaPorMes) / EstadisticasDto.NumLabels);

        // Si el salto en meses entre cada fecha es menor a 12 meses (1 año) el salto será mensual
        if (saltosEnMeses < 12) {
            if (saltosEnMeses < 1) {
                saltosEnMeses = 1;
            }

            // Creamos los labels para la gráfica y las cantidades para cada label
            for (byte i = 0; i < EstadisticasDto.NumLabels; i++) {
                Estadisticas.LabelsXGrafTotales[i] = fechaMin.ToString(formato);
                fechaMin = fechaMin.AddMonths(saltosEnMeses);
                ObtenerTotales(i, fechaMin);
            }
        } else {

            // Calculamos el salto en años 
            saltosEnMeses = Convert.ToInt16(saltosEnMeses);
            fechaMin = new(fechaMin.Year, 1, 1);

            // Creamos los labels para la gráfica y las cantidades para cada label
            for (byte i = 0; i < EstadisticasDto.NumLabels; i++) {
                Estadisticas.LabelsXGrafTotales[i] = fechaMin.ToString("yyy");
                fechaMin = fechaMin.AddYears(saltosEnMeses);
                ObtenerTotales(i, fechaMin);
            }
        }
    }

    /// <summary>
    /// Obtenemos los totales menores a la ultima fecha
    /// </summary>
    /// <param name="indice"></param>
    /// <param name="fechaMax"></param>
    private void ObtenerTotales(byte indice, DateTime fechaMax) {
        Estadisticas.TotalMedicos[indice] = TotalMedicos.Where(q => q.Key < fechaMax).Sum(q => q.Value);
        Estadisticas.TotalPacientes[indice] = TotalPacientes.Where(q => q.Key < fechaMax).Sum(q => q.Value);
    }
}


using System.Collections.Immutable;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.BackEnd.Service;
public class EstadisticasService : IEstadisticasService {
    private const float PromedioDiaPorMes = 30.4167;

    private readonly EstadisticasDal _estadisticasDal;

    public EstadisticasService(EstadisticasDal estadisticasDal) {
        _estadisticasDal = estadisticasDal;
    }

    /// <summary>
    /// Obtener diccionario con las etapas y la cantidad de pacientes en dicha etapa
    /// </summary>
    /// <returns>ImmutableSortedDictionary<string, uint></returns>
    public ImmutableDictionary<string, uint> ObtenerResumenEtapas() {
        return
    }

    /// <summary>
    /// Obtener diccionario con fechas de creación y número de pacientes creados en esa fecha
    /// </summary>
    /// <returns>TotalPacientes y TotalMedicos</returns>
    public (ImmutableSortedDictionary<DateOnly, uint> TotalPacientes, ImmutableSortedDictionary<DateOnly, uint> TotalMedicos) ObtenerTotales() {
        
        return (, _estadisticasDal.GetTotalMedicos());
    }

    /// <summary>
    /// Devuelve objeto con todos los datos necesarios para las graficas
    /// </summary>
    /// <returns></returns>
    public EstadisticasDto ObtenerDatosGraficas() {
        EstadisticasDto estadisticas = new() {
        // Total pacientes en cada etapa
            TotalEtapas = _estadisticasDal.GetTotalEtapas()
        };

        // Total pacientes en cada fecha
        ImmutableSortedDictionary<DateTime, uint>  TotalPacientes = _estadisticasDal.GetTotalPacientes();
        // Total medicos en cada fecha
        ImmutableSortedDictionary<DateTime, uint>  TotalMedicos = _estadisticasDal.GetTotalPacientes();
        
        // Mapeamos y obtenemos el rango de fechas y el número de pacientes/medicos en cada una

        
        estadisticas.TotalMedicos = _estadisticasDal.GetTotalPacientes();
    }


    /// <summary>
    /// Mapear los datos totales pasados por referencia, obtener el rango de fechas para la gráfica y el número de pacientes/medicos
    /// </summary>
    /// <param name="estadisticas"></param>
    public void MapearLabelsYTotales (ref EstadisticasDto estadisticas, ImmutableSortedDictionary<DateTime, uint> TotalPacientes, ImmutableSortedDictionary<DateTime, uint> TotalMedicos) {
        // Calculamos el rango de meses optimo para los labels
        ImmutableArray<DateTime> AllFechas = TotalPacientes.Concat(TotalMedicos).Select(q => q.Key).ToImmutableArray();
        DateTime fechaMin = AllFechas.Min();
        DateTime fechaMax = AllFechas.Max();

        // Calculamos la diferencia entre la fecha maxima y minima
        TimeSpan difFechas = fechaMax - fechaMin;
        bool cambiaAnio = fechaMin.Year != fechaMax.Year;

        // Calculamos los saltos en meses que habría que hacer para pintar todo el rango de fechas
        short saltosEnMesesAnios = Convert.ToInt16((difFechas.TotalDays / PromedioDiaPorMes) / EstadisticasDto.NumLabels);

        // Si el salto en meses entre cada fecha es menor a 12 meses (1 año) el salto será mensual
        if (saltosEnMesesAnios < 12) {
            if (saltosEnMesesAnios < 1) {
                saltosEnMesesAnios = 1;
            }

            // Creamos los labels para la gráfica y las cantidades para cada label
            for (byte i = 0; i < EstadisticasDto.NumLabels; i++) {
                string formato = cambiaAnio ? "Y" : "MMM";
                estadisticas.LabelsXGrafTotales[i] = fechaMin.ToString("MMM");
                fechaMin.AddMonths(saltosEnMesesAnios);

                // Obtenemos los totales menores a la ultima fecha
                estadisticas.TotalMedicos[i] = (uint)TotalMedicos.Where(q => q.Key < fechaMin).Sum(q => q.Value);
                estadisticas.TotalPacientes[i] = (uint)TotalPacientes.Where(q => q.Key < fechaMin).Sum(q => q.Value);
            }
        } else {

            // Si el salto es anual dividimos entre 12 y redondeamos
            saltosEnMesesAnios = (short)Math.Round((float)saltosEnMesesAnios / 12);

            // Creamos los labels para la gráfica y las cantidades para cada label
            for (byte i = 0; i < EstadisticasDto.NumLabels; i++) {
                estadisticas.LabelsXGrafTotales[i] = fechaMin.ToString("MMM");
                fechaMin.AddYears(saltosEnMesesAnios);

                // Obtenemos los totales menores a la ultima fecha
                estadisticas.TotalMedicos[i] = (uint)TotalMedicos.Where(q => q.Key < fechaMin).Sum(q => q.Value);
                estadisticas.TotalPacientes[i] = (uint)TotalPacientes.Where(q => q.Key < fechaMin).Sum(q => q.Value);
            }
        }


        // Si hay menos de 6 meses
        if((difFechas.TotalDays / PromedioDiaPorMes) <= 6) {
            while(indiceLabel < EstadisticasDto.NumLabels) {
                estadisticas.LabelsXGrafTotales[indiceLabel] = fechaMin.ToString("MMM");
                fechaMin.AddMonths(1);
            }

            // Si hay menos 12 meses
        } else if ((difFechas.TotalDays / PromedioDiaPorMes) <= 12) {
            while (indiceLabel < EstadisticasDto.NumLabels) {
                estadisticas.LabelsXGrafTotales[indiceLabel] = fechaMin.ToString("MMM");
                fechaMin.AddMonths(2);
            }

            // Si hay menos de 18 meses
        } else if () {
            while (indiceLabel < EstadisticasDto.NumLabels) {
                estadisticas.LabelsXGrafTotales[indiceLabel] = fechaMin.ToString("MMM");
                fechaMin.AddMonths(2);
            }
        }
        } else {
        }
    }
}


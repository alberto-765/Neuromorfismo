using System.Collections.Immutable;

namespace WebMedicina.Shared.Dto.Estadisticas {

    public class EstadisticasDto {
        // Gráfica de pacientes totales
        public string[] LabelsXGrafTotales { get; set; } = new string[NumLabels]; // Meses o años que aparecen en la grafica
        public double[] TotalPacientes { get; set; } = new double[NumLabels];
        public double[] TotalMedicos { get; set; } = new double[NumLabels];


        // Gráfica resumen evoluciones en las etapas -> Etapa - Nº Pac con Etapa
        public ImmutableDictionary<string, double> TotalEtapas { get; set; } = ImmutableDictionary<string, double>.Empty;


        // Número de labels de la grafica total
        public static byte NumLabels { get; } = 6;
    }
}

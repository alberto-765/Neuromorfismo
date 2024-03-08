using System.Collections.Immutable;

namespace WebMedicina.Shared.Dto.Estadisticas {

    public class EstadisticasDto {
        // Gráfica de pacientes totales
        public string[] LabelsXGrafTotales { get; set; } = new string[NumLabels]; // Meses o años que aparecen en la grafica
        public uint[] TotalPacientes { get; set; } = new uint[NumLabels];
        public uint[] TotalMedicos { get; set; } = new uint[NumLabels];


        // Gráfica resumen evoluciones en las etapas -> Etapa - Nº Pac con Etapa
        public ImmutableDictionary<string, uint> TotalEtapas { get; set; } = ImmutableDictionary<string, uint>.Empty;


        // Número de labels de la grafica total
        public static byte NumLabels { get; } = 6;
    }
}

using System.Collections.Immutable;

namespace WebMedicina.Shared.Dto.Estadisticas {

    public class EstadisticasDto {
        // Gráfica de pacientes totales
        public ImmutableSortedDictionary<DateOnly, uint> TotalPacientes { get; set; } = ImmutableSortedDictionary<DateOnly, uint>.Empty;
        public ImmutableSortedDictionary<DateOnly, uint> TotalMedicos { get; set; } = ImmutableSortedDictionary<DateOnly, uint>.Empty;

        // Gráfica resumen evoluciones en las etapas
        public ImmutableDictionary<string, uint> TotalEtapas { get; set; } = ImmutableDictionary<string, uint>.Empty;

    }
}

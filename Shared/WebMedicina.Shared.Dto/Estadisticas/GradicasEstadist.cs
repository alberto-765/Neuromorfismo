using System.Collections.Immutable;

namespace WebMedicina.Shared.Dto.Estadisticas {
    // Gráfica de pacientes totales
    public record TotalPacientesDto(DateOnly Fecha, uint Total);

    // Gráfica de evoluciones en las etapas
    public record TotalEtapaDto(string Etapa, uint Total);

    public class EstadisticasDto {
        public ImmutableSortedDictionary<DateOnly, uint> TotalPacientes { get; set; } = ImmutableSortedDictionary<DateOnly, uint>.Empty;
        public <TotalEtapaDto> TotalEtapas { get; set; } = ImmutableList<TotalEtapaDto>.Empty;

    }
}

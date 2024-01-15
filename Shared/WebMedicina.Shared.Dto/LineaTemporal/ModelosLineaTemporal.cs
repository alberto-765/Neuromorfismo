
namespace WebMedicina.Shared.Dto.LineaTemporal {
    public record RequestActEvo (int IdPaciente, EvolucionLTDto Evolucion);
    public record EvolucionLTDto (int Id, bool Confirmado, DateTime Fecha, int IdMedicoUltModif, int IdEtapa, int IdPaciente);
    public record class EtapaLTDto (int Id, string Titulo, string Label, string? Descripcion, int? IdMedicoCreador, int? IdMedicoUltModif);
}

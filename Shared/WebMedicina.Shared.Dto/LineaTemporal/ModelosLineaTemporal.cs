
namespace WebMedicina.Shared.Dto.LineaTemporal {
    public record EvolucionLTDto (int Id, bool Confirmado, DateTime Fecha, int IdMedicoUltModif, int IdEtapa, int IdPaciente);
    public record EtapaLTDto (int Id, string Titulo, string Label, string? Descripcion, int? IdMedicoCreador, int? IdMedicoUltModif);
    public record LLamadaEditarEvoDto (EditarEvolucionLTDto Evolucion, int UltimaEtapaPaciente, int IdPaciente);
    public record EmailEditarEvoDto (EvolucionLTDto Evolucion, int IdPaciente, string ImgBase64);
}

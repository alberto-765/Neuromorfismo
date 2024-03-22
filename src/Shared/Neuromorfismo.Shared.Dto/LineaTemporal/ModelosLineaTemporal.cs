
namespace Neuromorfismo.Shared.Dto.LineaTemporal {
    public record EvolucionLTDto (int Id, bool Confirmado, DateTime Fecha, int IdMedicoUltModif, short IdEtapa, int IdPaciente);
    public record EtapaLTDto (short Id, string Titulo, string Label, string? Descripcion, int? IdMedicoCreador, int? IdMedicoUltModif);
    public record LLamadaEditarEvoDto (EditarEvolucionLTDto Evolucion,int IdPaciente);
    public record EmailEditarEvoDto (EvolucionLTDto Evolucion, int IdPaciente, string ImgBase64);
}

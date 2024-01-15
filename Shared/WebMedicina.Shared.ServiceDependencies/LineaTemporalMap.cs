using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.Shared.Mapers {
    public static class LineaTemporalMap {
        public static EditarEvolucionLTDto ToEditDto(this EvolucionLTDto evolucion) =>
            new() {
                Id = evolucion.Id,
                Confirmado = evolucion.Confirmado,
                Fecha = evolucion.Fecha,
                IdEtapa = evolucion.IdEtapa,
                IdMedicoUltModif = evolucion.IdMedicoUltModif
            };

        public static EvolucionLTDto ToDto(this EditarEvolucionLTDto evolucion) =>
            new(evolucion.Id, evolucion.Confirmado, evolucion.Fecha, evolucion.IdMedicoUltModif, evolucion.IdEtapa, evolucion.IdPaciente);
    }
}

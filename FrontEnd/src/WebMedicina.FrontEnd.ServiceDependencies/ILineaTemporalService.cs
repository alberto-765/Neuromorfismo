using System.Collections.Immutable;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.ServiceDependencies
{
    public interface ILineaTemporalService {
        Task<ImmutableSortedDictionary<short, EtapaLTDto>?> ObtenerEtapas();
        Task<SortedList<short, EvolucionLTDto>> ObtenerEvolucionPaciente(int idPaciente);
        Task<SortedList<short, EvolucionLTDto>> ActEvoPac(LLamadaEditarEvoDto evoEditada);
    }
}

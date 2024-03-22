using System.Collections.Immutable;
using Neuromorfismo.Shared.Dto.LineaTemporal;
using Neuromorfismo.Shared.Dto.Pacientes;

namespace Neuromorfismo.FrontEnd.ServiceDependencies
{
    public interface ILineaTemporalService {
        Task<ImmutableSortedDictionary<short, EtapaLTDto>?> ObtenerEtapas();
        Task<SortedList<short, EvolucionLTDto>> ObtenerEvolucionPaciente(int idPaciente);
        Task<SortedList<short, EvolucionLTDto>> ActEvoPac(LLamadaEditarEvoDto evoEditada);
    }
}

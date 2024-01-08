
using System.Collections.Immutable;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies
{
    public interface ILineaTemporalService {
        ImmutableSortedDictionary<int, EtapaLTDto> GetEtapas();
        Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucion(int idPaciente);
    }
}

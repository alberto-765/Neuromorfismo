using System.Collections.Immutable;
using WebMedicina.Shared.Dto.LineaTemporal;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.ServiceDependencies
{
    public interface ILineaTemporalService {
        Task<ImmutableSortedDictionary<int, EtapaLTDto>?> ObtenerEtapas();
        Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucionPaciente(int idPaciente);
        Task<SortedList<int, EvolucionLTDto>> ActEvoPac(LLamadaEditarEvoDto evoEditada);
    }
}

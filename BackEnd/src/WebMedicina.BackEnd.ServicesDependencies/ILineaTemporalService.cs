
using System.Collections.Immutable;
using System.Security.Claims;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies
{
    public interface ILineaTemporalService {
        ImmutableSortedDictionary<int, EtapaLTDto> GetEtapas();
        Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucion(int idPaciente);
        Task<SortedList<int, EvolucionLTDto>> ActOInsertEvolucion(LLamadaEditarEvoDto evoEditada, ClaimsPrincipal User);
    }
}


using System.Collections.Immutable;
using System.Security.Claims;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies
{
    public interface ILineaTemporalService {
        ImmutableSortedDictionary<short, EtapaLTDto> GetEtapas();
        Task<SortedList<short, EvolucionLTDto>> ObtenerEvoluciones(int idPaciente);
        Task<SortedList<short, EvolucionLTDto>> ActOInsertEvolucion(LLamadaEditarEvoDto evoEditada, ClaimsPrincipal User);
    }
}

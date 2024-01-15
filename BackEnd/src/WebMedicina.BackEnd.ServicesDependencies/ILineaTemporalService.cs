
using System.Collections.Immutable;
using System.Security.Claims;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies
{
    public interface ILineaTemporalService {
        ImmutableSortedDictionary<int, EtapaLTDto> GetEtapas();
        Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucion(int idPaciente);
        Task<SortedList<int, EvolucionLTDto>> ActualizarEvolucion(RequestActEvo request, ClaimsPrincipal User);
        Task<SortedList<int, EvolucionLTDto>> InsertarEvolucion(RequestActEvo request, ClaimsPrincipal User);
    }
}

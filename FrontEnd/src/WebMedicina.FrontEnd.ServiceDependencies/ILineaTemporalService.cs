using System.Collections.Immutable;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface ILineaTemporalService {
        Task<ImmutableSortedDictionary<int, EtapasDto>?> ObtenerEtapas();
    }
}

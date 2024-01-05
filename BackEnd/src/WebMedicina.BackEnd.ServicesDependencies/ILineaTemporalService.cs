
using System.Collections.Immutable;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface ILineaTemporalService {
        ImmutableSortedDictionary<int, EtapasDto> GetEtapas();
    }
}

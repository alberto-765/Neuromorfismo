using System.Collections.Immutable;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.BackEnd.Dal {
    public class LineaTemporalDal {
        private readonly WebmedicinaContext _context;

        public LineaTemporalDal(WebmedicinaContext context) {
            _context = context;
        }

        // Obtener diccionario ordenado de las etapas, si no hay devolvemos uno vacio
        public ImmutableSortedDictionary<int, EtapasDto> GetEtapas() {
			try {
                return _context.EtapaLTModel.ToImmutableSortedDictionary(q => q.Id, q=> q.ToDto()) ;
			} catch (Exception) {
				throw;
			}
        }
    }
}

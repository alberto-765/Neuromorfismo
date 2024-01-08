using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Cryptography;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.Dal
{
    public class LineaTemporalDal {
        private readonly WebmedicinaContext _context;

        public LineaTemporalDal(WebmedicinaContext context) {
            _context = context;
        }

        // Obtener diccionario ordenado de las etapas, si no hay devolvemos uno vacio
        public ImmutableSortedDictionary<int, EtapaLTDto> GetEtapas() {
			try {
                return _context.EtapaLTModel.ToImmutableSortedDictionary(q => q.Id, q=> q.ToDto()) ;
			} catch (Exception) {
				throw;
			}
        }

        // Obtener la evolucion de un paciente
        public async Task<List<EvolucionLTDto>> GetEvolucion(int idPaciente) {
			try {
                return await (from q in _context.EvolucionLTModels where q.Etapa.Id == idPaciente select q.ToDto()).ToListAsync();
			} catch (Exception) {
				throw;
			}
        }
    }
}

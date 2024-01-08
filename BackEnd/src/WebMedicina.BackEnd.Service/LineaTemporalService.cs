using System.Collections.Immutable;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.Service
{
    public class LineaTemporalService : ILineaTemporalService {
        private readonly LineaTemporalDal _lineaTemporalDal;

        public LineaTemporalService(LineaTemporalDal lineaTemporalDal) {
            _lineaTemporalDal = lineaTemporalDal;
        }


        /// <summary>
        /// Obtener todas las etapas existentes
        /// </summary>
        /// <returns>ImmutableSortedDictionary de todas las etapas</returns>
        public ImmutableSortedDictionary<int, EtapaLTDto> GetEtapas() {
			try {
				return _lineaTemporalDal.GetEtapas();
			} catch (Exception) {
				throw;
			}       
		}

        public async Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucion(int idPaciente) {
            try {
                List<EvolucionLTDto> evoluciones = await _lineaTemporalDal.GetEvolucion(idPaciente);

                // convertimos la lista en una shortedlist
                SortedList<int, EvolucionLTDto> evolucionOrdenada = new();
                foreach (EvolucionLTDto evo in evoluciones) {
                    evolucionOrdenada.Add(evo.IdEtapa, evo);
                }

                return evolucionOrdenada;
            } catch (Exception) {
                throw;
            }
        }
    }
}

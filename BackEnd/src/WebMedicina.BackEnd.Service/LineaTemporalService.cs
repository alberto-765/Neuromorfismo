using System.Collections.Immutable;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.BackEnd.Service {
    public class LineaTemporalService : ILineaTemporalService {
        private readonly LineaTemporalDal _lineaTemporalDal;

        public LineaTemporalService(LineaTemporalDal lineaTemporalDal) {
            _lineaTemporalDal = lineaTemporalDal;
        }


        /// <summary>
        /// Obtener todas las etapas existentes
        /// </summary>
        /// <returns>ImmutableSortedDictionary de todas las etapas</returns>
        public ImmutableSortedDictionary<int, EtapasDto> GetEtapas() {
			try {
				return  _lineaTemporalDal.GetEtapas();
			} catch (Exception) {
				throw;
			}       
		}
    }
}

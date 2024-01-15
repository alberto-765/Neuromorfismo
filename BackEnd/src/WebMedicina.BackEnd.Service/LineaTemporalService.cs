using System.Collections.Immutable;
using System.Security.Claims;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
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

        /// <summary>
        /// Actualizar etapa evolucion paciente
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Lista evoluciones actualizada</returns>
        public async Task<SortedList<int, EvolucionLTDto>> ActualizarEvolucion(RequestActEvo request, ClaimsPrincipal User) {
            try {
                if(Comun.ObtenerIdUsuario(User, out int idMedico) && idMedico > 0) {

                    // Si se ha actualizado correctamente devolvemos de nuevo la lista
                    if (await _lineaTemporalDal.ActualizarEvolucion(request, idMedico)) {
                        return await ObtenerEvolucion(request.IdPaciente);
                    }
                }

                throw new Exception();
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Insertar etapa evolucion paciente
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Lista evoluciones actualizada</returns>
        public async Task<SortedList<int, EvolucionLTDto>> InsertarEvolucion(RequestActEvo request, ClaimsPrincipal User) {
            try {
                if (Comun.ObtenerIdUsuario(User, out int idMedico) && idMedico > 0) {

                    // Creamos evolucion nueva a insertar 
                    EvolucionLTModel evolucion = request.Evolucion.ToModel();
                    evolucion.IdMedicoUltModif = idMedico;
                    evolucion.IdPaciente = request.IdPaciente;

                    // Si se ha actualizado correctamente devolvemos de nuevo la lista
                    if (await _lineaTemporalDal.InsertarEvolucion(evolucion)) {
                        return await ObtenerEvolucion(request.IdPaciente);
                    }
                }

                throw new Exception();
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

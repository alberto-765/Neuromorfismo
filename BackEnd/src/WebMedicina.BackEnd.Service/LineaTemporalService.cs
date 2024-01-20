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
            return _lineaTemporalDal.GetEtapas();   
		}


        /// <summary>
        /// Identificar si existe la evolucion, si existe actualizarla y sino insertarla
        /// </summary>
        /// <param name="request"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<SortedList<int, EvolucionLTDto>> ActOInsertEvolucion(LLamadaEditarEvoDto evoEditada, ClaimsPrincipal User) {
            if (Comun.ObtenerIdUsuario(User, out int idMedico) && idMedico > 0) {
                // Obtenemos la evolucion y comprobamos si hay que insertala o actualizarla
                EvolucionLTModel? nuevaEvolucion = await _lineaTemporalDal.GetEvolucion(evoEditada.Evolucion.Id, evoEditada.IdPaciente);
                bool accionOk = false;


                // Si no existe la insertamos
                if(nuevaEvolucion is null) {

                    // Mapeamos la nueva evolucion
                    nuevaEvolucion = evoEditada.Evolucion.ToModel();
                    nuevaEvolucion.IdPaciente = evoEditada.IdPaciente;
                    nuevaEvolucion.IdEtapa = evoEditada.UltimaEtapaPaciente;
                    nuevaEvolucion.IdMedicoUltModif = idMedico;

                    accionOk = await _lineaTemporalDal.InsertarEvolucion(nuevaEvolucion);
                } else {

                    // Actualizamos campos de la nueva evolucion
                    nuevaEvolucion.IdMedicoUltModif = idMedico;
                    nuevaEvolucion.Fecha = DateTime.Today;
                    nuevaEvolucion.Confirmado = evoEditada.Evolucion.Confirmado;

                    accionOk = await _lineaTemporalDal.ActualizarEvolucion(nuevaEvolucion);
                }

                // Si se ha actualizado o insertado devolvemos nuevo listado de evoluciones del paciente
                if(accionOk) {
                    return await ObtenerEvolucion(evoEditada.IdPaciente);
                }
            }

            return new SortedList<int, EvolucionLTDto>();
        }

        /// <summary>
        /// Obtener todas las evoluciones de un paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>ShortedList evoluciones del paciente</returns>
        public async Task<SortedList<int, EvolucionLTDto>> ObtenerEvolucion(int idPaciente) {
            List<EvolucionLTDto> evoluciones = await _lineaTemporalDal.GetEvoluciones(idPaciente);

            // convertimos la lista en una shortedlist
            SortedList<int, EvolucionLTDto> evolucionesOrdenadas = new();
            foreach (EvolucionLTDto evo in evoluciones) {
                evolucionesOrdenadas.Add(evo.IdEtapa, evo);
            }

            return evolucionesOrdenadas;
        }
    }
}

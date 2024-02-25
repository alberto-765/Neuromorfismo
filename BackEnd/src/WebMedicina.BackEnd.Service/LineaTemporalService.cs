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
        public ImmutableSortedDictionary<short, EtapaLTDto> GetEtapas() {
            return _lineaTemporalDal.GetEtapas();   
		}


        /// <summary>
        /// Identificar si existe la evolucion, si existe actualizarla y sino insertarla
        /// </summary>
        /// <param name="evoEditada"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        public async Task<SortedList<short, EvolucionLTDto>> ActOInsertEvolucion(LLamadaEditarEvoDto evoEditada, ClaimsPrincipal User) {
            if (Comun.ObtenerIdUsuario(User, out int idMedico) && idMedico > 0) {
                // Obtenemos la evolucion y comprobamos si hay que insertala o actualizarla
                EvolucionLTModel? nuevaEvolucion = await _lineaTemporalDal.GetEvolucion(evoEditada.Evolucion.Id, evoEditada.IdPaciente);
                bool accionOk;

                // Si no existe la insertamos
                if(nuevaEvolucion is null) {

                    // Mapeamos la nueva evolucion
                    nuevaEvolucion = evoEditada.Evolucion.ToModel();
                    nuevaEvolucion.IdPaciente = evoEditada.IdPaciente;
                    nuevaEvolucion.IdEtapa = evoEditada.Evolucion.IdEtapa;
                    nuevaEvolucion.IdMedicoUltModif = idMedico;

                    accionOk = await _lineaTemporalDal.InsertarEvolucion(nuevaEvolucion);
                } else {

                    // Actualizamos campos de la nueva evolucion
                    nuevaEvolucion.IdMedicoUltModif = idMedico;
                    nuevaEvolucion.Fecha = DateTime.Now;
                    nuevaEvolucion.Confirmado = evoEditada.Evolucion.Confirmado;

                    accionOk = await _lineaTemporalDal.ActualizarEvolucion(nuevaEvolucion);
                }

                // Si se ha actualizado o insertado devolvemos nuevo listado de evoluciones del paciente
                if(accionOk) {
                    return await ObtenerEvoluciones(evoEditada.IdPaciente);
                }
            }

            return new SortedList<short, EvolucionLTDto>();
        }

        /// <summary>
        /// Obtener todas las evoluciones de un paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>ShortedList evoluciones del paciente</returns>
        public async Task<SortedList<short, EvolucionLTDto>> ObtenerEvoluciones(int idPaciente) {
            List<EvolucionLTDto> evoluciones = await _lineaTemporalDal.GetEvoluciones(idPaciente);

            // convertimos la lista en una shortedlist
            SortedList<short, EvolucionLTDto> evolucionesOrdenadas = new();
            foreach (EvolucionLTDto evo in evoluciones) {
                evolucionesOrdenadas.Add(evo.IdEtapa, evo);
            }

            return evolucionesOrdenadas;
        }
    }
}

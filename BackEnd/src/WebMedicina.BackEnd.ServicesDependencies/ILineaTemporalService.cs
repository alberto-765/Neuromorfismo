
using System.Collections.Immutable;
using System.Security.Claims;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.ServicesDependencies
{
    public interface ILineaTemporalService {

        /// <summary>
        /// Obtener todas las etapas existentes
        /// </summary>
        /// <returns>ImmutableSortedDictionary de todas las etapas</returns>
        ImmutableSortedDictionary<short, EtapaLTDto> GetEtapas();

        /// <summary>
        /// Identificar si existe la evolucion, si existe actualizarla y sino insertarla
        /// </summary>
        /// <param name="evoEditada"></param>
        /// <param name="User"></param>
        /// <returns></returns>
        Task<SortedList<short, EvolucionLTDto>> ObtenerEvoluciones(int idPaciente);

        /// <summary>
        /// Obtener todas las evoluciones de un paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>ShortedList evoluciones del paciente</returns>
        Task<SortedList<short, EvolucionLTDto>> ActOInsertEvolucion(LLamadaEditarEvoDto evoEditada, ClaimsPrincipal User);
    }
}

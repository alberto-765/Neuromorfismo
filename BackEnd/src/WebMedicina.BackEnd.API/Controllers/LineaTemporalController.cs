using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LineaTemporalController : ControllerBase {

        private ILineaTemporalService _lineaTemporalService;

        public LineaTemporalController(ILineaTemporalService lineaTemporalService) {
            _lineaTemporalService = lineaTemporalService;
        }

        [HttpGet("obtenertodasetapas")]
        public ImmutableSortedDictionary<short, EtapaLTDto> ObtenerEtapas() {
            try {
                return _lineaTemporalService.GetEtapas();
            } catch (Exception) {
                return ImmutableSortedDictionary<short, EtapaLTDto>.Empty;
            }
        }

        [HttpGet("obtenerevolucionpaciente/{idpaciente}")]
        public async Task<ActionResult<SortedList<short, EvolucionLTDto>>> ObtenerEvolucion(int idPaciente) {
            try {
                return Ok(await _lineaTemporalService.ObtenerEvoluciones(idPaciente));
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpPut("actoinsertevolucionpaciente")]
        public async Task<SortedList<short, EvolucionLTDto>> ActOInsertEvolucionPaciente([FromBody] LLamadaEditarEvoDto evoEditada) {
            try {
                if (!ModelState.IsValid) {
                    return new SortedList<short, EvolucionLTDto>();
                }
                return await _lineaTemporalService.ActOInsertEvolucion(evoEditada, User);
            } catch (Exception) {
                return new SortedList<short, EvolucionLTDto>();
            }
        }
    }   
}

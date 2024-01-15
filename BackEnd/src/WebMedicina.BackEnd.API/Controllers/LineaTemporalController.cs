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

        [HttpGet("ObtenerTodasEtapas")]
        public ImmutableSortedDictionary<int, EtapaLTDto> ObtenerEtapas() {
            try {
                return _lineaTemporalService.GetEtapas();
            } catch (Exception) {
                return ImmutableSortedDictionary<int, EtapaLTDto>.Empty;
            }
        }

        [HttpGet("ObtenerEvolucionPaciente/{idPaciente}")]
        public async Task<ActionResult<SortedList<int, EvolucionLTDto>>> ObtenerEvolucion(int idPaciente) {
            try {
                return Ok(await _lineaTemporalService.ObtenerEvolucion(idPaciente));
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet("LineaTemporal/ActualizarEvolucionPaciente")]
        public async Task<ActionResult<SortedList<int, EvolucionLTDto>>> ActualizarEvolucionPaciente([FromBody] RequestActEvo request) {
            try {
                return Ok(await _lineaTemporalService.ActualizarEvolucion(request, User));
            } catch (Exception) {
                return BadRequest();
            }
        }

        [HttpGet("LineaTemporal/InsertarEvolucionPaciente")]
        public async Task<ActionResult<SortedList<int, EvolucionLTDto>>> InsertarEvolucionPaciente([FromBody] RequestActEvo request) {
            try {
                return Ok(await _lineaTemporalService.InsertarEvolucion(request, User));
            } catch (Exception) {
                return BadRequest();
            }
        }
    }   
}

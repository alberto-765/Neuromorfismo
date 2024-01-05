using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Immutable;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LineaTemporalController : ControllerBase {

        private ILineaTemporalService _lineaTemporalService;

        public LineaTemporalController(ILineaTemporalService lineaTemporalService) {
            _lineaTemporalService = lineaTemporalService;
        }

        [HttpGet("obtenerEtapas")]
        public ImmutableSortedDictionary<int, EtapasDto> ObtenerEtapas() {
            try {
                return _lineaTemporalService.GetEtapas();
            } catch (Exception) {
                return ImmutableSortedDictionary<int, EtapasDto>.Empty;
            }
        }
    }
}

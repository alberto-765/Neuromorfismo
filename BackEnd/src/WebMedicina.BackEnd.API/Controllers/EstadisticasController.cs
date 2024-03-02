using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class EstadisticasController : ControllerBase {

        /// <summary>
        /// Get te todos los datos para las gráficas del inicio
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<EstadisticasDto> GetEstadisticas() {
            // Obtenemos gráfica de total de pacientes

        }
    }
}

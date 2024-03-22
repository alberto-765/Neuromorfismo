using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.Shared.Dto.Estadisticas;

namespace Neuromorfismo.BackEnd.API.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstadisticasController : ControllerBase {
        private readonly IEstadisticasService _estadisticasService;

        public EstadisticasController(IEstadisticasService estadisticasService) {
            _estadisticasService = estadisticasService;
        }

        /// <summary>
        /// Get te todos los datos para las gráficas del inicio
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public EstadisticasDto GetEstadisticas() {
            return _estadisticasService.ObtenerDatosGraficas();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.BackEnd.API.Controllers {
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
            var totales = _estadisticasService.ObtenerTotales();

            EstadisticasDto estadisticas = new() {

                // Obtenemos datos gráfica de total de pacientes y medicos
                TotalPacientes = totales.TotalPacientes,
                TotalMedicos = totales.TotalMedicos,

                // Obtenemos datos gráfica resumen evoluciones etapa
                TotalEtapas = _estadisticasService.ObtenerResumenEtapas()
            };

            // Devolvemos objeto con las estadisticas
            return estadisticas;
        }
    }
}

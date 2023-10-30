using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("api/administracion")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class AdministracionController : ControllerBase {
        private IAdminsService _adminsService;

        public AdministracionController(IAdminsService adminsService) { 
            _adminsService = adminsService;
        }



        [HttpGet("getEpilepsias")]
        public ActionResult<List<EpilepsiasDto>> GetEpilepsias() {
            try {
                var epilepsias = _adminsService.ObtenerEpilepsias();

                if(epilepsias != null && epilepsias.Any()) {
                    return Ok(epilepsias);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor");
            }

        }
    }
}

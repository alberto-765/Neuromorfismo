using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("api/pacientes")]
    [ApiController]
    [Authorize]
    public class PacientesController : Controller {
        private IPacientesService _pacientesService;

        public PacientesController(IPacientesService pacientesService) {
            _pacientesService = pacientesService;
        }

        [HttpGet("getMedicosPacientes")]
        public ActionResult<IEnumerable<string>> GetAllMed() {
            try {

                List<MedicosPacientesDto> listMedPac = new();

                // Devolvemos la lista con los id de los medicos
                return Ok(listMedPac);
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }


        //  Farmacos
        [HttpGet("getFarmacos")]
        public ActionResult<List<FarmacosDto>> GetFarmacos() {
            try {
                List<FarmacosDto> farmacos = _pacientesService.ObtenerFarmacos();

                if (farmacos != null && farmacos.Any()) {
                    return Ok(farmacos);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }


        //  Mutaciones
        [HttpGet("getMutaciones")]
        public ActionResult<List<MutacionesDto>> GetMutaciones() {
            try {
                List<MutacionesDto> mutaciones = _pacientesService.ObtenerMutaciones();

                if (mutaciones != null && mutaciones.Any()) {
                    return Ok(mutaciones);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        //  Epilepsias
        [HttpGet("getEpilepsias")]
        public ActionResult<List<EpilepsiasDto>> GetEpilepsias() {
            try {
                List<EpilepsiasDto> epilepsias = _pacientesService.ObtenerEpilepsias();

                if (epilepsias != null && epilepsias.Any()) {
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

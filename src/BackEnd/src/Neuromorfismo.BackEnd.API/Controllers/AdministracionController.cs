using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.Shared.Dto.Tipos;

namespace Neuromorfismo.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class AdministracionController : ControllerBase {
        private IAdminsService _adminsService;

        public AdministracionController(IAdminsService adminsService) { 
            _adminsService = adminsService;
        }


        //  Epilepsias
        [Authorize]
        [HttpGet("getepilepsias")]
        public ActionResult<List<EpilepsiasDto>> GetEpilepsias() {
            try {
                List<EpilepsiasDto> epilepsias = _adminsService.ObtenerEpilepsias();

                if(epilepsias != null && epilepsias.Any()) {
                    return Ok(epilepsias);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }

        }

        //  Epilepsias
        [HttpPost("crearepilepsia")]
        public async Task<ActionResult<bool>> CrearEpilepsia([FromBody] string nombre) {
            try {
                return Ok(await _adminsService.CrearNuevaEpilepsia(nombre));
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Epilepsias
        [HttpDelete("eliminarepilepsia/{idepilepsia}")]
        public async Task<ActionResult<bool>> DeleteEpilepsia(int idEpilepsia) {
            try {
                if(idEpilepsia > 0) {
                    return Ok(await _adminsService.EliminarEpilepsia(idEpilepsia));
                }

                return BadRequest("Error al eliminar la epilepsia");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Epilepsias
        [HttpPut("updateepilepsia")]
        public async Task<ActionResult<bool>> UpdateEpilepsia([FromBody] EpilepsiasDto epilepsia) {
            try {
                (bool validacionEntry, bool filasModif) = await _adminsService.ActualizarEpilepsia(epilepsia);

                // Validamos si la epilepsia ha sido modificada por el cliente
                if (validacionEntry) {
                    return Ok(filasModif);
                } else {
                    return NoContent();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Mutaciones
        [Authorize]
        [HttpGet("getmutaciones")]
        public ActionResult<List<MutacionesDto>> GetMutaciones() {
            try {
                List<MutacionesDto> mutaciones = _adminsService.ObtenerMutaciones();

                if (mutaciones != null && mutaciones.Any()) {
                    return Ok(mutaciones);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }

        }

        //  Mutaciones
        [HttpPost("crearmutacion")]
        public async Task<ActionResult<bool>> CrearMutacion([FromBody] string nombre) {
            try {
                    return Ok(await _adminsService.CrearNuevaMutacion(nombre));
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Mutaciones
        [HttpDelete("eliminarmutacion/{idmutacion}")]
        public async Task<ActionResult<bool>> DeleteMutacion(int idMutacion) {
            try {
                if (idMutacion > 0) {
                    return Ok(await _adminsService.EliminarMutacion(idMutacion));
                }

                return BadRequest("Error al eliminar la epilepsia");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Mutaciones
        [HttpPut("updatemutacion")]
        public async Task<ActionResult<bool>> UpdateMutacion([FromBody] MutacionesDto mutacion) {
            try {
                    (bool validacionEntry, bool filasModif) = await _adminsService.ActualizarMutacion(mutacion);

                    // Validamos si la epilepsia ha sido modificada por el cliente
                    if (validacionEntry) {
                        return Ok(filasModif);
                    } else {
                        return NoContent();
                    }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Farmacos
        [Authorize]
        [HttpGet("getfarmacos")]
        public ActionResult<List<FarmacosDto>> GetFarmacos() {
            try {
                List<FarmacosDto> farmacos = _adminsService.ObtenerFarmacos();

                if (farmacos != null && farmacos.Any()) {
                    return Ok(farmacos);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Farmacos
        [HttpPost("crearfarmaco")]
        public async Task<ActionResult<bool>> CrearFarmaco([FromBody] string nombre) {
            try {
                    return Ok(await _adminsService.CrearNuevoFarmaco(nombre));
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Farmacos
        [HttpDelete("eliminarfarmaco/{idfarmaco}")]
        public async Task<ActionResult<bool>> DeleteFarmaco(int idFarmaco) {
            try {
                if (idFarmaco > 0) {
                    return Ok(await _adminsService.EliminarFarmaco(idFarmaco));
                }

                return BadRequest("Error al eliminar la fármaco");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Farmacos 
        [HttpPut("updatefarmaco")]
        public async Task<ActionResult<bool>> UpdateFarmaco([FromBody] FarmacosDto farmaco) {
            try {
                    (bool validacionEntry, bool filasModif) = await _adminsService.ActualizarFarmaco(farmaco);

                    // Validamos si la epilepsia ha sido modificada por el cliente
                    if (validacionEntry) {
                        return Ok(filasModif);
                    } else {
                        return NoContent();
                    }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }
    }
}

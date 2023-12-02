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


        //  Epilepsias
        [HttpGet("getEpilepsias")]
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
        [HttpPost("crearEpilepsia")]
        public async Task<ActionResult<bool>> CrearEpilepsia([FromBody] string nombre) {
            try {
                if(ModelState.IsValid) {
                    return Ok(await _adminsService.CrearNuevaEpilepsia(nombre));
                }

                return BadRequest("Nombre inválido");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Epilepsias
        [HttpDelete("eliminarEpilepsia/{idEpilepsia}")]
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
        [HttpPut("updateEpilepsia")]
        public async Task<ActionResult<bool>> UpdateEpilepsia([FromBody] EpilepsiasDto epilepsia) {
            try {
                if (ModelState.IsValid) {
                    var resultado = await _adminsService.ActualizarEpilepsia(epilepsia);

                    // Validamos si la epilepsia ha sido modificada por el cliente
                    if (resultado.validacionEntry) {
                        return Ok(resultado.filasModif);
                    } else {
                        return NoContent();
                    }
                }

                return BadRequest("Epilepsia no válida");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Mutaciones
        [HttpGet("getMutaciones")]
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
        [HttpPost("crearMutacion")]
        public async Task<ActionResult<bool>> CrearMutacion([FromBody] string nombre) {
            try {
                if (ModelState.IsValid) {
                    return Ok(await _adminsService.CrearNuevaMutacion(nombre));
                }

                return BadRequest("Nombre inválido");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Mutaciones
        [HttpDelete("eliminarMutacion/{idMutacion}")]
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
        [HttpPut("updateMutacion")]
        public async Task<ActionResult<bool>> UpdateMutacion([FromBody] MutacionesDto mutacion) {
            try {
                if (ModelState.IsValid) {
                    var resultado = await _adminsService.ActualizarMutacion(mutacion);

                    // Validamos si la epilepsia ha sido modificada por el cliente
                    if (resultado.validacionEntry) {
                        return Ok(resultado.filasModif);
                    } else {
                        return NoContent();
                    }
                }

                return BadRequest("Epilepsia no válida");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Farmacos
        [HttpGet("getFarmacos")]
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
        [HttpPost("crearFarmaco")]
        public async Task<ActionResult<bool>> CrearFarmaco([FromBody] string nombre) {
            try {
                if (ModelState.IsValid) {
                    return Ok(await _adminsService.CrearNuevoFarmaco(nombre));
                }

                return BadRequest("Nombre inválido");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Farmacos
        [HttpDelete("eliminarFarmaco/{idFarmaco}")]
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
        [HttpPut("updateFarmaco")]
        public async Task<ActionResult<bool>> UpdateFarmaco([FromBody] FarmacosDto farmaco) {
            try {
                if (ModelState.IsValid) {
                    var resultado = await _adminsService.ActualizarFarmaco(farmaco);

                    // Validamos si la epilepsia ha sido modificada por el cliente
                    if (resultado.validacionEntry) {
                        return Ok(resultado.filasModif);
                    } else {
                        return NoContent();
                    }
                }

                return BadRequest("Fármaco no válido");
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

    }
}

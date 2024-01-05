using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.API.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    [Authorize]
    public class PacientesController : Controller {
        private IPacientesService _pacientesService;

        public PacientesController(IPacientesService pacientesService) {
            _pacientesService = pacientesService;
        }

        // Validar si el numero de historia es valido par el paciente
        [HttpGet("validarNumHistoria/{numHistoria}")]
        public ActionResult<IEnumerable<string>> ValidarNumHistoria(string numHistoria) {
            try {

                // Devolvemos la lista con los id de los medicos
                return Ok(_pacientesService.ValidarNumHistoria(numHistoria));
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        // Obtener todos los medicos que tienen pacientes asignados
        [Authorize(Roles = "superAdmin, admin")]
        [HttpGet("getMedicosPacientes")]
        public async Task<IEnumerable<UserInfoDto>> GetAllMed() {
            try {
                return await _pacientesService.GetAllMed();
            } catch (Exception) {
                return Enumerable.Empty<UserInfoDto>();
            }
        }

        // Crear nuevo paciente
        [HttpPost("crearPaciente")]
        public async Task<ActionResult<int>> CrearPaciente([FromBody] CrearPacienteDto nuevoPaciente) {
            try {
                if(ModelState.IsValid) {

                    // Validamos que el numero de historia sea valido
                    if (_pacientesService.ValidarNumHistoria(nuevoPaciente.NumHistoria) == false) {

                        // Obtenemos el id del medico y creamos paciente
                        if (int.TryParse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out int idMedico) && idMedico > 0) {
                            return Ok(await _pacientesService.CrearPaciente(nuevoPaciente, idMedico));
                        } else {
                            return Ok(false);
                        }
                    } else {
                        return BadRequest($"El Número de Historia \"{nuevoPaciente.NumHistoria}\" ya está en uso.");
                    }
                } else {
                    return BadRequest("Los datos del nuevo cliente no son correctos."); 
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        // Editar paciente
        [HttpPut("editarPaciente")]
        public async Task<ActionResult<bool>> EditarPaciente([FromBody] CrearPacienteDto nuevoPaciente) {
            try {
                if (ModelState.IsValid) {

                    // Validar que el usuario tiene permisos
                    if (await _pacientesService.ValidarPermisosEdicYElim(User, nuevoPaciente.IdPaciente) && Comun.ObtenerIdUsuario(User, out int idMedico) && idMedico > 0) {

                        // Obtenemos el id del medico y editamos el  paciente
                        return Ok(await _pacientesService.EditarPaciente(nuevoPaciente, idMedico));
                    }  else {
                        return BadRequest($"No posee permisos para editar el paciente {nuevoPaciente.NumHistoria}.");
                    }
                } else {
                    return BadRequest("Los datos para cliente no son correctos.");
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }



        // Eliminar paciente
        [HttpDelete("eliminarPaciente/{idPaciente}")]
        public async Task<ActionResult<bool>> EliminarPaciente(int idPaciente) {
            try {
                // Validar que el usuario tiene permisos
                if (await _pacientesService.ValidarPermisosEdicYElim(User, idPaciente)) {

                    // Obtenemos el id del medico y creamos paciente
                    return Ok(await _pacientesService.EliminarPaciente(idPaciente));
                } else {
                    return BadRequest($"No posee permisos para eliminar el paciente.");
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        // Obtener pacientes y devolver listado
        [HttpGet("obtenerPacientes")]
        public ActionResult ObtenerPacientes() {
            try {
                List<CrearPacienteDto> listaPacientes = _pacientesService.ObtenerPacientes(User);
                return Ok(listaPacientes);

            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Farmacos
        [HttpGet("getFarmacos")]
        public ActionResult<List<FarmacosDto>> GetFarmacos() {
            try {
                List<FarmacosDto> farmacos = _pacientesService.ObtenerFarmacos();

                return Ok(farmacos);
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }


        //  Mutaciones
        [HttpGet("getMutaciones")]
        public ActionResult<List<MutacionesDto>> GetMutaciones() {
            try {
                List<MutacionesDto> mutaciones = _pacientesService.ObtenerMutaciones();

                return Ok(mutaciones); 
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        //  Epilepsias
        [HttpGet("getEpilepsias")]
        public ActionResult<List<EpilepsiasDto>> GetEpilepsias() {
            try {
                List<EpilepsiasDto> epilepsias = _pacientesService.ObtenerEpilepsias();

                return Ok(epilepsias);
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }

        }

        [HttpGet("obtenerPaciente/{idPaciente}")]
        public async Task<ActionResult<CrearPacienteDto?>> ObtenerPaciente(int idPaciente) {
            try {
                return Ok(await _pacientesService.GetUnPaciente(idPaciente));
            } catch (Exception) {
                return BadRequest();
            }
        }
    }
}

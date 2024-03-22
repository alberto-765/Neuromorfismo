using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.ServicesDependencies;
using Neuromorfismo.Shared.Dto.Pacientes;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class GestionUsersController : Controller {
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;
        private readonly NeuromorfismoContext _context;


        public GestionUsersController(IAdminsService adminsService, IIdentityService identity, NeuromorfismoContext context) {
            _adminService = adminsService;
            _context = context;
            _identityService = identity;
        }

        [HttpPost("obtenerusuariosfiltrados")]
        public async Task<IActionResult> ObtenerUsuariosFiltrados([FromBody] FiltradoTablaDefaultDto camposFiltrado) {
            try {
                List<UserUploadDto> listaMedicos = await _adminService.ObtenerFiltradoUsuarios(camposFiltrado, HttpContext.User);
                if (listaMedicos.Any()) {
                    return Ok(listaMedicos);
                }

                return NoContent();
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        [HttpPut("actualizarusuario")]
        public async Task<IActionResult> ActualizarUsuario([FromBody] LLamadaUploadUserDto usuarioEditado) {
            using var transactionContext = _context.Database.BeginTransaction();
            try {
                    // Validamos los campos del nuevo usuario
                    List<ValidationResult> errores = new();
                    Validator.TryValidateObject(usuarioEditado.usuario, new ValidationContext(usuarioEditado.usuario), errores, true);

                    if (!errores.Any()) {
                        // Si el usuario se ha actualizado correctamente, cambiamos el rol si es necesario
                        if (await _adminService.ActualizarMedico(usuarioEditado.usuario)) {

                            if (usuarioEditado.rolModificado) {

                                // Actualizamos el rol del usuario
                                if (await _identityService.ActualizarRol(usuarioEditado.usuario.UserLogin, usuarioEditado.usuario.Rol)) {
                                    await transactionContext.CommitAsync();
                                    return Ok("Médico editado correctamente");
                                }
                            } else {
                                await transactionContext.CommitAsync();
                                return Ok("Médico editado correctamente");
                            }
                        }

                        await transactionContext.RollbackAsync();
                        return BadRequest("Error al editar el médico");
                    }

                return BadRequest("Alguno de los campos del usuario no es válido");
            } catch (Exception) {
                await transactionContext.RollbackAsync();
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }
    }

    }

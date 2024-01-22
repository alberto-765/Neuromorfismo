using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.API.Controllers
{
    [Route("/api/cuentas")]
    [ApiController]
    [Authorize(Roles = "superAdmin, admin")]
    public class CuentasController : ControllerBase {
        private readonly IUserAccountService _userAccountService;
        private readonly IIdentityService _identityService;

        // Contructor con inyeccion de dependencias
        public CuentasController(IUserAccountService userAccountService, IIdentityService identityService) {
            _userAccountService = userAccountService;
            _identityService = identityService;
        }

        [HttpPost("crear")]
        public async Task<ActionResult> CreateUser([FromBody] UserRegistroDto model) {
            try {
                EstadoCrearUsuario estadoCrearUsu = EstadoCrearUsuario.ModeloKO;

                if (ModelState.IsValid && model != null) {
                    estadoCrearUsu = await _userAccountService.CrearUsuarioYMedico(model);
                }


                switch (estadoCrearUsu) {
                    case EstadoCrearUsuario.MedicoKO:
                    return BadRequest("No ha podido crearse el nuevo médico, inténtelo más tarde");
                    case EstadoCrearUsuario.IdentityUserKO:
                    return BadRequest($"Ya existe un usuario con el username: {model?.UserLogin}, o el username no es válido");
                    case EstadoCrearUsuario.UserYMedicoOK:
                    // Devolvemos que la respuesta ha sido correcta
                    return Ok($"Nuevo {model?.Rol} creado correctamente");
                    default:
                    return BadRequest("Datos del nuevo usuario incorrectos");
                }

            } catch (Exception) {
                return BadRequest("Datos del nuevo usuario incorrectos");
            }
        }


        [AllowAnonymous]
        [HttpPost("autenticarusuario")]
        public async Task<ActionResult<Tokens>> AutenticarUsuario([FromBody] UserLoginDto userLogin) {
            try {
                // Comprobamos si la contraseña es válida
                if (ModelState.IsValid && await _identityService.ComprobarContraseña(userLogin)) {

                    Tokens? tokens = _userAccountService.ObtenerTokenLogin(userLogin);

                    // Devolvemos token y el refreshToken si han sido generados correctamente
                    if (tokens is not null) {
                        return Ok(tokens);
                    }
                }

                return Unauthorized("Usuario o contraseña no válidos");
            } catch (Exception) {
                return Unauthorized("Usuario o contraseña no válidos");
            }
        }

        // Generamos y comprobamos si el userName está disponible 
        [HttpPost("generarusername")]
        public async Task<ActionResult> GenerarUserName([FromBody] string[] nomYApell) {
            try {
                var respuesta = await _identityService.GenerarUserName(nomYApell);
                if (respuesta.userNameInvalido == false) {
                    return Ok(respuesta.userNameGenerado);
                } else {
                    return BadRequest();
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor. Inténtelo de nuevo o conteacte con un administrador.");
            }
        }

        // Cerrar sesion de un usuario
        [HttpPost("cerrarsesion")]
        public void CerrarSesion([FromBody] Tokens tokens) {
            if (ModelState.IsValid) {
                _userAccountService.CerrarSesion(tokens, User.ToUserInfoDto());
            }
        }
    }
}

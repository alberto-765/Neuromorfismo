using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("/api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAdminsService _adminService;
        private readonly IIdentityService _identityService;

        // Contructor con inyeccion de dependencias
        public CuentasController(IConfiguration configuration, IMapper mapper, IAdminsService adminService, IIdentityService identityService) {
            _configuration = configuration;
            _mapper = mapper;
            _adminService = adminService;
            _identityService = identityService;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserRegistroDto model) {
            if (ModelState.IsValid && model != null) {
                var user = new IdentityUser {
                    UserName = model.NumHistoria
                };

                // Creamos user en identity
                if (await _identityService.CrearUser(user, model)) {


                    UserInfoDto userInfo = _mapper.Map<UserInfoDto>(model);
                    // Insertamos el nuevo medico a la tabla y generamos token si todo esta OK
                    if (await _adminService.CrearMedico(model, user.Id)) {
                        return BuildToken(userInfo);
                    } 
                } 
                return BadRequest("Ha surgido un error al crear el nuevo medico");
            } else {
                return BadRequest("Alguno de los campos no es valido");
            }

       
        }
        
            

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLoginDto userLogin) {
            try {
                if(ModelState.IsValid) {
                    if (await _identityService.ComprobarContraseña(userLogin)) {

                        // Mapeamos el modelo de medico denro del dto 
                        UserInfoDto userInfo = _mapper.Map<UserInfoDto>(_identityService.ObtenerUsuario(userLogin.UserName));
                        return BuildToken(userInfo);
                    } else {
                        ModelState.AddModelError(string.Empty, "Credenciales incorrectas");
                        return BadRequest(ModelState);
                    }
                } else {
                    return BadRequest(ModelState);
                }
            } catch (Exception) {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost("CrearRol")]
        public async Task<IActionResult> CrearRol([FromBody] string nombreRol) {

            //var roleExists = await _roleManager.RoleExistsAsync(nombreRol);
            //if (!roleExists) {
            //    // Añadir el nuevo rol
            //    var identityRole = new IdentityRole { Name = nombreRol };
            //    //var result =  await _roleManager.CreateAsync(identityRole);
            //     //if (result.Succeeded) {
            //        return Ok("Rol creado exitosamente");
            //    //} 

            //}
            return BadRequest("Error al crear el rol");

        }




        private UserToken BuildToken(UserInfoDto userInfo) {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.NumHistoria),
                new Claim(ClaimTypes.Name, userInfo.Nombre),
                new Claim(ClaimTypes.Surname, userInfo.Apellidos),
                new Claim(ClaimTypes.DateOfBirth, userInfo.FechaNac.ToString("dd/MM/yyyy")),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tiempo de expiración del token. En nuestro caso lo hacemos de una hora.
            var expiration = DateTime.UtcNow.AddDays(7);

            JwtSecurityToken token = new JwtSecurityToken(
               issuer: null,
               audience: null,
               claims: claims,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken() {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    
    }
}

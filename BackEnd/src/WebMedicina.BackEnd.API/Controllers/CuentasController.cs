using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Plugins;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.API.Controllers {
    [Route("/api/cuentas")]
    [ApiController]
    public class CuentasController : ControllerBase {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public CuentasController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IConfiguration configuration, IMapper mapper) {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("Crear")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] UserRegistro model) {
            var user = new IdentityUser { UserName = model.NumHistoria};
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                UserInfo userInfo = _mapper.Map<UserInfo>(model);
                return BuildToken(userInfo);
            } else {
                return BadRequest("Username or password invalid");
            }

        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserLogin userLogin) {
            var result = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, isPersistent: false, lockoutOnFailure: false);
            if (result.Succeeded) {
                UserInfo userInfo = _mapper.Map<UserInfo>(userLogin);
                return BuildToken(userInfo);
            } else {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return BadRequest(ModelState);
            }
        }

        private UserToken BuildToken(UserInfo userInfo) {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, userInfo.NumHistoria),
                new Claim(ClaimTypes.Name, userInfo.Nombre),
                new Claim(ClaimTypes.Surname, userInfo.Apellidos),
                new Claim(ClaimTypes.DateOfBirth, userInfo.FechaNacimiento.ToString("dd/MM/yyyy")),
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

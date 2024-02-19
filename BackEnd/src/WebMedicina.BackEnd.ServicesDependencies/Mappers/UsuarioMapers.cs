using IdentityModel;
using System.Data;
using System.Security.Claims;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies.Mappers {
    public static class UsuarioMap {

        // ClaimsPrincipal to UserInfoDto
        public static UserInfoDto ToUserInfoDto(this ClaimsPrincipal user) =>
            new() {
                IdMedico = int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out int idMedico) ? idMedico : throw new NoNullAllowedException(),
                UserLogin = user.FindFirstValue("UserLogin") ?? string.Empty,
                Nombre = user.FindFirstValue(ClaimTypes.Name),
                Apellidos = user.FindFirstValue(ClaimTypes.Surname),
                Rol = user.FindFirstValue(ClaimTypes.Role)
            };
    }
}

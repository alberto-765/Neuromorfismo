using System.Security.Claims;
using WebMedicina.Shared.Dto.Usuarios;
namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IObtenerInfoUser {
        UserInfoDto ConvervirToken(ClaimsPrincipal User);
    }
}

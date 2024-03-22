using System.Security.Claims;
using Neuromorfismo.Shared.Dto.Pacientes;
using Neuromorfismo.Shared.Dto.Tipos;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.ServicesDependencies {
    public static class Comun  {
        public static bool ObtenerIdUsuario(ClaimsPrincipal? user, out int idMedico) {
            try {
                if (int.TryParse(user?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value, out idMedico)) {
                    return true;
                }
                return false;
            } catch (Exception) {
                throw;
            }
        }
    }
}

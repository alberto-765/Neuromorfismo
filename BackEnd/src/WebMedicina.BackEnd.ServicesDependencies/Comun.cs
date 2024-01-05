using System.Security.Claims;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.ServicesDependencies {
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

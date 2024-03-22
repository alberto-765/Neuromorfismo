
using Neuromorfismo.Shared.Dto.UserAccount;

namespace Neuromorfismo.FrontEnd.ServiceDependencies {
    public interface IPerfilService {

        public Task CerrarSesion();
        public Task<CodigosErrorChangePass[]> CambiarContrasena(ChangePasswordDto changePass);
    }
}


using WebMedicina.Shared.Dto.UserAccount;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IPerfilService {

        public Task CerrarSesion();
        public Task<bool> CambiarContrasena(ChangePasswordDto changePass);
    }
}

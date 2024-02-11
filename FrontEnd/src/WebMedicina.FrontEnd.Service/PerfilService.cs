using Microsoft.JSInterop;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.UserAccount;

namespace WebMedicina.FrontEnd.Service {
    public class PerfilService : IPerfilService {
        private readonly IRedirigirManager redirigirManager;
        private JWTAuthenticationProvider _jwtAuthenticationProvider;


        public PerfilService(JWTAuthenticationProvider jwtAuthenticationProvider, IRedirigirManager redirigirManager) { 
            _jwtAuthenticationProvider = jwtAuthenticationProvider;
            this.redirigirManager = redirigirManager;
		}

        /// <summary>
        /// Cambiar contraseña
        /// </summary>
        /// <param name="changePass"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<bool> CambiarContrasena(ChangePasswordDto changePass) {
            //bool contraseña = false;

            //// Realizamos la llamada 
            //HttpResponseMessage resouesta
            return await Task.FromResult(true);
        }

        /// <summary>
        /// Cerrar sesion 
        /// </summary>
        /// <returns></returns>
        public async Task CerrarSesion() {
            // Cerramos sesion
            await _jwtAuthenticationProvider.Logout();

            // Redirigimos al login
            await redirigirManager.RedirigirLogin();
        }
    }
}

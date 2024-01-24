using Microsoft.JSInterop;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class PerfilService : IPerfilService{
        private readonly IRedirigirManager redirigirManager;
        private JWTAuthenticationProvider _jwtAuthenticationProvider;


        public PerfilService(JWTAuthenticationProvider jwtAuthenticationProvider, IRedirigirManager redirigirManager) { 
            _jwtAuthenticationProvider = jwtAuthenticationProvider;
            this.redirigirManager = redirigirManager;
		}

        // Cerrar sesion 
        public async Task CerrarSesion() {
            // Cerramos sesion
            await _jwtAuthenticationProvider.Logout();

            // Redirigimos al login
            await redirigirManager.RedirigirLogin();
        }
    }
}

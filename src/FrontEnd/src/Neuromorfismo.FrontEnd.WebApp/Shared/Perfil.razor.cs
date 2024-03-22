using Microsoft.AspNetCore.Components;
using Neuromorfismo.FrontEnd.ServiceDependencies;

namespace Neuromorfismo.FrontEnd.WebApp.Shared {
    public partial class Perfil {
        [Inject] private IPerfilService _perfilService { get; set; } = null!;

        // Cerrar Sesion 
        private async Task CerrarSesion() {
            try {
                await _perfilService.CerrarSesion();
            } catch (Exception) {
                throw;
            }
        }
    }
}

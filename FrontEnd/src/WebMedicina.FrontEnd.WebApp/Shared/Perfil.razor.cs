using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.WebApp.Shared {
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

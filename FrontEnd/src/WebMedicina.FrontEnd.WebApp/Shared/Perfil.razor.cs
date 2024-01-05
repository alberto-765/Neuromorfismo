using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.WebApp.Shared {
    public partial class Perfil {
        [Inject] IPerfilService perfilService { get; set; } = null!;
        // Cerrar Sesion 
        private async Task CerrarSesion() {
            try {
                await perfilService.CerrarSesion();
            } catch (Exception) {
                throw;
            }
        }
    }
}

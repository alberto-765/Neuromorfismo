using Microsoft.AspNetCore.Components;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.WebApp.Shared {
    public partial class Perfil {
        [Inject] IPerfilService perfilService { get; set; }
        [Inject] ExcepcionPersonalizada excepcionPersonalizada { get; set; }
        // Cerrar Sesion 
        private async Task CerrarSesion() {
            try {
                await perfilService.CerrarSesion();
            } catch (Exception ex) {
                excepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }
    }
}

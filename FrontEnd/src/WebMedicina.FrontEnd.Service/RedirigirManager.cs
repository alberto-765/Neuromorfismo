using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebMedicina.FrontEnd.ServiceDependencies;


namespace WebMedicina.FrontEnd.Service {
    public class RedirigirManager : IRedirigirManager {
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime js;
        private const string enlaceSeguimientoKey  = "SeguimientoPagina"; // clave del enlace de seguimiento sessionStorage
        public RedirigirManager(NavigationManager navigationManager, IJSRuntime js) {
            this.navigationManager = navigationManager;
            this.js = js;
        }

        // Obtenemos la url de segumiento y redirigimos 
        public async Task RedirigirPagAnt() { 
            string segEnl = await js.GetFromSessionStorage(enlaceSeguimientoKey);
            await RedirigirDefault(segEnl);
        }

        public async Task RedirigirDefault (string enlace = "/") {
            string urlActual = navigationManager.Uri;
            string baseUri = navigationManager.BaseUri;

            // Quitamos de la url actual la base url
            string paginaActual = urlActual.Replace(baseUri, "");

            // Comprobamos la nueva ruta a la que se va a redirigir
            if (string.IsNullOrWhiteSpace(enlace)) {
                enlace = "/";
            }

            // Comprobamos la ruta actual
            if (string.IsNullOrWhiteSpace(paginaActual)) {
                paginaActual = "/";
            }


            // Comprobamos que no se está ya en la url a la que se quiere redirigir
            if (!paginaActual.Equals(enlace, StringComparison.InvariantCultureIgnoreCase)) {
                await js.SetInSessionStorage(enlaceSeguimientoKey, paginaActual);
                navigationManager.NavigateTo(enlace); 
            }
        }
    }
}

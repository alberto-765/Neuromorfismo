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

        // Redirigimos al login si no se esta ya en el
        public async Task RedirigirLogin() { 
            string urlActual = navigationManager.Uri;
            string baseUri = navigationManager.BaseUri;
            if (urlActual.Replace(baseUri, "") != "login") {
                await RedirigirDefault("login");
            } 
        }

        // Obtenemos la url de segumiento y redirigimos a esta misma
        public async Task RedirigirPagAnt() { 
            string segEnl = await js.GetFromSessionStorage(enlaceSeguimientoKey);

            if (!string.IsNullOrWhiteSpace(segEnl)) {
                await RedirigirDefault(segEnl);
            } else {
                await RedirigirDefault();
            } 
        }

        public async Task RedirigirDefault (string enlace = "/") {  
            // Acutalizamos el enlace de seguimiento
            await ActualizarSeguimientoEnlace();
            navigationManager.NavigateTo(enlace); 
        }

        // Actualizar variable de url seguimiento de session
        public async Task ActualizarSeguimientoEnlace() {
            string urlActual = navigationManager.Uri;
            string baseUri = navigationManager.BaseUri;

            // Quitamos de la url actual la base url
            string paginaActual = urlActual.Replace(baseUri, "");

            // Guardamos en sessino la nueva url de seguimiento
            await js.SetInSessionStorage(enlaceSeguimientoKey, paginaActual);
        }
    }
}

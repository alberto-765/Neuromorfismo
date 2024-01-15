using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using WebMedicina.FrontEnd.ServiceDependencies;


namespace WebMedicina.FrontEnd.Service {
    public class RedirigirManager : IRedirigirManager {
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime js;
        private const string enlaceSeguimiento  = "segEnl";
        // replaceHistoryEntry
        public RedirigirManager(NavigationManager navigationManager, IJSRuntime js) {
            this.navigationManager = navigationManager;
            this.js = js;
        }

        public async Task RedirigirLogin() { 
            string urlActual = navigationManager.Uri;
            string baseUri = navigationManager.BaseUri;
            if (urlActual.Replace(baseUri, "") != "login") {
                await RedirigirDefault("login");
            } 
        }

        public async Task ActualizarSeguimientoEnlace() { 
            string urlActual = navigationManager.Uri;
            string baseUri = navigationManager.BaseUri;
            string paginaActual = urlActual.Replace(baseUri, "");

            await js.SetInSessionStorage(enlaceSeguimiento, paginaActual); 
        }

        public async Task RedirigirPagAnt() { 
            string segEnl = await js.GetFromSessionStorage(enlaceSeguimiento);

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
    }
}

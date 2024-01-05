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
            try {
                string urlActual = navigationManager.Uri;
                string baseUri = navigationManager.BaseUri;
                if (urlActual.Replace(baseUri, "") != "login") {
                    await RedirigirDefault("login");
                }
            } catch (Exception) {
                throw;
            }
        }

        public async Task ActualizarSeguimientoEnlace() {
            try { 
                string urlActual = navigationManager.Uri;
                string baseUri = navigationManager.BaseUri;
                string paginaActual = urlActual.Replace(baseUri, "");

                await js.SetInSessionStorage(enlaceSeguimiento, paginaActual);
            } catch (Exception) {
                throw;
            }
        }

        public async Task RedirigirPagAnt() {
            try { 
                string segEnl = await js.GetFromSessionStorage(enlaceSeguimiento);

                if (!string.IsNullOrWhiteSpace(segEnl)) {
                   await RedirigirDefault(segEnl);
                } else {
                    await RedirigirDefault();
                }
            } catch (Exception) {
                throw;
            }
        }

        public async Task RedirigirDefault (string enlace = "/") { 
            try  {	        
	
                // Acutalizamos el enlace de seguimiento
                await ActualizarSeguimientoEnlace();
                navigationManager.NavigateTo(enlace);
            } catch (Exception) {
                throw;
            }
        }
    }
}

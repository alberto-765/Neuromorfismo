using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Neuromorfismo.FrontEnd.ServiceDependencies;


namespace Neuromorfismo.FrontEnd.Service {
    public class RedirigirManager : IRedirigirManager {
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime js;
        private const string enlaceSeguimientoKey  = "SeguimientoPagina"; // clave del enlace de seguimiento sessionStorage
        public RedirigirManager(NavigationManager navigationManager, IJSRuntime js) {
            this.navigationManager = navigationManager;
            this.js = js;
        }

        /// <summary>
        /// Obtenemos la url de segumiento y redirigimos 
        /// </summary>
        /// <returns></returns>
        public async Task RedirigirPagAnt() { 
            string segEnl = await js.GetFromSessionStorage(enlaceSeguimientoKey);
            await RedirigirDefault(segEnl);
        }

        /// <summary>
        /// Redirige a la url especificada o al inicio
        /// </summary>
        /// <param name="enlace"></param>
        /// <returns></returns>
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
                await ActualizarSeguimiento(enlace);
                navigationManager.NavigateTo(enlace); 
            }
        }

        /// <summary>
        /// Actualiza la url de segumiento
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task ActualizarSeguimiento(string url) {
            await js.SetInSessionStorage(enlaceSeguimientoKey, url);
        }
    }
}

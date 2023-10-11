using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class RedirigirManager : IRedirigirManager {
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime js;
        private string enlaceSeguimiento { get; set; } = "segEnl";
        // replaceHistoryEntry
        public RedirigirManager(NavigationManager navigationManager, IJSRuntime js) {
            this.navigationManager = navigationManager;
            this.js = js;
        }

        public async Task RedirigirLogin() {
            await ActualizarSeguimientoEnlace();
            string urlActual = navigationManager.Uri;
            string baseUri = navigationManager.BaseUri;
            if (urlActual.Replace(baseUri, "") != "login") {
                navigationManager.NavigateTo("login");
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
                navigationManager.NavigateTo(segEnl);
            } else {
                navigationManager.NavigateTo("");
            }
        }

        public void RedirigirDefault (string enlace = "") {
            navigationManager.NavigateTo(enlace);
        }
    }
}

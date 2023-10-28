using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;


namespace WebMedicina.FrontEnd.Service {
    public class RedirigirManager : IRedirigirManager {
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime js;
        private const string enlaceSeguimiento  = "segEnl";
        private readonly ExcepcionDto excepcionPers;
        // replaceHistoryEntry
        public RedirigirManager(NavigationManager navigationManager, IJSRuntime js, ExcepcionDto excepcion) {
            this.navigationManager = navigationManager;
            this.js = js;
            excepcionPers = excepcion;
        }

        public async Task RedirigirLogin() {
            try {
                    string urlActual = navigationManager.Uri;
                string baseUri = navigationManager.BaseUri;
                if (urlActual.Replace(baseUri, "") != "login") {
                    await RedirigirDefault("login");
                }
            } catch (Exception ex) {
                excepcionPers.ConstruirPintarExcepcion(ex);
            }
        }

        public async Task ActualizarSeguimientoEnlace() {
            try { 
                string urlActual = navigationManager.Uri;
                string baseUri = navigationManager.BaseUri;
                string paginaActual = urlActual.Replace(baseUri, "");

                await js.SetInSessionStorage(enlaceSeguimiento, paginaActual);
            } catch (Exception ex) {
                        excepcionPers.ConstruirPintarExcepcion(ex);
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
            } catch (Exception ex) {
                    excepcionPers.ConstruirPintarExcepcion(ex);
             }
}

        public async Task RedirigirDefault (string enlace = "/") { 
            try  {	        
	
                // Acutalizamos el enlace de seguimiento
                await ActualizarSeguimientoEnlace();
                navigationManager.NavigateTo(enlace);
            } catch (Exception ex) {
                excepcionPers.ConstruirPintarExcepcion(ex);
            }
        }
    }
}

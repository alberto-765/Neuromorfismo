using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class PerfilService : IPerfilService{
        private readonly IJSRuntime js;
        private readonly ICrearHttpClient http;
        private readonly HttpClient Http;
        private readonly IRedirigirManager redirigirManager;
        JWTAuthenticationProvider _jwtAuthenticationProvider { get; set; }



        public PerfilService(IJSRuntime js, ICrearHttpClient http, JWTAuthenticationProvider jwtAuthenticationProvider, IRedirigirManager redirigirManager) { 
            this.http = http;
			this.js = js;
			Http = http.CrearHttp();
            _jwtAuthenticationProvider = jwtAuthenticationProvider;
            this.redirigirManager = redirigirManager;
		}

        // Cerrar sesion 
        public async Task CerrarSesion() {
            try {
                // Cerramos sesion
                await _jwtAuthenticationProvider.Logout();

                // Redirigimos al login
                redirigirManager.RedirigirLogin();
            } catch (Exception) {
                throw;
            }
        }
    }
}

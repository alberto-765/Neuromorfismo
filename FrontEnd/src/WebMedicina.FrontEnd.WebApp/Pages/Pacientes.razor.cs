using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using System.Security.Claims;
using MudBlazor.Charts;
using System.Text;
using MudBlazor;
using WebMedicina.FrontEnd.ServiceDependencies;
using static System.Net.WebRequestMethods;
using WebMedicina.Shared.Dto;
using WebMedicina.FrontEnd.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages {
    public partial class Pacientes {
        // Dependencias
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        private ClaimsPrincipal? user { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionDto excepcionPersonalizada { get; set; }
        [Inject] IRedirigirManager _redirigirManager { get; set; }

        // Panel de filtros
        private bool filtrosAbierto { get; set; } = false;
        private bool bloquearFiltros { get; set; } = true;
        private PacienteDto filrosPacientes { get; set; } = new();
        private int? filtroFarmaco { get; set; } = null;

        protected override async Task OnInitializedAsync() {
            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;
            }

            Http = _crearHttpClient.CrearHttp(); // creamos http
        }

        private void EventoFiltros() {
            filtrosAbierto = !filtrosAbierto;
            bloquearFiltros = !bloquearFiltros;
        }
    }
}

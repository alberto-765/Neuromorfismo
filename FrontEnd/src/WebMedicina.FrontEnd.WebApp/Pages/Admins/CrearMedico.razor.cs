using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using WebMedicina.FrontEnd.Service;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;


namespace WebMedicina.FrontEnd.WebApp.Pages.Admins
{
    public partial class CrearMedico {
        private UserRegistroDto userRegistro = new();
        [Inject]
        private ICrearHttpClient crearHttpClient{ get; set; }
        private HttpClient Http;
        protected override void OnInitialized()
        {
            try
            {
                Http = crearHttpClient.CrearHttp();
            }
            catch (Exception ex)
            {
                ExcepcionPersonalizada.ConstruirPintarExcepcion(ex);
            }
        }

        private async Task CrearUsuario()
        {
            HttpResponseMessage respuesta = await Http.PutAsJsonAsync("cuentas/Crear", userRegistro);
            if (respuesta.IsSuccessStatusCode)
            {
                UserToken userToken = await respuesta.Content.ReadFromJsonAsync<UserToken>();
                await loginService.Login(userToken.Token);
                navigationManager.NavigateTo("");
            }
        }
    }
}

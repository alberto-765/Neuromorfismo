using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.WebApp.Pages.Admins {
    public partial class GestionUsers {
        // Tooltip de info para el usuario, donde podra ver los permisos que tiene
        private MarkupString tooltipInfoUser { get; set; }
        private bool mostrarTooltip { get; set; }
        [Inject] IRedirigirManager redirigirManager { get; set; }
        [CascadingParameter(Name = "excepcionPersonalizada")] ExcepcionDto excepcionPersonalizada { get; set; }
        [Inject] ICrearHttpClient _crearHttpClient { get; set; }
        private HttpClient Http { get; set; }
        [CascadingParameter] private Task<AuthenticationState>? authenticationState { get; set; }
        private ClaimsPrincipal? user { get; set; }

        protected override async Task OnInitializedAsync() {
            Http = _crearHttpClient.CrearHttp();

            if (authenticationState is not null) {
                var authState = await authenticationState;
                user = authState?.User;

                // Generamos el texto para el tooltip
                await GenerarTooltip();
            }
        }


        private async Task GenerarTooltip() {

            // Por defecto el rol será medico
            string role = "medico";
            if (user.IsInRole("superAdmin")) {
                role = "superAdmin";
            } else if(user.IsInRole("admin")) {
                role = "admin";
            }


            switch (role) {
                case "superAdmin":
                tooltipInfoUser = new MarkupString($"<div style='text-align: left;'> Usted tiene permisos de Super Admin. <br />" +
                    $"Puede <b>crear, editar y eliminar</b> usuarios de tipo: <br/>" +
                    $"<ul style='padding-left: 15px;'><li>- Administradores</li><li>- Médicos</li></ul></div>");
                    mostrarTooltip = true;
                break;
                case "admin":
                tooltipInfoUser = new MarkupString($"<div style='text-align: left;'> Usted tiene permisos de Administrador. <br />" +
                 $"Puede <b>crear, editar y eliminar</b> usuarios de tipo: <br/>" +
                 $"<ul style='padding-left: 15px;'><li>- Médicos</li></ul></div>");
                mostrarTooltip = true;
                break;

                default:
                    mostrarTooltip = false;
                break;
            }

        }
    }
}

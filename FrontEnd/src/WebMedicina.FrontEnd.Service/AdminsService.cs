using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebMedicina.FrontEnd.ServiceDependencies;

namespace WebMedicina.FrontEnd.Service {
    public class AdminsService :IAdminsService  {

        private IJSRuntime js { get; set; }

        public AdminsService(IJSRuntime js) { 
             this.js = js;
        }

        public async Task<string> GenerarContraseñaAleatoria() {
            try {

                // Generamos constantes para la contraseña
                const string letrasMin = "abcdefghijklmnopqrstuvwxyz";
                const string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
                const string numeros = "1234567890";
                const string especiales = "!@#$%^&*()_+";

                // Generamos objeto random y contraseña la cual se rellenará
                Random random = new Random();
                StringBuilder constra = new StringBuilder();

                // Añadimos 1 letra minuscula
                constra.Append(letrasMay[random.Next(letrasMay.Length)]);

                // Añadimos 1 letra mayuscula
                constra.Append(letrasMin[random.Next(letrasMin.Length)]);
                // Añadimos 3 numeros
                for (int i = 0; i < 5; i++) {
                    constra.Append(numeros[random.Next(numeros.Length)]);
                }
                // Añadimos 1 caracter especial
                constra.Append(especiales[random.Next(especiales.Length)]);

                return constra.ToString();
            } catch (Exception) {
                throw;
            }
        }

        // GESTION DE USUARIOS
        public void GenerarTooltipInfoUser(ClaimsPrincipal user, ref MarkupString tooltipInfoUser, ref bool mostrarTooltip) {
            try {
                // Por defecto el rol será medico
                string role = "medico";
                if (user is not null) {
                    if (user.IsInRole("superAdmin")) {
                        role = "superAdmin";
                    } else if (user.IsInRole("admin")) {
                        role = "admin";
                    }
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
            } catch (Exception) {
                throw;
            }
        }

        public async Task<ReadOnlyDictionary<string, string>> ObtenerFiltrosSession() {
            try {

                // Obtenemos los filtros de session
                var filtrosSession = await js.GetFromSessionStorage("filtrosGestionUsers");

                // Comprobamos que hay filtros almacenados y si no devolvemos los filtros por defecto
                if (!string.IsNullOrEmpty(filtrosSession)) {
                    ReadOnlyDictionary<string, string> filros = JsonSerializer.Deserialize<ReadOnlyDictionary<string, string>>(filtrosSession);

                    if (filros is not null && filros.Any()) {
                        return filros;
                    }
                }
                return new ReadOnlyDictionary<string, string>(CrearDiccionarioFiltros());

            } catch (Exception) {
                throw;
            }
        }

        public Dictionary<string, string> CrearDiccionarioFiltros() {
            return new Dictionary<string, string> {
                { "busqueda" , "" },
                {"campoOrdenar" , "" },
                {"direccionOrdenar", ""} ,
                {"rol" , "" }
            };
        }
    }
}

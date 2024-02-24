using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.ComponentModel.DataAnnotations;
using System.Net.Http.Json;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.UserAccount;
using WebMedicina.Shared.Dto.Usuarios;
using WebMedicina.Shared.Dto.Pacientes;

namespace WebMedicina.FrontEnd.Service
{
    public class AdminsService : IAdminsService {
        // DEPENDENCIAS
        private IJSRuntime js { get; set; }
        private readonly HttpClient _httpClient;

        public AdminsService(IJSRuntime js, ICrearHttpClient crearHttpClient) {
            _httpClient = crearHttpClient.CrearHttpApi();
            this.js = js;
        }

        public Task<string> GenerarContraseñaAleatoria() {
            // Generamos constantes para la contraseña
            const string letrasMin = "abcdefghijklmnopqrstuvwxyz";
            const string letrasMay = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string numeros = "1234567890";
            const string especiales = "!@#$%^&*()_+";

            // Generamos objeto random y contraseña la cual se rellenará
            Random random = new();
            StringBuilder constra = new();

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

            return Task.FromResult(constra.ToString());
        }

        // GESTION DE USUARIOS
        public void GenerarTooltipInfoUser(ClaimsPrincipal user, ref MarkupString tooltipInfoUser, ref bool mostrarTooltip) {
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
                    $"<ul style='padding-left: 15px;'><li>- Administradores.</li><li>- Médicos.</li></ul></div>");
                mostrarTooltip = true;
                break;
                case "admin":
                tooltipInfoUser = new MarkupString($"<div style='text-align: left;'> Usted tiene permisos de Administrador. <br />" +
                    $"Puede <b>crear, editar y eliminar</b> usuarios de tipo: <br/>" +
                    $"<ul style='padding-left: 15px;'><li>- Médicos.</li></ul></div>");
                mostrarTooltip = true;
                break;
                default:
                mostrarTooltip = false;
                break;
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

        public string ValidarNuevoNombre(string nombre) {
            if (!string.IsNullOrWhiteSpace(nombre)) {
                string patron = "[!@#$%^&*(),.?\":{}|<>]";
                if (Regex.IsMatch(nombre, patron)) {
                    return "El nombre no puede contener caracteres especiales.";
                } else if (nombre.Length > 50) {
                    return "La longitud máxima son 50 caracteres.";
                }
            }
            return string.Empty;
        }

        // Validamos si el nombre y apellidos del nuevo usuario son validos 
        public bool ValidarNomYApellUser(string nombre, string apellidos) {
            if (!string.IsNullOrEmpty(nombre) && !string.IsNullOrEmpty(apellidos)) {
                UserRegistroDto usuarioRegistro = new() {
                    Nombre = nombre,
                    Apellidos = apellidos
                };

                // Validamos que el campo del Numero Historia cumpla las validaciones del dto
                var validationErrors = new List<ValidationResult>();
                bool nombreValido = Validator.TryValidateProperty(nombre, new ValidationContext(usuarioRegistro) { MemberName = nameof(usuarioRegistro.Nombre) }, validationErrors);
                bool apellidosValidos = Validator.TryValidateProperty(apellidos, new ValidationContext(usuarioRegistro) { MemberName = nameof(usuarioRegistro.Apellidos) }, validationErrors);

                return nombreValido && apellidosValidos;
            }

            return false;
        }

        /// <summary>
        /// Obtener todos los medicos filtrando por rango del usuario
        /// </summary>
        /// <returns></returns>
        public async Task<List<UserUploadDto>> ObtenerAllMedicos(FiltradoTablaDefaultDto? camposFiltrado = null) {
            // Llamamos a la api para obtener de BBDD los usuarios con los filtros
            HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync("gestionusers/obtenerusuariosfiltrados", camposFiltrado?? new());
            List<UserUploadDto>? list = null;
            if (responseMessage.IsSuccessStatusCode) {
                if (responseMessage.StatusCode != HttpStatusCode.NoContent) {
                    list = await responseMessage.Content.ReadFromJsonAsync<List<UserUploadDto>>();
                } 
            }

            return list ?? new();
        }


        //public async Task ResetearContrasena(RestartPasswordDto restartPass) {
            
        //}
    }
}

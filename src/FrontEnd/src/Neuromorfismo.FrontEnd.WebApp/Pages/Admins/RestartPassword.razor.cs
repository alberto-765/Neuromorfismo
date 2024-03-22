using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Admins {
    public partial class RestartPassword {
        // DEPENDENCIAS
        [Inject] private ISnackbar _snackbar { get; set; } = null!;
        [Inject] private IAdminsService _adminService { get; set; } = null!;

        // ---------- PROPIEDADES -----------
        // Contraseña
        RestartPasswordDto restartPass = new();
        string Pass { get; set; } = string.Empty;
        string? ErrorPassText { get; set; }

        // Lista de medicos para filtrar
        private IEnumerable<UserUploadDto>? ListaMedicos { get; set; } = null;

        // Enviando formulario 
        private bool Enviando { get; set; } = false;

        // ---------- PROPIEDADES -----------



        protected override void OnInitialized() {
            base.OnInitialized();

            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
            _snackbar.Configuration.ClearAfterNavigation = true;
            _snackbar.Configuration.ShowCloseIcon = true;
            _snackbar.Configuration.ShowTransitionDuration = 100;
            _snackbar.Configuration.HideTransitionDuration = 100;
            _snackbar.Configuration.VisibleStateDuration = 1000;
        }


        ///  <summary>
        /// Realizamos llamada para resetear la contraseña del usuario
        /// </summary>
        /// <returns></returns>
        private async Task ResetarPass(EditContext editContext) {
            if (editContext.Validate()) {
                Enviando = true;
                try {
                    if (await _adminService.ResetearContrasena(restartPass)) {
                        // Mostramos mensaje ok y reestablecemos valores
                        _snackbar.Add("Contraseña reestablecida y <i> copiada en su portapapeles </i>", Severity.Success);
                    } else {
                        _snackbar.Add(string.Concat("Error al reestablecer la contraseña",
                            (restartPass.Medico is not null && !string.IsNullOrWhiteSpace(restartPass.Medico.Nombre) && !string.IsNullOrWhiteSpace(restartPass.Medico.Apellidos)
                            ? string.Concat("de <b>", restartPass.Medico.Nombre, " ", restartPass.Medico.Apellidos, "</b>") : ""),
                            "Inténtelo de nuevo o contacte con un administrador"), Severity.Error);
                    }
                } finally {
                    ReiniciarDatos();
                }
            } else {
                ErrorPassText = editContext.GetValidationMessages(() => restartPass.Password).FirstOrDefault();
            }
        }

        /// <summary>
        /// Buscador para autocomplete de medicos
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        private async Task<IEnumerable<UserUploadDto>> BuscarMedPac(string? busqueda) {
            // Si la lista es null se obtiene por primera vez de BD
            ListaMedicos ??= await _adminService.ObtenerAllMedicos();

            // Si hay medicos en la lista se realiza la busqueda
            if (!string.IsNullOrWhiteSpace(busqueda) && ListaMedicos != null && ListaMedicos.Any()) {
                return ListaMedicos.Where(q => ($"{q.UserLogin} - {q.Nombre} {q.Apellidos}").Contains(busqueda, StringComparison.OrdinalIgnoreCase));
            }

            return ListaMedicos ?? Enumerable.Empty<UserUploadDto>();
        }

        private void ReiniciarDatos() {
            Enviando = false;
            restartPass = new();
        }

    }
}

using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Runtime.CompilerServices;
using WebMedicina.FrontEnd.ServiceDependencies;
using WebMedicina.Shared.Dto.UserAccount;

namespace WebMedicina.FrontEnd.WebApp.Pages.Perfil {
    public partial class CambiarContrasena {
        // INJECCIONES
        [Inject] private ISnackbar _snackbar { get; set; } = default!;
        [Inject] private IPerfilService _perfilService { get; set; } = null!;



        // Modelo reestablecer contraseña
        private ChangePasswordDto modelo { get; set; } = new();

        private bool _contraMostrada;
        private bool ContraMostrada {
            get => ContraMostrada;
            set {
                _contraMostrada = value;
                if (_contraMostrada) {
                    InputType = InputType.Text;
                    IconoInput = Icons.Material.Filled.VisibilityOff;
                } else {
                    IconoInput = Icons.Material.Filled.Visibility;
                    InputType = InputType.Password;
                }
            }
        }

        private InputType InputType = InputType.Text;
        private string IconoInput = Icons.Material.Filled.Visibility;
        private bool Enviando {  get; set; } 

        protected override void OnInitialized() {
            base.OnInitialized();

            // Configuracion default snackbar
            _snackbar.Configuration.PreventDuplicates = true;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.TopLeft;
            _snackbar.Configuration.ClearAfterNavigation = true;
            _snackbar.Configuration.ShowCloseIcon = true;
            _snackbar.Configuration.ShowTransitionDuration = 100;
            _snackbar.Configuration.HideTransitionDuration = 100;
        }

        private async Task Enviar() {
            string mensajeAlerta = "No ha sido posible actualizar la contraseña, inténtelo de nuevo mas tarde";
            Severity severityAlerta = Severity.Warning;

            try {
                await _perfilService.CambiarContrasena(modelo);
            } catch (Exception) {
                // No hacemos nada en caso de excepcion
            }
            _snackbar.Add(mensajeAlerta, severityAlerta);
        }
    }
}

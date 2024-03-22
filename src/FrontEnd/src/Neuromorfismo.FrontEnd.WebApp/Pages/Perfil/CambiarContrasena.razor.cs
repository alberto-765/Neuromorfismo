using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Text;
using Neuromorfismo.FrontEnd.ServiceDependencies;
using Neuromorfismo.Shared.Dto.UserAccount;
using Neuromorfismo.Shared.Service;

namespace Neuromorfismo.FrontEnd.WebApp.Pages.Perfil {
    public partial class CambiarContrasena {
        // INJECCIONES
        [Inject] private ISnackbar _snackbar { get; set; } = default!;
        [Inject] private IPerfilService _perfilService { get; set; } = null!;



        // Modelo reestablecer contraseña
        private ChangePasswordDto modelo { get; set; } = new();

        // Contraseña actual
        private InputType InputTypeOldPass = InputType.Password;
        private string IconoInputOldPass = Icons.Material.Filled.Visibility;
        private bool _mostrarOldPass;
        private bool MostrarOldPass {
            get => _mostrarOldPass;
            set {
                _mostrarOldPass = value;
                if (_mostrarOldPass) {
                    InputTypeOldPass = InputType.Text;
                    IconoInputOldPass = Icons.Material.Filled.VisibilityOff;
                } else {
                    IconoInputOldPass = Icons.Material.Filled.Visibility;
                    InputTypeOldPass = InputType.Password;
                }
            }
        }

        // Contraseña nueva
        private InputType InputTypeNewPass = InputType.Password;
        private string IconoInputNewPass = Icons.Material.Filled.Visibility;
        private bool _mostrarNewPass;
        private bool MostrarNewPass {
            get => _mostrarNewPass;
            set {
                _mostrarNewPass = value;
                if (_mostrarNewPass) {
                    InputTypeNewPass = InputType.Text;
                    IconoInputNewPass = Icons.Material.Filled.VisibilityOff;
                } else {
                    IconoInputNewPass = Icons.Material.Filled.Visibility;
                    InputTypeNewPass = InputType.Password;
                }
            }
        }

        // Hacer envio 
        private bool Enviando {  get; set; } 

        // Errores mostrados en el contenedor
        private MarkupString MensajesError { get; set; }

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
            // Mostramos boton de cargando
            Enviando = true;

            // Reiniciamos variables globales
            Severity severityAlerta = Severity.Error;
            StringBuilder stringbuilder = new();
            MensajesError = new();


            try {
                // Obtenemos el codigo de la respuesta
                CodigosErrorChangePass[] codigosError= await _perfilService.CambiarContrasena(modelo);

                if (codigosError.Any()) {
                    // Mapeamos los errores y obtenemos los mensajes de error
                    foreach (CodigosErrorChangePass error in codigosError) {
                        switch (error) {
                            case CodigosErrorChangePass.ContraIncorrecta:
                            stringbuilder.Append("<li>La contraseña actual insertada es incorrecta.</li>");
                            break;

                            // Errores de formato
                            case CodigosErrorChangePass.FaltaMayuscula:
                            stringbuilder.Append("<li>La nueva contraseña debe contener una letra mayúscula.</li>");
                            break;
                            case CodigosErrorChangePass.FaltaNumero:
                            stringbuilder.Append("<li>La nueva contraseña debe contener un número.</li>");
                            break;
                            case CodigosErrorChangePass.FaltaCaractEspecial:
                            stringbuilder.Append("<li>La nueva contraseña debe contener un caracter especial.</li>");
                            break;
                            case CodigosErrorChangePass.ContraCorta:
                            stringbuilder.Append("<li>La nueva contraseña debe contener un mínimo de 8 caracteres.</li>");
                            break;

                            default:
                            stringbuilder.Clear();
                            stringbuilder.Append("No ha sido posible cambiar la contraseña, cierre sesión e inténtelo de nuevo");
                            severityAlerta = Severity.Warning;
                            goto MostrarSnack;
                        }
                    }

                    // Pasamos el html de los errores al contenedor
                    MensajesError = new(stringbuilder.ToString());
                } else {
                    stringbuilder.Append("Contraseña cambiada exitosamente.");
                    severityAlerta = Severity.Success;
                }
            } catch (Exception) {
                // No hacemos nada en caso de excepcion
                stringbuilder.Clear();
                stringbuilder.Append("Error al intentar cambiar la contraseña, inténtelo de nuevo más tarde");
                severityAlerta = Severity.Error;
            }

            MostrarSnack:
                // Solo se entrara por el go to
                if (string.IsNullOrWhiteSpace(MensajesError.Value) && stringbuilder.Length > 0) {
                    _snackbar.Add(stringbuilder.ToString(), severityAlerta);
                }

            // Mostramos boton de enviar normal
            Enviando = false;
        }
    }
}

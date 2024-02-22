using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Dto.UserAccount;
public class ChangePasswordDto : IValidatableObject {

    [Required(ErrorMessage = "Debe especificar la contraseña actual de su cuenta.")]
    public string OldPassword { get; set; } = default!;


    // Que haya 1 letra minuscula, 1 mayuscula, 1 digito y entre 8 y 16 caracteres
    [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*()_+])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un número, al menos una minúscula, al menos una mayúscula y al menos un caracter especial.")]
    [Required(ErrorMessage = "Debe especificar la nueva contraseña.")]
    public string NewPassword { get; set; } = default!;

    [Required(ErrorMessage = "Debe especificar confirmación de la nueva contraseña")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmNewPassword { get; set; } = default!;


    // Validacion personalizada del modelo 
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {

        // Comprobamos que la nueva contraseña no sea igual a la anterior
        if (OldPassword.Equals(NewPassword, StringComparison.InvariantCultureIgnoreCase)) {
            yield return new ("La nueva contraseña debe ser diferente a la contraseña actual.", new[] { nameof(NewPassword), nameof(ConfirmNewPassword) });
        }
    }
}

// Tipos de errores al intentar cambiar la contraseña
public enum CodigosErrorChangePass {
    [Description("No ha sido posible obtener el usuario o cambiar la contraseña.")]
    ErrorDefault,

    [Description("La contraseña actual insertada es incorrecta.")]
    ContraIncorrecta,

    // Formato de la nueva contraseña incorrecto
    FaltaMayuscula,
    FaltaCaractEspecial,
    FaltaNumero,
    ContraCorta
}

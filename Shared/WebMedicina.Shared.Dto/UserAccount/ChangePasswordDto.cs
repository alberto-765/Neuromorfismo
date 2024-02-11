using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Dto.UserAccount;
public class ChangePasswordDto {

    // Que haya 1 letra minuscula, 1 mayuscula, 1 digito y 6 caracteres o más
    [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*()_+])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula, al menos una mayúscula y al menos un caracter especial")]
    [Required(ErrorMessage = "Debe especificar una nueva contraseña")]
    public string NewPassword { get; set; } = string.Empty;

    [Required]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}


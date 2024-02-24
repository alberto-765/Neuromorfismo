using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Dto.Usuarios;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto.UserAccount {
    public class RestartPasswordDto {
        [Required(ErrorMessage ="Debe especificar el usuario al que desea restablecerle la contraseña")]
        public UserUploadDto? Medico { get; set; } = null;

        [RegularExpression(ValidacionesRegistro.PatronPassword, ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un número, al menos una minúscula, al menos una mayúscula y al menos un caracter especial")]
        [Required(ErrorMessage = "Debe especificar la nueva contraseña")]
        public string Password { get; set; } = string.Empty;
    }
}

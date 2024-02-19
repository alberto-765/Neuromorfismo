using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto.Usuarios {

    public class UserRegistroDto {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        [RegularExpression(ValidacionesRegistro.PatronNombres, ErrorMessage = "La primera letra del nombre debe ser mayúscula y contener mínimo 2 caracteres.")]
        [MaxLength(50, ErrorMessage = "El nombre puede contener máximo 50 caracteres")]
        [MinLength(2, ErrorMessage = "El nombre debe tener mínimo 2 caracteres")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "Los apellidos del usuario es obligatorio")]
        [MaxLength(50, ErrorMessage = "Los apellidos pueden contener máximo 50 caracteres")]
        [RegularExpression(ValidacionesRegistro.PatronApellidos, ErrorMessage = "Debe ingresar los dos apellidos del usuario y contener mínimo 2 caracteres cada uno. " +
            "La primera letra debe ser mayúscula.")]
        [MinLength(4, ErrorMessage = "El nombre debe tener mínimo 3 caracteres")]
        public string Apellidos { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacimiento")]
        public DateTime? FechaNac { get; set; } =  ValidacionesRegistro.ObtenerFechaMaxNacimiento();

        [RegularExpression("^[a-z]+$", ErrorMessage = "El nombre de usuario solo puede tener letras y minúsculas")]
        [Required(ErrorMessage = "El usuario para login debe ser generado automáticamente.")]
        public string UserLogin { get; set; } = null!;

        public DateTime FechaCreac { get; init; } = DateTime.Today;
        public DateTime FechaUltMod { get; init; } = DateTime.Today;


        [Required(ErrorMessage = "La contraseña es obligatoria")]
        // Que haya 1 letra minuscula, 1 mayuscula, 1 digito y minimo 8 caracteres
        [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*()_+])(?=.*[A-Z])(?=.*[a-z])\S{8, 16}$", ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula, al menos una mayúscula y al menos un caracter especial")]
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Debes seleccionar un rol para el nuevo usuario")]
        [ValidacionLista("admin", "medico", "superAdmin")]
        public string Rol { get; set; } = null!;

        [Required(ErrorMessage = "El campo género es obligatorio")]
        [ValidacionLista("M", "H")]
        public string Sexo { get; set; } = null!;
    }
}

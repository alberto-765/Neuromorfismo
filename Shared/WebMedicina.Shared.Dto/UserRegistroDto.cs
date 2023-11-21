using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto {
    public class UserRegistroDto {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El nombre no puede contener números")]
        [MaxLength(50, ErrorMessage = "El nombre puede contener máximo 50 caracteres")]
        public string Nombre { get; set; } 

        [Required(ErrorMessage = "Los apellidos del usuario es obligatorio")]
        [MaxLength(50, ErrorMessage = "Los apellidos pueden contener máximo 50 caracteres")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacimiento")]
        public DateTime? FechaNac { get; set; } =  ValidacionesRegistro.ObtenerFechaMaxNacimiento();

        [Required(ErrorMessage = "El usuario para login debe ser generado.")]
        [ReadOnly(true)]
        public string UserLogin { get; set; }

        [ReadOnly(true)]
        public DateOnly FechaCreac { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        [ReadOnly(true)]
        public DateOnly FechaUltMod { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        [Required(ErrorMessage = "La contraseña es obligatoria")]
        // Que haya 1 letra minuscula, 1 mayuscula, 1 digito y 6 caracteres o más
        [RegularExpression(@"^(?=.*\d)(?=.*[!@#$%^&*()_+])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula, al menos una mayúscula y al menos un caracter especial")]
        [ReadOnly(true)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un rol para el nuevo usuario")]
        [ValidacionLista("admin", "medico", "superAdmin")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "El campo género es obligatorio")]
        [ValidacionLista("M", "H")]
        public string Sexo { get; set; }
    }
}

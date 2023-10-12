using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto {
    public class UserRegistroDto {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El nombre no puede contener números")]
        public string Nombre { get; set; } 

        [Required(ErrorMessage = "Los apellidos del usuario es obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacimiento")]
        public DateOnly FechaNacimiento { get; set; } = DateOnly.FromDateTime(ValidacionesRegistro.ObtenerFechaMaxNacimiento());


        [Required(ErrorMessage = "El número de historia es obligatorio")]
        [RegularExpression(@"^AN\d{10}$", ErrorMessage = "El formato debe ser ANXXXXXXXXXX")]
        public string NumHistoria { get; set; }
        [DataType(DataType.Date)]

        public DateOnly FechaCreacion { get; set; } = DateOnly.FromDateTime(DateTime.Now);
        [DataType(DataType.Date)]

        public DateOnly? FechaUltMod { get; set; } = DateOnly.FromDateTime(DateTime.Now);


        [Required(ErrorMessage = "La contraseña es obligatoria")]
        // Que haya 1 letra minuscula, 1 mayuscula, 1 digito y 6 caracteres o más
        [RegularExpression(@"^(?=.*\d)(?=.*[\u0021-\u002b\u003c-\u0040])(?=.*[A-Z])(?=.*[a-z])\S{8,16}$", ErrorMessage = "La contraseña debe tener al entre 8 y 16 caracteres, al menos un dígito, al menos una minúscula, al menos una mayúscula y al menos un caracter especial")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un rol para el nuevo usuario")]
        [ValidacionLista("admin", "medico", "superAdmin")]
        public string Rol {  get; set; }
    }
}

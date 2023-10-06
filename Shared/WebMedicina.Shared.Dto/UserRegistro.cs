using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Dto {
    public class UserRegistro {
        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        public string Nombre { get; set; } 
        [Required(ErrorMessage = "Los apellidos del usuario es obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El número de historia es obligatorio")]
        public string NumHistoria { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaUltMod { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]

        // Que haya 1 letra minuscula, 1 mayuscula, 1 digito y 6 caracteres o más
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)$", ErrorMessage = "La contraseña debe contener al menos un carácter en minúscula, un carácter en mayúscula y un dígito")]
        public string Password { get; set; }
    }
}

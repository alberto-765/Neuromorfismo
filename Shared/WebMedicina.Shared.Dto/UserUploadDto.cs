using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Service;

namespace WebMedicina.Shared.Dto {
    public class UserUploadDto {
        [ReadOnly(true)]
        public string IdMedico { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        [RegularExpression(@"^[^\d]+$", ErrorMessage = "El nombre no puede contener números")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos del usuario es obligatorio")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.DateTime, ErrorMessage = "El formato debe ser dd/MM/yyyy")]
        [CustomValidation(typeof(ValidacionesRegistro), "ValidateFechaNacimiento")]
        public DateTime? FechaNac { get; set; }


        [ReadOnly(true)]
        public DateOnly FechaCreac { get; set; }

        [ReadOnly(true)]
        public DateOnly FechaUltMod { get; set; }

        [Required(ErrorMessage = "Debes seleccionar un rol para el nuevo usuario")]
        [ValidacionLista("admin", "medico", "superAdmin")]
        public string Rol { get; set; }

        [Required(ErrorMessage = "El campo género es obligatorio")]
        [ValidacionLista("M", "H")]
        public string Sexo { get; set; }
    }
}

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
        public int IdMedico { get; set; }

        [ReadOnly(true)]
        public string UserLogin { get; set; }

        [ReadOnly(true)]
        public int Indice { get; set; }

        [Required(ErrorMessage = "El nombre del usuario es obligatorio")]
        [RegularExpression(ValidacionesRegistro.PatronNombres, ErrorMessage = "La primera letra del nombre debe ser mayúscula.")]
        [MaxLength(50, ErrorMessage = "El nombre puede contener máximo 50 caracteres")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Los apellidos del usuario es obligatorio")]
        [MaxLength(50, ErrorMessage = "Los apellidos pueden contener máximo 50 caracteres")]
        [RegularExpression(ValidacionesRegistro.PatronApellidos, ErrorMessage = "Debe ingresar los dos apellidos del usuario. " +
            "La primera letra debe ser mayúscula.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}", ApplyFormatInEditMode = true)]
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

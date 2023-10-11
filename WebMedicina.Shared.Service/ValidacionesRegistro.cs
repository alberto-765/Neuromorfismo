using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Service {
    public static class ValidacionesRegistro {
        public static ValidationResult ValidateFechaNacimiento(DateOnly fechaNacimiento, ValidationContext context) {
            // Valida que la persona tenga al menos 18 años
            if (fechaNacimiento > DateOnly.FromDateTime(DateTime.Now.AddYears(-18))) {
                return new ValidationResult("El usuario debe tener al menos 18 años de edad.");
            }

            return ValidationResult.Success;
        }

        public static DateTime ObtenerFechaMaxNacimiento () {
            return DateTime.Now.AddYears(-18);
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WebMedicina.Shared.Service {
    public static class ValidacionesRegistro {

         // Valida que la persona tenga al menos 18 años
        public static ValidationResult ValidateFechaNacimiento(DateTime fechaNacimiento, ValidationContext context) {
            try {
                if (fechaNacimiento > DateTime.Now.AddYears(-18)) {
                    return new ValidationResult("El usuario debe tener al menos 18 años de edad.");
                }

                return ValidationResult.Success;
            } catch (Exception) {
                throw;
            }
        }

        public static DateTime ObtenerFechaMaxNacimiento () {
            return DateTime.Now.AddYears(-18);
        }
    }
}

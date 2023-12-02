using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace WebMedicina.Shared.Service {
    public static class ValidacionesRegistro {
        public const string PatronNombres = @"^[A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]*( [A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]*){0,2}$";
        public const string PatronApellidos = @"^[A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]*( [A-ZÁÉÍÓÚÜÑ][a-záéíóúüñ]*){1,2}$";


         // Valida que la persona tenga al menos 18 años
        public static ValidationResult ValidateFechaNacimiento(DateTime fechaNacimiento) {
            try {
                if (fechaNacimiento > DateTime.Now.AddYears(-18)) {
                    return new ValidationResult("El usuario debe tener al menos 18 años de edad.");
                }

                return ValidationResult.Success;
            } catch (Exception) {
                throw;
            }
        }  
        
        public static ValidationResult ValidateFechaNacPaciente(DateTime? fechaNacimientoNull) {
            try {

                if(fechaNacimientoNull is not null) {
                    DateTime fechaNacimiento = fechaNacimientoNull ?? DateTime.MinValue;
                    if (DateTime.Compare(fechaNacimiento, DateTime.Now) > 0) {
                        return new ValidationResult("Fecha de nacimiento no válida.");
                    }
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

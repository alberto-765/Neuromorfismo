using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.Shared.Service {
    public class ValidacionLista : ValidationAttribute {
        private readonly List<string> _valoresPermitidos;

        public ValidacionLista(params string[] valoresPermitidos) {
            _valoresPermitidos = new List<string>(valoresPermitidos);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (value == null || _valoresPermitidos.Contains(value.ToString())) {
                return ValidationResult.Success;
            }

            return new ValidationResult($"El campo debe ser uno de los siguientes valores: {string.Join(", ", _valoresPermitidos)}.");
        }
    }
}

using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Service {
    public class ValidacionLista : ValidationAttribute {
        private readonly IEnumerable<string> _valoresPermitidos;

        public ValidacionLista(params string[] valoresPermitidos) {
            _valoresPermitidos = new List<string>(valoresPermitidos);
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext) {
            if (value is not null && _valoresPermitidos.Contains(value.ToString())) {
                return ValidationResult.Success;
            }

            return new ValidationResult($"El campo debe ser uno de los siguientes valores: {string.Join(", ", _valoresPermitidos)}.");
        }
    }
}

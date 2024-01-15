using System.ComponentModel.DataAnnotations;


namespace WebMedicina.Shared.Dto.Tipos {
    public class BaseTipoDto {
        public int Indice { get; set; }

        [Required(ErrorMessage = "El nombre es un campo obligatorio")]
        [MaxLength(50, ErrorMessage = "La longitud máxima son 50 caracteres")]
        [RegularExpression(@"^[^!@#$%^&*(),.?"":{}|<>]*$", ErrorMessage = "El nombre no puede contener caracteres espciales")]
        public string Nombre { get; set; } = string.Empty;

        public DateTime FechaCreac { get; set; }

        public DateTime FechaUltMod { get; set; }

    }
}

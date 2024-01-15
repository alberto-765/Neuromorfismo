using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Dto.LineaTemporal
{
    public class EditarEvolucionLTDto {
        [ReadOnly(true)]
        public int Id { get; set; }

        [ReadOnly(true)]
        public int IdPaciente { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public bool Confirmado { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required]
        public int IdMedicoUltModif { get; set; }

        [ReadOnly(true)]
        public int IdEtapa { get; set; }

    }
}

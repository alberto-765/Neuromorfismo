using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.Shared.Dto.LineaTemporal
{
    public class EditarEvolucionEtapaDto {
        [ReadOnly(true)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo obligatorio")]
        public bool Confirmado { get; set; }

        [Required]
        public DateTime Fecha { get; set; } = DateTime.Today;

        [Required]
        public UserInfoDto MedicoUltModif { get; set; } = null!;

        [Required]
        public int IdEtapa { get; set; }
    }
}

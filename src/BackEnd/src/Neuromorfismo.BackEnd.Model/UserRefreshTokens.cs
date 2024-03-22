using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Neuromorfismo.BackEnd.Model {
    public class UserRefreshTokens {
        [Key]
        public int RefreshTokenId { get; set; }

        [Required]
        public int IdMedico { get; set; } = default!;

        [ForeignKey("IdMedico")]
        public MedicosModel? Medico { get; set; } 

        [Required]
        public string RefreshToken { get; set; } = default!;

        [Required]
        public DateTime FechaExpiracion { get; set; } = default!;
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model {

    [Table("EtapaLT")]
    public class EtapaLTModel : BaseModel {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        [Required]
        public string Titulo { get; set; } = null!; 
        
        [Required]
        [MaxLength(50)]
        public string Label { get; set; } = null!;

        [MaxLength(250)]
        public string? Descripcion { get; set; }

        public int? IdMedicoCreador { get; set; }
        [ForeignKey("IdMedicoCreador")]
        public MedicosModel? MedicoCreador { get; set; }

        [ConcurrencyCheck]
        public int? IdMedicoUltModif { get; set; }

        [ForeignKey("IdMedicoUltModif")]
        public MedicosModel? MedicoUltModif { get; set; }

        public ICollection<EvolucionLTModel> EvolucionEtapa { get; set;} = new List<EvolucionLTModel>();
    }
}

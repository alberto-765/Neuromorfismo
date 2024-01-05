using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model {

    [Table("EtapaLT")]
    public class EtapaLTModel : BaseModel {
        [Key]
        public int Id { get; set; }
        
        [MaxLength(50)]
        [Description("Titulo para la etapa")]
        public string Titulo { get; set; } = null!; 
        
        [MaxLength(50)]
        [Description("Label para la etapa")]
        public string Label { get; set; } = null!;

        [MaxLength(250)]
        [Description("Descripcion de la etapa)]")]
        public string? Descripcion { get; set; }

        public virtual MedicosModel? MedicoCreador { get; set; }
        public virtual MedicosModel? MedicoUltModif { get; set; } 
    }
}

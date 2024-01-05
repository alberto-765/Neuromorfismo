using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model {

    [Table("EvolucionLT")]
    public class EvolucionLTModel {
        [Key]
        public int Id { get; set; }

        [Description("Respuesta en la línea temporal")]
        public bool Confirmado { get; set; }

        public DateTime Fecha { get; set; } = DateTime.Today;

        public virtual MedicosModel MedicoUltModif { get; set; } = null!;

        public virtual EtapaLTModel EtapasLT { get; set; } = null!;
    }
}

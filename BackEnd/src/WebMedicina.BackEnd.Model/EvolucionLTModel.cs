using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMedicina.BackEnd.Model {

    [Table("EvolucionLT")]
    public class EvolucionLTModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public bool Confirmado { get; set; }

        [Required]
        [Description("Fecha de la ultima modificacion de la evolucion del paciente.")]
        public DateTime Fecha { get; set; } = DateTime.Today;

        public int IdMedicoUltModif { get; set; }
        [ForeignKey("IdMedicoUltModif")]
        public MedicosModel MedicoUltModif { get; set; } = null!;

        public int IdEtapa { get; set; }
        [ForeignKey("IdEtapa")]
        public EtapaLTModel Etapa { get; set; } = null!;

        public int IdPaciente { get; set; }
        [ForeignKey("IdPaciente")]
        public PacientesModel Paciente { get; set; } = null!;

    }
}

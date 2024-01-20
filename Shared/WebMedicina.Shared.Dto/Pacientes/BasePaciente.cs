using System.ComponentModel.DataAnnotations;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.Shared.Dto.Pacientes
{
    public class BasePaciente
    {
        public int IdPaciente { get; init; }
        public virtual string NumHistoria { get; set; } = string.Empty;
        public virtual DateTime? FechaNac { get; set; }
        public virtual string Sexo { get; set; } = string.Empty;
        public virtual int Talla { get; set; } = 50;

        public virtual DateTime? FechaDiagnostico { get; set; }
        public virtual DateTime? FechaFractalidad { get; set; }
        public virtual string Farmaco { get; set; } = string.Empty;
        public virtual bool EnfermRaras { get; set; }
        public virtual string DescripEnferRaras { get; set; } = null!;

        public DateTime FechaCreac { get; init; }
        public DateTime FechaUltMod { get; init; }
        public virtual string NombreMedicoUltMod { get; set; } = string.Empty;
        public virtual string NombreMedicoCreador { get; set; } = string.Empty;
        public int MedicoUltMod { get; init; }
        public int MedicoCreador { get; init; }

        [Required(ErrorMessage = "Debes especificar una mutación para el paciente.")]
        public virtual MutacionesDto? Mutacion { get; set; } = null;

        public virtual EpilepsiasDto? Epilepsia { get; set; } = null;
    }
}

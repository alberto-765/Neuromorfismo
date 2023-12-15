
using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Dto {
	public class PacienteDto {

		public int IdPaciente { get; set; }
        public string NumHistoria { get; set; } = null!;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime FechaNac { get; set; }

		public string Sexo { get; set; } = string.Empty;

		public int Talla { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime FechaDiagnostico { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateTime FechaFractalidad { get; set; }

		public string Farmaco { get; set; } = string.Empty;

		public int? IdEpilepsia { get; set; }
        public string? NombreEpilepsia { get; set; }

        public int? IdMutacion { get; set; }
        public string? NombreMutacion { get; set; }


        public string EnfermRaras { get; set; } = string.Empty;

		public string DescripEnferRaras { get; set; } = string.Empty;

		[DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateOnly FechaCreac { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{dd/MM/yyyy}")]
        public DateOnly FechaUltMod { get; set; }

		public string MedicoUltMod { get; set; } = string.Empty;

        public string MedicoCreador { get; set; } = string.Empty;

        // Medicos que tienen permisos sobre el paciente
        public Dictionary<int, string?> MedicosPacientes { get; set; } = null!;
    }
}

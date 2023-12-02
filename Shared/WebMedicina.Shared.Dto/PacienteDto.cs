
namespace WebMedicina.Shared.Dto {
	public class PacienteDto {

		public int IdPaciente { get; set; }
        public string NumHistoria { get; set; } = null!;

        public DateTime FechaNac { get; set; }

		public string Sexo { get; set; } = string.Empty;

		public decimal Talla { get; set; }

		public DateTime FechaDiagnostico { get; set; }

		public DateTime FechaFractalidad { get; set; }

		public string Farmaco { get; set; } = string.Empty;

		public string TipoEpilepsia { get; set; } = string.Empty;

		public string TipoMutacion { get; set; } = string.Empty;

		public string EnfermRaras { get; set; } = string.Empty;

		public string DescripEnferRaras { get; set; } = string.Empty;

		public DateOnly FechaCreac { get; set; }

		public DateOnly FechaUltMod { get; set; }

		public KeyValuePair<int, string> MedicoUltMod { get; set; } = new();

		public KeyValuePair<int, string> MedicoCreador { get; set; } = new();
    }
}

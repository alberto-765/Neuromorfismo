using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebMedicina.FrontEnd.Dto {
	public class PacienteDto {

		public int IdPaciente { get; set; }

		public DateTime? FechaNac { get; set; }

		public string Sexo { get; set; } = String.Empty;

		public decimal Talla { get; set; }

		public DateTime? FechaDiagnostico { get; set; }

		public DateTime? FechaFractalidad { get; set; }

		public string Farmaco { get; set; } = string.Empty;

		public string TipoEpilepsia { get; set; } = string.Empty;

		public string TipoMutacion { get; set; } = string.Empty;

		public string EnfermRaras { get; set; } = "No";

		public string DescripEnferRaras { get; set; } = String.Empty;

		public DateOnly FechaCreac { get; set; }

		public DateOnly FechaUltMod { get; set; }

		public string MedicoUltMod { get; set; } = string.Empty;

		public string MedicoCreador { get; set; } = string.Empty;
		public bool Mostrar { get; set; } = false;

	}
}

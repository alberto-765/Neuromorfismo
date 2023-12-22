
using System.ComponentModel.DataAnnotations;

namespace WebMedicina.Shared.Dto {
	public class PacienteDto : BasePaciente<DateTime?, string> {
		public int IdPaciente { get; set; }
        public string? NombreEpilepsia { get; set; }
        public string? NombreMutacion { get; set; }

        // Medicos que tienen permisos sobre el paciente
        public Dictionary<int, string?> MedicosPacientes { get; set; } = null!;
    }
}

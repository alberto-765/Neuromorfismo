using WebMedicina.BackEnd.Model;

namespace WebMedicina.BackEnd.Dto {
    public class InfoPacienteDto {
        public PacientesModel Paciente { get; set; } = null!;
        public string? NombreEpilepsia { get; set; }
        public string? NombreMutacion { get; set; }

    }
}

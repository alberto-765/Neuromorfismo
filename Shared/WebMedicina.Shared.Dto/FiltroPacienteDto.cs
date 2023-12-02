namespace WebMedicina.Shared.Dto {
    public class FiltroPacienteDto {

        public string? Sexo { get; set; } = null;

        public decimal? Talla { get; set; } = 50;

        public IEnumerable<string> TipoEpilepsias { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> TipoMutacion { get; set; } = Enumerable.Empty<string>();

        public bool EnfermRaras { get; set; } = false;

        public string? Medico { get; set; } = null; // Filtrado por los pacientes de un medico
    }
}

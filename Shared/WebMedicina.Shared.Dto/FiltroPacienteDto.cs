namespace WebMedicina.Shared.Dto {
    public class FiltroPacienteDto : BasePaciente{

        public IEnumerable<string> TipoEpilepsias { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<string> TipoMutacion { get; set; } = Enumerable.Empty<string>();

        public UserInfoDto? Medico { get; set; } = null; // Filtrado por los pacientes de un medico
    }
}

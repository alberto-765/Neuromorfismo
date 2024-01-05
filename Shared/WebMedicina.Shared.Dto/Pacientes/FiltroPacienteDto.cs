using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.Shared.Dto.Pacientes
{
    public class FiltroPacienteDto : BasePaciente
    {

        public IEnumerable<EpilepsiasDto> TipoEpilepsias { get; set; } = new HashSet<EpilepsiasDto>();

        public IEnumerable<MutacionesDto> TipoMutacion { get; set; } = new HashSet<MutacionesDto>();

        public UserInfoDto? Medico { get; set; } = null; // Filtrado por los pacientes de un medico

        public bool? FiltrarEnfRara { get; set; } = null;
    }
}

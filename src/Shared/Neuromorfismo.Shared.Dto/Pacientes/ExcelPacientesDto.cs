using System.Collections.Immutable;

namespace Neuromorfismo.Shared.Dto.Pacientes;
    public class ExcelPacientesDto {
        public ImmutableList<CrearPacienteDto> Pacientes { get; set; } = ImmutableList<CrearPacienteDto>.Empty;
        public string NombrePaginaExcel { get; set; } = string.Empty;
    }


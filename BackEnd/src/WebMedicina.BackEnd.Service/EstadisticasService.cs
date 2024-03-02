using System.Collections.Immutable;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto.Estadisticas;

namespace WebMedicina.BackEnd.Service;
public class EstadisticasService : IEstadisticasService{
    private readonly EstadisticasDal _estadisticasDal;

    public EstadisticasService(EstadisticasDal estadisticasDal) {
        _estadisticasDal = estadisticasDal;
    }

    public async Task<ImmutableList<TotalPacientesDto>> ObtenerTotalPaciente() {
        await _estadisticasDal.GetTotalPaciente();
        return ImmutableList<TotalPacientesDto>.Empty;
    }
}


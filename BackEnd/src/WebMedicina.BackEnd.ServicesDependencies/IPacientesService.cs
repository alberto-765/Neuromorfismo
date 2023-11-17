using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IPacientesService {
        Task<IEnumerable<string>> GetAllMed();
        List<FarmacosDto> ObtenerFarmacos();
        List<MutacionesDto> ObtenerMutaciones();
        List<EpilepsiasDto> ObtenerEpilepsias();
    }
}

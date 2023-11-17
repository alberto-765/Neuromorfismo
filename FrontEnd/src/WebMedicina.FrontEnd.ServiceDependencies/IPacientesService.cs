using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IPacientesService {
        public Task<IEnumerable<string>> ObtenerAllMed();
        public Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerFiltros();
    }
}

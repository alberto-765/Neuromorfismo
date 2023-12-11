using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IPacientesService {
        Task<IEnumerable<string>> ObtenerAllMed();
        Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerFiltros();
        Task<bool> ValidarNumHistoria(string numHistoria);
        Task<HttpResponseMessage> CrearPaciente(CrearPacienteDto nuevoPaciente);
        Task<List<PacienteDto>?> ObtenerPacientes();
        Task<List<PacienteDto>?> FiltrarPacientes(FiltroPacienteDto filtrsPacientes);
    }
}

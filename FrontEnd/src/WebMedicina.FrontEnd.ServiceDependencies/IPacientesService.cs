using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.FrontEnd.ServiceDependencies {
    public interface IPacientesService {
        Task<IEnumerable<UserInfoDto>> ObtenerAllMed();
        Task<(List<FarmacosDto>? ListaFarmacos, List<EpilepsiasDto>? ListaEpilepsias, List<MutacionesDto>? ListaMutaciones)> ObtenerFiltros();
        Task<bool> ValidarNumHistoria(string numHistoria);
        Task<HttpResponseMessage> CrearPaciente(CrearPacienteDto nuevoPaciente);
        Task<HttpResponseMessage> EditarPaciente(CrearPacienteDto nuevoPaciente);
        Task<HttpResponseMessage> EliminarPaciente(int idPaciente);
        Task<List<CrearPacienteDto>?> ObtenerPacientes();
        List<CrearPacienteDto> FiltrarPacientes(FiltroPacienteDto filtrsPacientes, List<CrearPacienteDto>? listaPacientes);
        List<CrearPacienteDto> FiltrarMisPacientes(List<CrearPacienteDto>? listaPacientes, ClaimsPrincipal? user);
        Task BloquearScroll(string idDialogo);
        Task DesbloquearScroll(string idDialogo);
        Task<List<CrearPacienteDto>?> AnadirPacienteALista(int idPaciente);
        Task<List<CrearPacienteDto>?> EliminarPacienteLista(int idPaciente);
    }
}

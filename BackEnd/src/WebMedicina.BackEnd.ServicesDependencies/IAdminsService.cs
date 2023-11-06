using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IAdminsService {
        // [WEB V1] - USUARIOS Y MEDICOS
        bool CrearMedico(UserRegistroDto nuevoMedico, String idUsuario);
        Task<List<UserUploadDto>> ObtenerFiltradoUsuarios(Dictionary<string, string> filtros, ClaimsPrincipal user);
        List<UserUploadDto> FiltrarUsuarios(List<UserUploadDto> listaUsuarios, ClaimsPrincipal user);
        Task<bool> ActualizarMedico(UserUploadDto medicoActualizado);

        // [WEB V1] - EPILEPSIAS
        List<EpilepsiasDto> ObtenerEpilepsias();
        Task<bool> CrearNuevaEpilepsia(string nombre);
        Task<bool> EliminarEpilepsia(int idEpilepsia);
        Task<(bool validacionEntry, bool filasModif)> ActualizarEpilepsia(EpilepsiasDto epilepsia);

        // [WEB V1] - MUTACIONES
        List<MutacionesDto> ObtenerMutaciones();
        Task<bool> CrearNuevaMutacion(string nombre);
        Task<bool> EliminarMutacion(int idMutacion);
        Task<(bool validacionEntry, bool filasModif)> ActualizarMutacion(MutacionesDto mutacion);

        // [WEB V1] - FARMACOS
        List<FarmacosDto> ObtenerFarmacos();
        Task<bool> CrearNuevoFarmaco(string nombre);
        Task<bool> EliminarFarmaco(int idFarmaco);
        Task<(bool validacionEntry, bool filasModif)> ActualizarFarmaco(FarmacosDto farmaco);
    }
}

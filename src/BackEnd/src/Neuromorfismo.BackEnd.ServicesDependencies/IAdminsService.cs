using System.Security.Claims;
using Neuromorfismo.Shared.Dto.Pacientes;
using Neuromorfismo.Shared.Dto.Tipos;
using Neuromorfismo.Shared.Dto.Usuarios;

namespace Neuromorfismo.BackEnd.ServicesDependencies {
    public interface IAdminsService {
        // USUARIOS Y MEDICOS
        bool CrearMedico(UserRegistroDto nuevoMedico, String idUsuario);
        Task<List<UserUploadDto>> ObtenerFiltradoUsuarios(FiltradoTablaDefaultDto camposFiltrado, ClaimsPrincipal userClaims);
        List<UserUploadDto> FiltrarUsuariosPorPermisos(List<UserUploadDto> listaUsuarios, ClaimsPrincipal user);
        Task<bool> ActualizarMedico(UserUploadDto medicoActualizado);

        // EPILEPSIAS
        List<EpilepsiasDto> ObtenerEpilepsias();
        Task<bool> CrearNuevaEpilepsia(string nombre);
        Task<bool> EliminarEpilepsia(int idEpilepsia);
        Task<(bool validacionEntry, bool filasModif)> ActualizarEpilepsia(EpilepsiasDto epilepsia);

        // MUTACIONES
        List<MutacionesDto> ObtenerMutaciones();
        Task<bool> CrearNuevaMutacion(string nombre);
        Task<bool> EliminarMutacion(int idMutacion);
        Task<(bool validacionEntry, bool filasModif)> ActualizarMutacion(MutacionesDto mutacion);

        // FARMACOS
        List<FarmacosDto> ObtenerFarmacos();
        Task<bool> CrearNuevoFarmaco(string nombre);
        Task<bool> EliminarFarmaco(int idFarmaco);
        Task<(bool validacionEntry, bool filasModif)> ActualizarFarmaco(FarmacosDto farmaco);
    }
}

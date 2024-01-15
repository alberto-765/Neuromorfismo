using System.Security.Claims;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.Service
{
    public class AdminsService : IAdminsService {

        private readonly AdminDal _adminDal;
        private readonly EpilepsiasDal _epilepsiasDal;
        private readonly FarmacosDal _farmacosDal;
        private readonly MutacionesDal _mutacionesDal;


        // Constructor con dependencias
        public AdminsService(AdminDal adminDal, EpilepsiasDal epilepsiasDal, FarmacosDal farmacosDal, MutacionesDal mutacionesDal) {
            _adminDal = adminDal;
            _epilepsiasDal = epilepsiasDal;
            _farmacosDal = farmacosDal;
            _mutacionesDal = mutacionesDal;
        }

        // Crear nuevo usuario (superAdmin, admin medico)
        public bool CrearMedico(UserRegistroDto nuevoMedico, string idUsuario) {
            return _adminDal.CrearNuevoMedico(nuevoMedico, idUsuario);
        }

        // Filtrar tabla de usuarios
        public async Task<List<UserUploadDto>> ObtenerFiltradoUsuarios(FiltradoTablaDefaultDto camposFiltrado, ClaimsPrincipal userClaims) {
            return FiltrarUsuariosPorPermisos(await _adminDal.ObtenerMedicos(camposFiltrado, userClaims.ToUserInfoDto()), userClaims);
        }

        // Filtramos por los permisos del administrador 
        public List<UserUploadDto> FiltrarUsuariosPorPermisos(List<UserUploadDto> listaUsuarios, ClaimsPrincipal user) {

            // Los administradores no podrán editar a super administradores
            if (user.IsInRole("admin")) {
                return (from q in listaUsuarios where q.Rol == "medico" select q).ToList();
            } else {
                return listaUsuarios;
            }
        }

        // Update del medico pasado por parametro
        public async Task<bool> ActualizarMedico (UserUploadDto medicoActualizado) {
            return await _adminDal.UpdateMedico(medicoActualizado);
        }


        // Obtener todas las epilepsias disponibles 
        public List<EpilepsiasDto> ObtenerEpilepsias() {
            return _epilepsiasDal.GetEpilepsias();
        }

        // Crear una nueva epilepsia
        public async Task<bool> CrearNuevaEpilepsia(string nombre) {
            return await _epilepsiasDal.CrearEpilepsia(nombre);
        }

        // Eliminar epilepsia
        public async Task<bool> EliminarEpilepsia(int idEpilepsia) {
            return await _epilepsiasDal.DeleteEpilepsia(idEpilepsia);
        }

        // Actualizar nombre epilepsia
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarEpilepsia(EpilepsiasDto epilepsia) {
            return await _epilepsiasDal.UpdateEpilepsia(epilepsia);
        }


        // Obtener todas las mutaciones disponibles 
        public List<MutacionesDto> ObtenerMutaciones() {
            return _mutacionesDal.GetMutaciones();
        }

        // Crear una nueva mutacion
        public async Task<bool> CrearNuevaMutacion(string nombre) {
            return await _mutacionesDal.CrearMutacion(nombre);
        }

        // Eliminar mutacion
        public async Task<bool> EliminarMutacion(int idMutacion) {
            return await _mutacionesDal.DeleteMutacion(idMutacion);
        }

        // Actualizar nombre mutacion
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarMutacion(MutacionesDto mutacion) {
            return await _mutacionesDal.UpdateMutacion(mutacion);
        }


        // Obtener todas las farmacos disponibles 
        public List<FarmacosDto> ObtenerFarmacos() {
            return _farmacosDal.GetFarmacos();  
        }

        // Crear una nuevo farmaco
        public async Task<bool> CrearNuevoFarmaco(string nombre) {
            return await _farmacosDal.CrearFarmaco(nombre);
        }

        // Eliminar farmaco
        public async Task<bool> EliminarFarmaco(int idFarmaco) {
            return await _farmacosDal.DeleteFarmaco(idFarmaco);
        }

        // Actualizar nombre farmaco
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarFarmaco(FarmacosDto farmaco) {
            return await _farmacosDal.UpdateFarnaco(farmaco);
        }
    }
}

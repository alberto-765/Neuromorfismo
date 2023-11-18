using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Service {
    public class AdminsService : IAdminsService {

        private readonly AdminDal _adminDal;
        private readonly EpilepsiasDal _epilepsiasDal;
        private readonly FarmacosDal _farmacosDal;
        private readonly MutacionesDal _mutacionesDal;
        private readonly IMapper _mapper;


        // Constructor con dependencias
        public AdminsService(AdminDal adminDal, IMapper mapper) {
            _adminDal = adminDal;
            _mapper = mapper;   
        }

        // Crear nuevo usuario (superAdmin, admin medico)
        public bool CrearMedico(UserRegistroDto nuevoMedico, string idUsuario) {
            try {
                return _adminDal.CrearNuevoMedico(nuevoMedico, idUsuario);
            } catch (Exception) { throw; }
        }

        // Filtrar tabla de usuarios
        public async Task<List<UserUploadDto>> ObtenerFiltradoUsuarios(Dictionary<string, string> filtros, ClaimsPrincipal user) {
            try {
                return FiltrarUsuarios(await _adminDal.ObtenerMedicos(filtros, _mapper.Map<UserInfoDto>(user)), user);
            } catch (Exception) { throw; }
        }

        // Filtramos por los permisos del administrador 
        public List<UserUploadDto> FiltrarUsuarios(List<UserUploadDto> listaUsuarios, ClaimsPrincipal user) {

            // Los administradores no podrán editar a super administradores
            if (user.IsInRole("admin")) {
                return (from q in listaUsuarios where q.Rol == "medico" select q).ToList();
            } else {
                return listaUsuarios;
            }
        }

        // Update del medico pasado por parametro
        public async Task<bool> ActualizarMedico (UserUploadDto medicoActualizado) {
            try {
                return await _adminDal.UpdateMedico(medicoActualizado);
            } catch (Exception) { throw; }
        }


        // Obtener todas las epilepsias disponibles 
        public List<EpilepsiasDto> ObtenerEpilepsias() {
            try {
                 return _epilepsiasDal.GetEpilepsias();
            } catch (Exception) { throw; }
        }

        // Crear una nueva epilepsia
        public async Task<bool> CrearNuevaEpilepsia(string nombre) {
            try {
                return await _epilepsiasDal.CrearEpilepsia(nombre);
            } catch (Exception) { throw; }
        }

        // Eliminar epilepsia
        public async Task<bool> EliminarEpilepsia(int idEpilepsia) {
            try {
                return await _epilepsiasDal.DeleteEpilepsia(idEpilepsia);
            } catch (Exception) { throw; }
        }

        // Actualizar nombre epilepsia
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarEpilepsia(EpilepsiasDto epilepsia) {
            try {
                return await _epilepsiasDal.UpdateEpilepsia(epilepsia);
            } catch (Exception) { throw; }
        }


        // Obtener todas las mutaciones disponibles 
        public List<MutacionesDto> ObtenerMutaciones() {
            try {
                return _mutacionesDal.GetMutaciones();
            } catch (Exception) { throw; }
        }

        // Crear una nueva mutacion
        public async Task<bool> CrearNuevaMutacion(string nombre) {
            try {
                return await _mutacionesDal.CrearMutacion(nombre);
            } catch (Exception) { throw; }
        }

        // Eliminar mutacion
        public async Task<bool> EliminarMutacion(int idMutacion) {
            try {
                return await _mutacionesDal.DeleteMutacion(idMutacion);
            } catch (Exception) { throw; }
        }

        // Actualizar nombre mutacion
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarMutacion(MutacionesDto mutacion) {
            try {
                return await _mutacionesDal.UpdateMutacion(mutacion);
            } catch (Exception) { throw; }
        }


        // Obtener todas las farmacos disponibles 
        public List<FarmacosDto> ObtenerFarmacos() {
            try {
                return _farmacosDal.GetFarmacos();
            } catch (Exception) { throw; }
        }

        // Crear una nuevo farmaco
        public async Task<bool> CrearNuevoFarmaco(string nombre) {
            try {
                return await _farmacosDal.CrearFarmaco(nombre);
            } catch (Exception) { throw; }
        }

        // Eliminar farmaco
        public async Task<bool> EliminarFarmaco(int idFarmaco) {
            try {
                return await _farmacosDal.DeleteFarmaco(idFarmaco);
            } catch (Exception) { throw; }
        }

        // Actualizar nombre farmaco
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarFarmaco(FarmacosDto farmaco) {
            try {
                return await _farmacosDal.UpdateFarnaco(farmaco);
            } catch (Exception) { throw; }
        }
    }
}

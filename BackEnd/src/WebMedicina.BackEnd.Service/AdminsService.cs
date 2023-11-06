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
        private readonly IMapper _mapper;


        // Constructor con dependencias
        public AdminsService(AdminDal adminDal, IMapper mapper) {
            _adminDal = adminDal;
            _mapper = mapper;   
        }

        // [WEB V1] - Crear nuevo usuario (superAdmin, admin medico)
        public bool CrearMedico(UserRegistroDto nuevoMedico, string idUsuario) {
            try {
                return _adminDal.CrearNuevoMedico(nuevoMedico, idUsuario);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Filtrar tabla de usuarios
        public async Task<List<UserUploadDto>> ObtenerFiltradoUsuarios(Dictionary<string, string> filtros, ClaimsPrincipal user) {
            try {
                return FiltrarUsuarios(await _adminDal.ObtenerMedicos(filtros, _mapper.Map<UserInfoDto>(user)), user);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Filtramos por los permisos del administrador 
        public List<UserUploadDto> FiltrarUsuarios(List<UserUploadDto> listaUsuarios, ClaimsPrincipal user) {

            // Los administradores no podrán editar a super administradores
            if (user.IsInRole("admin")) {
                return (from q in listaUsuarios where q.Rol == "medico" select q).ToList();
            } else {
                return listaUsuarios;
            }
        }

        // [WEB V1] - Update del medico pasado por parametro
        public async Task<bool> ActualizarMedico (UserUploadDto medicoActualizado) {
            try {
                return await _adminDal.UpdateMedico(medicoActualizado);
            } catch (Exception) { throw; }
        }





        // [WEB V1] - Obtener todas las epilepsias disponibles 
        public List<EpilepsiasDto> ObtenerEpilepsias() {
            try {
                 return _adminDal.GetEpilepsias();
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Crear una nueva epilepsia
        public async Task<bool> CrearNuevaEpilepsia(string nombre) {
            try {
                return await _adminDal.CrearEpilepsia(nombre);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Eliminar epilepsia
        public async Task<bool> EliminarEpilepsia(int idEpilepsia) {
            try {
                return await _adminDal.DeleteEpilepsia(idEpilepsia);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Actualizar nombre epilepsia
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarEpilepsia(EpilepsiasDto epilepsia) {
            try {
                return await _adminDal.UpdateEpilepsia(epilepsia);
            } catch (Exception) { throw; }
        }




        // [WEB V1] - Obtener todas las mutaciones disponibles 
        public List<MutacionesDto> ObtenerMutaciones() {
            try {
                return _adminDal.GetMutaciones();
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Crear una nueva mutacion
        public async Task<bool> CrearNuevaMutacion(string nombre) {
            try {
                return await _adminDal.CrearMutacion(nombre);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Eliminar mutacion
        public async Task<bool> EliminarMutacion(int idMutacion) {
            try {
                return await _adminDal.DeleteMutacion(idMutacion);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Actualizar nombre mutacion
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarMutacion(MutacionesDto mutacion) {
            try {
                return await _adminDal.UpdateMutacion(mutacion);
            } catch (Exception) { throw; }
        }





        // [WEB V1] - Obtener todas las farmacos disponibles 
        public List<FarmacosDto> ObtenerFarmacos() {
            try {
                return _adminDal.GetFarmacos();
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Crear una nuevo farmaco
        public async Task<bool> CrearNuevoFarmaco(string nombre) {
            try {
                return await _adminDal.CrearFarmaco(nombre);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Eliminar farmaco
        public async Task<bool> EliminarFarmaco(int idFarmaco) {
            try {
                return await _adminDal.DeleteFarmaco(idFarmaco);
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Actualizar nombre farmaco
        public async Task<(bool validacionEntry, bool filasModif)> ActualizarFarmaco(FarmacosDto farmaco) {
            try {
                return await _adminDal.UpdateFarnaco(farmaco);
            } catch (Exception) { throw; }
        }
    }
}

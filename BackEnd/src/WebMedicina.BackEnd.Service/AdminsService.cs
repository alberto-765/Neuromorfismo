using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public bool CrearMedico(UserRegistroDto nuevoMedico, string idUsuario) {
            try {
                return _adminDal.CrearNuevoMedico(nuevoMedico, idUsuario);
            } catch (Exception) { throw; }
        }

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
                 return _adminDal.GetEpilepsias();
            } catch (Exception) { throw; }
        }

    }
}

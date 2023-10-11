using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Service {
    public class AdminsService : IAdminsService {

        private readonly AdminDal _adminDal;

        // Constructor con dependencias
        public AdminsService(AdminDal adminDal) {
            _adminDal = adminDal;
        }

        public async Task<bool> CrearMedico(UserRegistroDto nuevoMedico, string idUsuario) {
            return _adminDal.CrearNuevoMedico(nuevoMedico, idUsuario);
        }
    }
}

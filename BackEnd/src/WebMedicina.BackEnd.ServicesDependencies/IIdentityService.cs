using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IIdentityService  {

        // Funciones
        UserInfoDto ObtenerUsuario(string numHistoria);
        Task<bool> CrearUser(IdentityUser user, UserRegistroDto model);
        Task<bool> ComprobarContraseña(UserLoginDto userLogin);
    }
}

using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.ServicesDependencies {
    public interface IIdentityService  {

        Task<MedicosModel?> ObtenerUsuarioYRol(int idMedico);
        Task<MedicosModel?> ObtenerUsuarioYRolLogin(string userName); // Obtener info del usuario en el login
        Task<bool> CrearUser(IdentityUser user, UserRegistroDto model);
        Task<bool> ComprobarContraseña(UserLoginDto userLogin);
        Task<bool> ComprobarUserName(string userName);
        Task<bool> ActualizarRol(string userLogin, string nuevoRol);
    }
}

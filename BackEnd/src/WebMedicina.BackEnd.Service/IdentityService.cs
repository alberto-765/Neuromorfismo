using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Service {
    public class IdentityService : IIdentityService {
        private readonly MedicoDal _medicoDal;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public IdentityService(RoleManager<IdentityRole> roleManager, MedicoDal medicoDal, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager) {
            _roleManager = roleManager;
            _medicoDal = medicoDal;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public UserInfoDto ObtenerUsuario(string numHistoria) {
            try {
                return _medicoDal.ObtenerInfoUser(numHistoria);
            } catch (Exception) {
                throw;
            }
        }

        public async Task<bool> CrearUser(IdentityUser user, UserRegistroDto model) {
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded) {
                // Añadimos el usuario a su rol
                result = await _userManager.AddToRoleAsync(user, model.Rol);

                if (result.Succeeded) {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> ComprobarContraseña(UserLoginDto userLogin) {
            var respuesta = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, isPersistent: true, lockoutOnFailure: true);
            return respuesta.Succeeded;
        }
    }
}

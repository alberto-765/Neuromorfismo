using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
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
        public async Task<MedicosModel?> ObtenerUsuarioYRol(string numHistoria) {
            try {
                MedicosModel? medicosModel = _medicoDal.ObtenerInfoUser(numHistoria);

                // Obtenemos el rol si se ha obtenido correctamente la info del usuario
                if (medicosModel is not null) {
                   var roles = await ObtenerRol(numHistoria);

                    if(roles is not null) {
                        medicosModel.Roles = roles;
                    }
                }

                return medicosModel;
            } catch (Exception) {
                throw;
            }
        }

        public async Task<bool> CrearUser(IdentityUser user, UserRegistroDto model) {
            try {
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    // Añadimos el usuario a su rol
                    result = await _userManager.AddToRoleAsync(user, model.Rol);

                    if (result.Succeeded) {
                        return true;
                    }
                }
                return false;
            } catch (Exception) {
                    throw;
            }
        }

        public async Task<bool> ComprobarContraseña(UserLoginDto userLogin) {
            try {
                var respuesta = await _signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, isPersistent: true, lockoutOnFailure: true);
                return respuesta.Succeeded;
            } catch (Exception) {
                throw;
            }
        }

        // Obtener Rol del usuario
        public async Task<IList<string>?> ObtenerRol(string userName) {
            try {

                // Obtenemos el usuario por el userName
                var user = await _userManager.FindByNameAsync(userName);
                IList<string>? role = null;

                if (user is not null) {
                    role = await _userManager.GetRolesAsync(user);
                } 

                return role;
            } catch (Exception) {
                 throw;
             }
        }
    }
}

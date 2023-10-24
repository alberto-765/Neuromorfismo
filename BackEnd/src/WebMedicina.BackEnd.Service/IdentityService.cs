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
        private readonly AdminDal _adminDal;

        public IdentityService(RoleManager<IdentityRole> roleManager, MedicoDal medicoDal, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AdminDal adminDal) {
            _roleManager = roleManager;
            _medicoDal = medicoDal;
            _userManager = userManager;
            _signInManager = signInManager;
            _adminDal = adminDal;
        }
        public async Task<MedicosModel?> ObtenerUsuarioYRol(string numHistoria) {
            try {
                MedicosModel? medicosModel = _medicoDal.ObtenerInfoUser(numHistoria);

                // Obtenemos el rol si se ha obtenido correctamente la info del usuario
                if (medicosModel is not null) {
                   var rol = await ObtenerRol(numHistoria);

                    if(rol is not null) {
                        medicosModel.Rol = rol;
                    }

                    if (string.IsNullOrEmpty(medicosModel.Rol)) {
                        medicosModel.Rol = "medico";
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
        public async Task<string?> ObtenerRol(string userName) {
            try {
                return await _adminDal.ObtenerRolUser(userName, await _adminDal.ObtenerUsuario(userName));
            } catch (Exception) {
                 throw;
            }
        }

        // LLamamos a BBDD y devolvemos si existe o no el userName
        public async Task<bool> ComprobarUserName(string userName) {
            try {
                return await _userManager.FindByNameAsync(userName) != null;
            } catch (Exception) {
                throw;
            }
        }

        // Actualizamos rol del usuario
        public async Task<bool> ActualizarRol(string userName, string nuevoRol) {
            try { 
                // Obtenemos el usuario y sus roles
                IdentityUser? usuario = await _adminDal.ObtenerUsuario(userName);
                IdentityResult rolActualizado = new();

                if (usuario is not null) {
                    string? rol = await _adminDal.ObtenerRolUser(userName, usuario);
                    // Eliminamos y volvemos a añadir el nuevo rol
                    if(rol != null) {
                        rolActualizado = await _userManager.RemoveFromRoleAsync(usuario, rol);
                        rolActualizado = await _userManager.AddToRoleAsync(usuario, nuevoRol);
                    }
                }

                return rolActualizado.Succeeded;
            } catch (Exception) {
                throw;
            }
        }
    }
}

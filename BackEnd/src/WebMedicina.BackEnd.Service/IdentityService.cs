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

        public async Task<MedicosModel?> ObtenerUsuarioYRolLogin(string userName) {
            try {
                MedicosModel? medicosModel = _medicoDal.ObtenerInfoUserLogin(userName);

                // Obtenemos el rol si se ha obtenido correctamente la info del usuario
                if (medicosModel != null && !string.IsNullOrEmpty(medicosModel.UserLogin)) {
                    var rol = await ObtenerRol(medicosModel.UserLogin);

                    if (string.IsNullOrEmpty(rol)) {
                        medicosModel.Rol = "medico";
                    } else {
                        medicosModel.Rol = rol;
                    }
                }

                return medicosModel;
            } catch (Exception) {
                throw;
            }
        }

        public async Task<MedicosModel?> ObtenerUsuarioYRol(int idMedico) {
            try {
                MedicosModel? medicosModel = _medicoDal.ObtenerInfoUser(idMedico);

                // Obtenemos el rol si se ha obtenido correctamente la info del usuario
                if (medicosModel != null && !string.IsNullOrEmpty(medicosModel.UserLogin)) {
                   var rol = await ObtenerRol(medicosModel.UserLogin);

                    if(string.IsNullOrEmpty(rol)) {
                        medicosModel.Rol = "medico";
                    } else {
                        medicosModel.Rol = rol;
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
                    return result.Succeeded;
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
                return await _adminDal.ObtenerRolUser(await _adminDal.ObtenerUsuarioIdentity(userName));
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
        public async Task<bool> ActualizarRol(string userLogin, string nuevoRol) {
            try { 
                // Obtenemos el usuario y sus roles
                IdentityUser? usuario = await _adminDal.ObtenerUsuarioIdentity(userLogin);
                IdentityResult rolActualizado = new();

                if (usuario is not null) {
                    string? rol = await _adminDal.ObtenerRolUser(usuario);
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

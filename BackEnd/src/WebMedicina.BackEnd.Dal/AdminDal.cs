using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMedicina.BackEnd.Dal {
    public  class AdminDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminDal(WebmedicinaContext context, IMapper mapper, ExcepcionDto excepcionDto, UserManager<IdentityUser> userManager) {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }


        public bool CrearNuevoMedico (UserRegistroDto nuevoMedico, string idUsuario) {
            try {
                MedicosModel medicosModel = _mapper.Map<MedicosModel>(nuevoMedico);
                medicosModel.NetuserId = idUsuario;

                _context.Medicos.Add(medicosModel);
                // La operación se realizó correctamente
                return true; 
            } catch (Exception) {
                return false;   
            }
        }
        public async Task<IdentityUser?> ObtenerUsuario (string userName) {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<string?> ObtenerRolUser(string userName, IdentityUser? user) {
            try {
  
                IList<string>? roles = null;

                if (user is not null) {
                    roles = await _userManager.GetRolesAsync(user);
                }

                return roles?.FirstOrDefault().ToString();
            } catch (Exception) {
                throw;
            }
        }
        public async Task<List<UserUploadDto>> ObtenerMedicos(Dictionary<string, string> filtros, UserInfoDto admin) {
            List<UserUploadDto> listaMedicos = new();

            // Validamos si hay que filtrar por rol o no
            if (string.IsNullOrEmpty(filtros["rol"])) {


                // Obtenemos los usuarios con los filtros seleccionados
                listaMedicos = (from u in _context.Medicos
                                                where (u.NumHistoria != admin.NumHistoria &&
                                                (!string.IsNullOrEmpty(filtros["busqueda"]) || (u.NumHistoria == filtros["busqueda"] || u.Nombre.StartsWith(filtros["busqueda"])
                               || u.Apellidos.Contains(filtros["busqueda"]) || u.Apellidos.StartsWith(filtros["busqueda"]))))
                               select _mapper.Map<UserUploadDto>(u)).ToList();

                // Mapeamos los medicos y obtenemos su rol
                foreach (UserUploadDto usuario in listaMedicos)
                {
                    var role = await ObtenerRolUser(usuario.NumHistoria, await ObtenerUsuario(usuario.NumHistoria));
                    if (role != null) {
                        usuario.Rol = role;
                    }
                }
            } else {
                    
                // Obtenemos todos los usuarios con el rol indicado y generamos una lista
                var usuariosConRol = await _userManager.GetUsersInRoleAsync(filtros["rol"]);
                IEnumerable<string> listaUserNames =  (from q in usuariosConRol where q.NormalizedUserName is not null select q.NormalizedUserName).ToArray();


                //Obtenemos los usuarios con los filtros seleccionados
                listaMedicos = (from u in _context.Medicos
                                where (u.NumHistoria != admin.NumHistoria && 
                    (listaUserNames.Contains(u.NumHistoria) || (string.IsNullOrEmpty(filtros["busqueda"]) || (u.NumHistoria == filtros["busqueda"] || u.Nombre.StartsWith(filtros["busqueda"])
                    || u.Apellidos.Contains(filtros["busqueda"]) || u.Apellidos.StartsWith(filtros["busqueda"])))))
                                select _mapper.Map<UserUploadDto>(u)).ToList();

                // Añadimos el rol a los usuarios
                listaMedicos.ForEach(m => m.Rol = filtros["rol"]);
            }

            return listaMedicos;
        }

        // Update del medico con el numHistoria especificado
        public async Task<bool> UpdateMedico (UserUploadDto medicoActualizado) {

            // Obtenemos el medico
            MedicosModel? medico = _context.Medicos.Find(medicoActualizado.NumHistoria);

            // Actualizamos las propiedades
            if(medico != null) {
                medico.Nombre = medicoActualizado.Nombre;
                medico.Apellidos = medicoActualizado.Apellidos;
                medico.FechaNac = medicoActualizado.FechaNac ?? medico.FechaNac;
                medico.Sexo = medicoActualizado.Sexo;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;
using WebMedicina.Shared.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebMedicina.BackEnd.Dal {
    public  class AdminDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminDal(WebmedicinaContext context, IMapper mapper, UserManager<IdentityUser> userManager) {
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
        public async Task<IdentityUser?> ObtenerUsuarioIdentity(string userName) {
            try {
                return await _userManager.FindByNameAsync(userName);
            } catch (Exception) {
                throw;
            }
        }

        // Obtener todos los roles de un usuario
        public async Task<string?> ObtenerRolUser(IdentityUser? user) {
            try {
 
                IList<string>? roles = null;

                if (user is not null) {
                    roles = await _userManager.GetRolesAsync(user);
                }

                return roles?.FirstOrDefault().ToString() ?? string.Empty;
            } catch (Exception) {
                throw;
            }
        }
        public async Task<List<UserUploadDto>> ObtenerMedicos(FiltradoTablaDefaultDto camposFiltrado, UserInfoDto admin) {
            try { 
                List<UserUploadDto> listaMedicos = new();
                int indice = 1;


                IQueryable<MedicosModel> query = _context.Medicos.Where(m => m.IdMedico != admin.IdMedico &&
                (string.IsNullOrWhiteSpace(camposFiltrado.SearchString) ||
                 ($"{m.Nombre} {m.Apellidos} {m.UserLogin ?? string.Empty} {m.Sexo} {m.FechaNac}")
                     .Contains(camposFiltrado.SearchString, StringComparison.OrdinalIgnoreCase)));



                // ----------------- Creamos expresion lambda para el ordenamiento --------------
                if (!string.IsNullOrWhiteSpace(camposFiltrado.SortLabel) && typeof(MedicosModel).GetProperty(camposFiltrado.SortLabel) != null) {

                    // Generamos el nombre del parametro de entrada Ej: x => x.
                    var parametro = Expression.Parameter(typeof(MedicosModel), "x");
                    // Generamos la propiedad del modelo que se va a seleccionar
                    var propiedad = Expression.Property(parametro, camposFiltrado.SortLabel);

                    // obtenemos el tipo de la propiedad
                    var tipoDePropiedad = ((MemberExpression)propiedad).Type;

                    // Generamos expresion lamda con el modelo de entrada (x) y con lo que devuelve. Se le pasa la propiedad que se quiere ordenar 
                    // y el nombre del parametro
                    Expression<Func<MedicosModel, object>> lambda = x => x;

                    if (tipoDePropiedad == typeof(DateTime) || tipoDePropiedad == typeof(DateTime?)) {
                        // Convertir DateTime a object de manera segura
                        var conversion = Expression.Convert(propiedad, typeof(object));
                        var typeAs = Expression.TypeAs(conversion, typeof(object));

                        lambda = Expression.Lambda<Func<MedicosModel, object>>(typeAs, parametro);
                    } else if (tipoDePropiedad == typeof(DateOnly) || tipoDePropiedad == typeof(DateOnly?)) {

                        // Convertir DateTime a object de manera segura
                        var conversion = Expression.Convert(propiedad, typeof(object));
                        var typeAs = Expression.TypeAs(conversion, typeof(object));
                        lambda = Expression.Lambda<Func<MedicosModel, object>>(typeAs, parametro);
                    } else {
                        lambda = Expression.Lambda<Func<MedicosModel, object>>(propiedad, parametro);
                    }


                    // Obtenemos los usuarios que no sea el propio admin y filtramos por el search
                    if (camposFiltrado.SortDirection == 1) {
                        query = query.OrderBy(lambda);
                    } else {
                        query = query.OrderByDescending(lambda);
                    }
                } 
         
                // ----------------- Creamos expresion lambda para el ordenamiento --------------

                // Filtramos la cantiadad de usuarios a obtener
                query = query.Skip(camposFiltrado.Page * camposFiltrado.PageSize).Take(camposFiltrado.PageSize);

                // Obtenemos los medicos
                listaMedicos = _mapper.Map<List<UserUploadDto>>(query.ToList());


                // Mapeamos los medicos y obtenemos su rol
                foreach (UserUploadDto usuario in listaMedicos) {
                    var role = await ObtenerRolUser(await ObtenerUsuarioIdentity(usuario.UserLogin));
                    if (role != null) {
                        usuario.Rol = role;
                    }
                    usuario.Indice = indice;
                    indice++;
                }

                return listaMedicos;
            } catch (Exception ex) {
                    throw;
            }
        }

        // Update del medico con el numHistoria especificado
        public async Task<bool> UpdateMedico (UserUploadDto medicoActualizado) {
            try {

                // Obtenemos el medico
                MedicosModel? medico = _context.Medicos.Find(medicoActualizado.IdMedico);

                // Actualizamos las propiedades
                if(medico != null) {
                    medico.Nombre = medicoActualizado.Nombre;
                    medico.Apellidos = medicoActualizado.Apellidos;
                    medico.FechaNac = medicoActualizado.FechaNac ?? medico.FechaNac;
                    medico.Sexo = medicoActualizado.Sexo;
                    return await _context.SaveChangesAsync() > 0;
                }
                return false;
            } catch (Exception) {
                throw;
            }
        }
    }
}

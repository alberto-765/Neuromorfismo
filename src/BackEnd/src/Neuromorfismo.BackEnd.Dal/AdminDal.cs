using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
using Neuromorfismo.Shared.Dto.Pacientes;
using Neuromorfismo.Shared.Dto.Usuarios;


namespace Neuromorfismo.BackEnd.Dal {
    public  class AdminDal {
        private readonly WebmedicinaContext _context;
        private readonly UserManager<UserModel> _userManager;

        public AdminDal(WebmedicinaContext context, UserManager<UserModel> userManager) {
            _context = context;
            _userManager = userManager;
        }


        public bool CrearNuevoMedico (UserRegistroDto nuevoMedico, string idUsuario) {
            try {
                MedicosModel medicosModel = nuevoMedico.ToModel();
                medicosModel.NetuserId = idUsuario;

                _context.Medicos.Add(medicosModel);
                // La operación se realizó correctamente
                return true; 
            } catch (Exception) {
                return false;   
            }
        }
        public async Task<UserModel?> ObtenerUsuarioIdentity(string userName) {
            return await _userManager.FindByNameAsync(userName);
        }

        // Obtener todos los roles de un usuario
        public async Task<string?> ObtenerRolUser(UserModel? user) {
            IList<string>? roles = null;

            if (user is not null) {
                roles = await _userManager.GetRolesAsync(user);
            }

            return roles?.FirstOrDefault()?.ToString();
        }

        /// <summary>
        /// Obtener todos los medicos filtrados para la pantalla de gestion de usuarios
        /// </summary>
        /// <param name="camposFiltrado"></param>
        /// <param name="admin"></param>
        /// <returns></returns>
        public async Task<List<UserUploadDto>> ObtenerMedicos(FiltradoTablaDefaultDto camposFiltrado, UserInfoDto admin) {
            List<UserUploadDto> listaMedicos = new();
            int indice = 1;


            IQueryable<MedicosModel> query = _context.Medicos.Where(m => m.IdMedico != admin.IdMedico);
                
                
            // Realizamos filtrado si se ha pasado alguna propiedad
            if (!string.IsNullOrWhiteSpace(camposFiltrado.SearchString)) {
                query = query.Where(m => ($"{m.Nombre} {m.Apellidos} {m.UserLogin ?? string.Empty} {m.Sexo} {m.FechaNac}")
                    .Contains(camposFiltrado.SearchString, StringComparison.OrdinalIgnoreCase));
            }


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
            if (camposFiltrado.PageSize > 0) {
                query = query.Skip(camposFiltrado.Page * camposFiltrado.PageSize).Take(camposFiltrado.PageSize);
            }



            // Obtenemos los medicos
            listaMedicos = query.Select(q => q.ToDto()).ToList();


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
        }

        // Update del medico con el numHistoria especificado
        public async Task<bool> UpdateMedico (UserUploadDto medicoActualizado) {
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
        }
    }
}

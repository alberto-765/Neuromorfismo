using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
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
        public async Task<IdentityUser?> ObtenerUsuarioIdentity(string userName) {
            try {
                return await _userManager.FindByNameAsync(userName);
            } catch (Exception) {
                throw;
            }
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
            try { 
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
                        var role = await ObtenerRolUser(usuario.NumHistoria, await ObtenerUsuarioIdentity(usuario.NumHistoria));
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
            } catch (Exception) {
                    throw;
            }
        }

        // Update del medico con el numHistoria especificado
        public async Task<bool> UpdateMedico (UserUploadDto medicoActualizado) {
            try {

                // Obtenemos el medico
                MedicosModel? medico = _context.Medicos.Find(medicoActualizado.NumHistoria);

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

        // [WEB V1] - Get EPILEPSIAS
        public List<EpilepsiasDto> GetEpilepsias() {
            try {
                return _mapper.Map<List<EpilepsiasDto>>(_context.Epilepsias.ToList());
            } catch (Exception) {  throw;  }
        }

        // [WEB V1] - Create EPILEPSIAS
        public async Task<bool> CrearEpilepsia(string nombre) {
            try {
                EpilepsiaModel nuevaEpilepsia = new() { 
                    Nombre = nombre
                };

                await _context.Epilepsias.AddAsync(nuevaEpilepsia);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Delete EPILEPSIAS
        public async Task<bool> DeleteEpilepsia(int idEpilepsia) {
            try {
                EpilepsiaModel? epilepsiaBorrada = await _context.Epilepsias.FindAsync(idEpilepsia);
                if (epilepsiaBorrada != null) {
                    _context.Epilepsias.Remove(epilepsiaBorrada);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Update EPILEPSIAS
        public async Task<(bool validacionEntry, bool filasModif)> UpdateEpilepsia(EpilepsiasDto epilepsia) {
            try {
                EpilepsiaModel ep = _context.Epilepsias.Find(epilepsia.IdEpilepsia);
                bool validacionEntry = false, filasModif = false;
                if (ep != null) {
                    
                    // Asignamos nuevo nombre
                   ep.Nombre = epilepsia.Nombre;

                    // Verificamos que el objeto haya sido modicado
                    var entry = _context.Entry(ep);
                    if(entry.State == EntityState.Modified) {
                        validacionEntry = true;
                        filasModif =  await _context.SaveChangesAsync() > 0;
                    }
                
                }
                return (validacionEntry, !filasModif);
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Get FARMACOS
        public List<FarmacosDto> GetFarmacos() {
            try {
                return _mapper.Map<List<FarmacosDto>>(_context.Farmacos.ToList());
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Create FARMACOS
        public async Task<bool> CrearFarmaco(string nombre) {
            try {
                FarmacosModel nuevoFarmaco = new() {
                    Nombre = nombre
                };

                await _context.Farmacos.AddAsync(nuevoFarmaco);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Delete FARMACOS
        public async Task<bool> DeleteFarmaco(int idFarmaco) {
            try {
                FarmacosModel? farmacoBorrado = await _context.Farmacos.FindAsync(idFarmaco);
                if (farmacoBorrado != null) {
                    _context.Farmacos.Remove(farmacoBorrado);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Update FARMACOS
        public async Task<(bool validacionEntry, bool filasModif)> UpdateFarnaco(FarmacosDto farmaco) {
            try {
                FarmacosModel far = _context.Farmacos.Find(farmaco.IdFarmaco);
                bool validacionEntry = false, filasModif = false;
                if (far != null) {

                    // Asignamos nuevo nombre
                    far.Nombre = farmaco.Nombre;

                    // Verificamos que el objeto haya sido modicado
                    var entry = _context.Entry(far);
                    if (entry.State == EntityState.Modified) {
                        validacionEntry = true;
                        filasModif = await _context.SaveChangesAsync() > 0;
                    }

                }
                return (validacionEntry, !filasModif);
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Get MUTACION
        public List<MutacionesDto> GetMutaciones() {
            try {
                return _mapper.Map<List<MutacionesDto>>(_context.Mutaciones.ToList());
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Create MUTACION
        public async Task<bool> CrearMutacion(string nombre) {
            try {
                MutacionesModel nuevaMutacion = new() {
                    Nombre = nombre
                };

                await _context.Mutaciones.AddAsync(nuevaMutacion);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Delete MUTACION
        public async Task<bool> DeleteMutacion(int idMutacion) {
            try {
                MutacionesModel? mutacionBorrada = await _context.Mutaciones.FindAsync(idMutacion);
                if (mutacionBorrada != null) {
                    _context.Mutaciones.Remove(mutacionBorrada);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // [WEB V1] - Update MUTACION
        public async Task<(bool validacionEntry, bool filasModif)> UpdateMutacion(MutacionesDto mutacion) {
            try {
                MutacionesModel mut = _context.Mutaciones.Find(mutacion.IdMutacion);
                bool validacionEntry = false, filasModif = false;
                if (mut != null) {

                    // Asignamos nuevo nombre
                    mut.Nombre = mutacion.Nombre;

                    // Verificamos que el objeto haya sido modicado
                    var entry = _context.Entry(mut);
                    if (entry.State == EntityState.Modified) {
                        validacionEntry = true;
                        filasModif = await _context.SaveChangesAsync() > 0;
                    }

                }
                return (validacionEntry, !filasModif);
            } catch (Exception) {
                throw;
            }
        }

    }
}

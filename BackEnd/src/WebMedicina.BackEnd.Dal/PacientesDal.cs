using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Usuarios;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;

namespace WebMedicina.BackEnd.Dal {
    public class PacientesDal {
        private readonly WebmedicinaContext _context;

        public PacientesDal(WebmedicinaContext context) {
            _context = context;
        }

        /// <summary>
        ///  Obtener todos los medicos con pacientes asignados
        /// </summary>
        /// <returns>Lista Medicos Pacientes</returns>
        public async Task<IEnumerable<UserInfoDto>> ObtenerAllMedicoPacientes() {
            try {
                IQueryable<MedicosModel>? query = null;
                query = from a in _context.Medicospacientes
                        join b in _context.Medicos on a.IdMedico equals b.IdMedico
                        select b;
                return await query.Distinct().Select(q => q.ToUserInfoDto()).ToListAsync();
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Validar si existe un paciente con el numero de historia
        /// </summary>
        /// <param name="numHistoria"></param>
        /// <returns>Bool</returns>
        public bool ExisteNumHistoria(string numHistoria) {
            try {
                return _context.Pacientes.Any(paciente => paciente.NumHistoria == numHistoria);
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        ///  Crear nuevo paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns>Bool, paciente creado o no</returns>
        public async Task<int> CrearPaciente(PacientesModel nuevoPaciente) {
            try {
                await _context.Pacientes.AddAsync(nuevoPaciente);
                await _context.SaveChangesAsync();

                return nuevoPaciente.IdPaciente;
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        ///  Crear nuevo paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns>Bool con paciente editado o no</returns>
        public async Task<bool> EditarPaciente(PacientesModel nuevoPaciente) {
            try {
                _context.Update(nuevoPaciente);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }


        /// <summary>
        ///  Crear nuevo paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns>Bool, paciente eliminado o no</returns>
        public async Task<bool> EliminarPaciente(int idPaciente) {
            try {
                PacientesModel? paciente = await _context.Pacientes.FindAsync(idPaciente);
                if (paciente != null) {
                    _context.Pacientes.Remove(paciente);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Obtener todos los pacientes para SuperAdmins y Admins
        /// </summary>
        /// <returns>Lista de todos los pacientes</returns>
        public List<CrearPacienteDto> GetAllPacientes() {
            try {
                return _context.Pacientes
                    .Include(q => q.IdEpilepsiaNavigation)
                    .Include(q => q.IdMutacionNavigation)
                    .Include(q => q.Medicospacientes)
                        .ThenInclude(q => q.IdMedicoNavigation)
                    .Select(q => q.ToDto())
                    .ToList();
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Obtener los pacientes de un unico medico
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Pacientes de un medico</returns>        
        public List<CrearPacienteDto> GetPacientesMed(UserInfoDto userInfo) {
            try {
                return _context.Pacientes
                    .Where(q => q.Medicospacientes.Any(medpac => medpac.IdMedPac == userInfo.IdMedico))
                    .Include(q => q.IdEpilepsiaNavigation)
                    .Include(q => q.IdMutacionNavigation)
                    .Include(q => q.Medicospacientes)
                        .ThenInclude(q => q.IdMedicoNavigation)
                    .Select(q => q.ToDto())
                    .ToList();
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Obtener un diccionario con el nombre y el id de cada medico
        /// </summary>
        /// <param name="idMedicos"></param>
        /// <returns>Diccionario IdMedico-Nombre</returns>
        public Dictionary<int, string> ObtenerNombresMed(HashSet<int> idMedicos) {
            try {
                Dictionary<int, string> listaNombresMed = _context.Medicos.Where(q => idMedicos.Contains(q.IdMedico)).ToDictionary(medico => medico.IdMedico, medico => medico.Nombre);
                return listaNombresMed;
            } catch (Exception) {
                throw;
            }
        }

        public async Task<bool> ValidarPermisosEdicYElim(int idMedico, int idPaciente) {
            try {
                return await _context.Medicospacientes.AnyAsync(q => q.IdPaciente == idPaciente && q.IdMedico == idMedico);
            } catch (Exception) {
                throw;
            }

        }

        /// <summary>
        /// Obtener datos de un paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>InfoPacienteDto de un paciente</returns>
        public async Task<CrearPacienteDto?> GetUnPaciente(int idPaciente) {
            try {
                PacientesModel? paciente =  await _context.Pacientes
                    .Include(q => q.IdEpilepsiaNavigation)
                    .Include(q => q.IdMutacionNavigation)
                    .Include(q => q.Medicospacientes)
                        .ThenInclude(q => q.IdMedicoNavigation)
                    .SingleOrDefaultAsync(q => q.IdPaciente == idPaciente);
                return paciente?.ToDto();
            } catch (Exception) {
                throw;
            }
        }
    }
}

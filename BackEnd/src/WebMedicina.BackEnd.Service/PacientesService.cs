using AutoMapper;
using System.Security.Claims;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Tipos;
using WebMedicina.Shared.Dto.Usuarios;

namespace WebMedicina.BackEnd.Service
{
    public class PacientesService : IPacientesService {
        private readonly PacientesDal _pacientesDal;
        private readonly EpilepsiasDal _epilepsiasDal;
        private readonly FarmacosDal _farmacosDal;
        private readonly MutacionesDal _mutacionesDal;
        private readonly WebmedicinaContext _context;

        public PacientesService(PacientesDal pacientesDal, EpilepsiasDal epilepsiasDal, FarmacosDal farmacosDal, MutacionesDal mutacionesDal, WebmedicinaContext context) {
            _pacientesDal = pacientesDal;
            _epilepsiasDal = epilepsiasDal;
            _farmacosDal = farmacosDal;
            _mutacionesDal = mutacionesDal;
            _context = context;
        }

        public bool ValidarNumHistoria(string numHistoria) {
            try {
               return _pacientesDal.ExisteNumHistoria(numHistoria);
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Obtener listado con todos los medicos que tienen pacientes relacionados
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<UserInfoDto>> GetAllMed() {
            try {
                // Obtenemos todos los medicos y sus pacientes
                return await _pacientesDal.ObtenerAllMedicoPacientes();
            } catch (Exception) {
                throw;
            }
        }

        // Crear nuevo paciente
        public async Task<int> CrearPaciente(CrearPacienteDto nuevoPaciente, int idMedico) {
            using var transaccion = _context.Database.BeginTransaction();
            try {
                PacientesModel modeloPaciente = nuevoPaciente.ToModel();
                int idPaciente = 0;

                // Mapeamos el medico creador
                modeloPaciente.MedicoCreador = idMedico;
                modeloPaciente.MedicoUltMod = idMedico;

                idPaciente = await _pacientesDal.CrearPaciente(modeloPaciente);
                await transaccion.CommitAsync();
                return idPaciente;
            } catch (Exception) {
                await transaccion.RollbackAsync();
                throw;
            }
        }

        // Obtener todos los pacientes con sus datos
        public List<CrearPacienteDto> ObtenerPacientes (ClaimsPrincipal? user) {
            try {
                // Get de todos los pacientes
                List<CrearPacienteDto> listaPacientes = new();

                // Obtenemos todos los pacientes o solamente los del medico 
                if (user is not null) {
                    UserInfoDto userInfo = user.ToUserInfoDto();

                    // Validamos que el id del medico sea valido
                    if (user.IsInRole("superAdmin") || user.IsInRole("superAdmin")) {
                        listaPacientes = _pacientesDal.GetAllPacientes();
                    } else {
                        if (userInfo.IdMedico != 0) {
                            listaPacientes = _pacientesDal.GetPacientesMed(userInfo);
                        }
                    }
                }

                return listaPacientes;
            } catch (Exception) {
                throw;
            }
        }

        // Obtener todas las epilepsias disponibles 
        public List<EpilepsiasDto> ObtenerEpilepsias() {
            try {
                return _epilepsiasDal.GetEpilepsias();
            } catch (Exception) { throw; }
        }

        // Obtener todas las mutaciones disponibles 
        public List<MutacionesDto> ObtenerMutaciones() {
            try {
                return _mutacionesDal.GetMutaciones();
            } catch (Exception) { throw; }
        }

        /// <summary>
        /// Obtener todas las farmacos disponibles 
        /// </summary>
        /// <returns>Listado con todos los farmacos</returns>
        public List<FarmacosDto> ObtenerFarmacos() {
            try {
                return _farmacosDal.GetFarmacos();
            } catch (Exception) { throw; }
        }

        /// <summary>
        /// Editar paciente 
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <param name="idMedico"></param>
        /// <returns>Si el paciente ha sido editado</returns>
        public async Task<bool> EditarPaciente(CrearPacienteDto nuevoPaciente, int idMedico) {
            try {
                using var transaccion = _context.Database.BeginTransaction();
                try {

                    // Mapeamos el modelo y editamos el paciente
                    PacientesModel modeloPaciente = nuevoPaciente.ToModel();

                    // Asignamos al medico como el ultimo que ha modificado el paciente
                    modeloPaciente.MedicoUltMod = idMedico;
                    bool pacienteEditado = await _pacientesDal.EditarPaciente(modeloPaciente);

                    // Si no ha habido ningun error finalizamos la transaccion
                    await transaccion.CommitAsync();
                    return pacienteEditado;
                } catch (Exception) {
                        await transaccion.RollbackAsync();
                        throw;
                    }
            } catch (Exception) {
                throw;
            }
        }

        /// <summary>
        /// Eliminar paciente 
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <param name="idMedico"></param>
        /// <returns>Si el paciente ha sido eliminado o no</returns>
        public async Task<bool> EliminarPaciente(int idPaciente) {
            try {
                using var transaccion = _context.Database.BeginTransaction();
                try {
                    // Eliminamos el paciente
                    bool pacienteEliminado = await _pacientesDal.EliminarPaciente(idPaciente);

                    // Si no ha habido ningun error finalizamos la transaccion
                    await transaccion.CommitAsync();
                    return pacienteEliminado;
                } catch (Exception) {
                    await transaccion.RollbackAsync();
                    throw;
                }
            } catch (Exception) {

                throw;
            }
        }

        /// <summary>
        /// Validar los permisos del medico para un paciente
        /// </summary>
        /// <param name="user"></param>
        /// <param name="idPaciente"></param>
        /// <returns>Si el usuario tiene permisos para editar o eliminar el paciente</returns>
        public async Task<bool> ValidarPermisosEdicYElim(ClaimsPrincipal? user, int idPaciente) {
            try {
                bool tienePermisos = false;

                if (user != null) {
                    UserInfoDto userInfo = user.ToUserInfoDto();

                    // Permisos sin limites para SuperAdmin y Admin
                    if (user.IsInRole("superAdmin") || user.IsInRole("admin")) {
                        tienePermisos = true;
                    } else {
                        // Validamos tabla MedicosPacientes 
                        tienePermisos = await _pacientesDal.ValidarPermisosEdicYElim(userInfo.IdMedico, idPaciente);
                    }
                }

                return tienePermisos;
            } catch (Exception) {
                throw;
            }
        }
        
        public async Task<CrearPacienteDto?> GetUnPaciente(int idPaciente) {
            try {
                return await _pacientesDal.GetUnPaciente(idPaciente);
            } catch (Exception) {
                throw;
            }
        }
    }
}

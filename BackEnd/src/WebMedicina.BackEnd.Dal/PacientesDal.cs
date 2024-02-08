using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto.Pacientes;
using WebMedicina.Shared.Dto.Usuarios;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using DocumentFormat.OpenXml.Bibliography;

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
            IQueryable<MedicosModel>? query = null;
            query = from a in _context.Medicospacientes
                    join b in _context.Medicos on a.IdMedico equals b.IdMedico
                    select b;
            return await query.AsNoTracking()
                .Distinct()
                .Select(q => q.ToUserInfoDto()).ToListAsync();
        }

        /// <summary>
        /// Validar si existe un paciente con el numero de historia
        /// </summary>
        /// <param name="numHistoria"></param>
        /// <returns>Bool</returns>
        public bool ExisteNumHistoria(string numHistoria) {
            return _context.Pacientes.Any(paciente => paciente.NumHistoria == numHistoria);
        }

        /// <summary>
        ///  Crear nuevo paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns>Bool, paciente creado o no</returns>
        public async Task<int> CrearPaciente(PacientesModel nuevoPaciente) {
            await _context.Pacientes.AddAsync(nuevoPaciente);
            await _context.SaveChangesAsync();

            return nuevoPaciente.IdPaciente;
        }

        /// <summary>
        ///  Crear nuevo paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns>Bool con paciente editado o no</returns>
        public async Task<bool> EditarPaciente(PacientesModel nuevoPaciente) {
            PacientesModel? paciente = await _context.Pacientes.FindAsync(nuevoPaciente.IdPaciente);
            if (paciente is not null && !nuevoPaciente.Equals(paciente)) {
                _context.Entry(paciente).CurrentValues.SetValues(nuevoPaciente);
            }
            return await _context.SaveChangesAsync() > 0;
        }


        /// <summary>
        ///  Crear nuevo paciente
        /// </summary>
        /// <param name="nuevoPaciente"></param>
        /// <returns>Bool, paciente eliminado o no</returns>
        public async Task<bool> EliminarPaciente(int idPaciente) {
            PacientesModel? paciente = await _context.Pacientes.FindAsync(idPaciente);
            if (paciente != null) {
                _context.Pacientes.Remove(paciente);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// Obtener todos los pacientes para SuperAdmins y Admins
        /// </summary>
        /// <returns>Lista de todos los pacientes</returns>
        public List<CrearPacienteDto> GetAllPacientes() {
            return _context.Pacientes
                .AsNoTracking()
                .Include(q => q.IdEpilepsiaNavigation)
                .Include(q => q.IdMutacionNavigation)
                .Include(q => q.Medicospacientes)
                    .ThenInclude(q => q.IdMedicoNavigation)
                .Select(q => q.ToDto())
                .ToList();
        }

        /// <summary>
        /// Obtener los pacientes de un unico medico
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns>Pacientes de un medico</returns>        
        public List<CrearPacienteDto> GetPacientesMed(UserInfoDto userInfo) {
            return _context.Pacientes
                .AsNoTracking()
                .Where(q => q.Medicospacientes.Any(medpac => medpac.IdMedPac == userInfo.IdMedico))
                .Include(q => q.IdEpilepsiaNavigation)
                .Include(q => q.IdMutacionNavigation)
                .Include(q => q.Medicospacientes)
                    .ThenInclude(q => q.IdMedicoNavigation)
                .Select(q => q.ToDto())
                .ToList();
        }

        public async Task<bool> ValidarPermisosEdicYElim(int idMedico, int idPaciente) {
            return await _context.Medicospacientes.AnyAsync(q => q.IdPaciente == idPaciente && q.IdMedico == idMedico);
        }

        /// <summary>
        /// Obtener datos de un paciente
        /// </summary>
        /// <param name="idPaciente"></param>
        /// <returns>InfoPacienteDto de un paciente</returns>
        public async Task<CrearPacienteDto?> GetUnPaciente(int idPaciente) {
            PacientesModel? paciente =  await _context.Pacientes
                .AsNoTracking()
                .Include(q => q.IdEpilepsiaNavigation)
                .Include(q => q.IdMutacionNavigation)
                .Include(q => q.Medicospacientes)
                    .ThenInclude(q => q.IdMedicoNavigation)
                .SingleOrDefaultAsync(q => q.IdPaciente == idPaciente);
            return paciente?.ToDto();
        }

        public string? GetNumHistoria(int idPaciente) {
            return _context.Pacientes
                .AsNoTracking()
                .Where(p => p.IdPaciente == idPaciente)
                .Select(p => p.NumHistoria)
                .FirstOrDefault();
        }
    }
}

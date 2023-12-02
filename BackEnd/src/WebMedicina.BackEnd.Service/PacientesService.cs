using AutoMapper;
using System.ComponentModel.DataAnnotations;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Service {
    public class PacientesService : IPacientesService {
        private readonly PacientesDal _pacientesDal;
        private readonly EpilepsiasDal _epilepsiasDal;
        private readonly FarmacosDal _farmacosDal;
        private readonly MutacionesDal _mutacionesDal;
        private readonly IMapper _mapper;
        private readonly WebmedicinaContext _context;

        public PacientesService(PacientesDal pacientesDal, EpilepsiasDal epilepsiasDal, FarmacosDal farmacosDal, MutacionesDal mutacionesDal, 
            IMapper mapper, WebmedicinaContext context) {
            _pacientesDal = pacientesDal;
            _epilepsiasDal = epilepsiasDal;
            _farmacosDal = farmacosDal;
            _mutacionesDal = mutacionesDal;
            _mapper = mapper;
            _context = context;
        }

        public bool ValidarNumHistoria(string numHistoria) {
            try {
               return _pacientesDal.ValidarNumHistoria(numHistoria);
            } catch (Exception) {
                throw;
            }
        }

        public async Task<IEnumerable<int>> GetAllMed() {
            try {
                // Obtenemos todos los medicos y sus pacientes
                IEnumerable<MedicosPacientesDto> listaMedicosPac =  await _pacientesDal.ObtenerAllMedicoPacientes();

                // Obtenemos los id de los medicos de la lista
                if(listaMedicosPac != null && listaMedicosPac.Any()) {
                    return listaMedicosPac.Select(q => q.Medico).ToList();
                }

                return Enumerable.Empty<int>();
            } catch (Exception) {
                throw;
            }
        }

        // Crear nuevo paciente
        public async Task<bool> CrearPaciente(CrearPacienteDto nuevoPaciente, int idMedico) {
            using (var transaccion = _context.Database.BeginTransaction()) {
                try {
                    PacientesModel modeloPaciente = _mapper.Map<PacientesModel>(nuevoPaciente);

                    // Mapeamos el medico creador
                    modeloPaciente.MedicoCreador = idMedico;
                    modeloPaciente.MedicoUltMod = idMedico;
                    bool pacienteCreado = await _pacientesDal.CrearPaciente(modeloPaciente);

                    // Si no ha habido ningun error finalizamos la transaccion
                    await transaccion.CommitAsync();
                    return pacienteCreado;
                } catch (Exception) {
                    await transaccion.RollbackAsync();
                    throw;
                }
            }
        }

        // Obtener todos los pacientes con sus datos
        public IEnumerable<PacienteDto> ObtenerPacientes () {
            try {
                // Get de todos los pacientes
                IEnumerable<PacientesModel>? listaPacientesBD = _pacientesDal.GetAllPacientes();

                if (!listaPacientesBD.Any()) {
                    return Enumerable.Empty<PacienteDto>();
                }

                // Mapeamos id de medicos para obtener sus nombres
                HashSet<int> hastIdMedicos = listaPacientesBD.Select(q=> q.MedicoCreador).ToHashSet();
                hastIdMedicos.Union(listaPacientesBD.Select(q => q.MedicoUltMod).ToHashSet());
                Dictionary<int, string> listaNombresMed = _pacientesDal.ObtenerNombresMed(hastIdMedicos);

              
                // Creamos listado de pacientes
                List<PacienteDto> listaPacientes = new();

                // Mapeamos y añadimos nombres de medicos a cada paciente
                foreach (PacientesModel modelo in listaPacientesBD) {
                    PacienteDto nuevoPaciente = new() {
                        IdPaciente = modelo.IdPaciente,
                        NumHistoria = modelo.NumHistoria,
                        FechaNac = modelo.FechaNac, 
                        Sexo =  modelo.Sexo,
                        Talla = modelo.Talla,
                        FechaDiagnostico = modelo.FechaDiagnostico,
                        FechaFractalidad = modelo.FechaFractalidad,
                        //Farmaco = modelo.Farmaco,
                        TipoEpilepsia = modelo.IdEpilepsia,
                        TipoMutacion = 

                    };
                }
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

        // Obtener todas las farmacos disponibles 
        public List<FarmacosDto> ObtenerFarmacos() {
            try {
                return _farmacosDal.GetFarmacos();
            } catch (Exception) { throw; }
        }
    }
}

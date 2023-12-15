using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.Dto;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;
using System.IdentityModel.Tokens.Jwt;


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
        public List<PacienteDto> ObtenerPacientes (ClaimsPrincipal user) {
            try {
                // Get de todos los pacientes
                List<InfoPacienteDto>? listaInfoPacientes = null;

                // Obtenemos todos los pacientes o solamente los del medico 
                if(user is not null) {
                    UserInfoDto userInfo = _mapper.Map<UserInfoDto>(user);

                    // Validamos que el id del medico sea valido
                    if (user.IsInRole("superAdmin") || user.IsInRole("superAdmin")) {
                        listaInfoPacientes = _pacientesDal.GetAllPacientes();
                    } else {
                        if (userInfo.IdMedico != 0) {
                            listaInfoPacientes = _pacientesDal.GetPacientesMed(userInfo);
                        }
                    }
                }

                // Creamos listado de pacientes
                List<PacienteDto> listaPacientes = new();

                // Mapeamos y añadimos nombres de medicos a cada paciente
                if(listaInfoPacientes is not null) {
                    foreach (InfoPacienteDto infoPaciente in listaInfoPacientes) {
                        PacienteDto nuevoPaciente = new() {
                            IdPaciente = infoPaciente.Paciente.IdPaciente,
                            NumHistoria = infoPaciente.Paciente.NumHistoria,
                            FechaNac = infoPaciente.Paciente.FechaNac,
                            Sexo = infoPaciente.Paciente.Sexo,
                            Talla = infoPaciente.Paciente.Talla,
                            FechaDiagnostico = infoPaciente.Paciente.FechaDiagnostico,
                            FechaFractalidad = infoPaciente.Paciente.FechaFractalidad,
                            Farmaco = infoPaciente.Paciente.Farmaco,
                            IdEpilepsia = infoPaciente.Paciente.IdEpilepsia,
                            NombreEpilepsia = infoPaciente.NombreEpilepsia,
                            NombreMutacion = infoPaciente.NombreMutacion,
                            IdMutacion = infoPaciente.Paciente.IdMutacion,
                            EnfermRaras = (infoPaciente.Paciente.EnfermRaras == "S" ? "Sí" : (infoPaciente.Paciente.EnfermRaras == "N" ? "No" : string.Empty)),
                            DescripEnferRaras = (infoPaciente.Paciente.EnfermRaras == "S" ? infoPaciente.Paciente.DescripEnferRaras : string.Empty  ),
                            FechaCreac = infoPaciente.Paciente.FechaCreac,
                            FechaUltMod = infoPaciente.Paciente.FechaUltMod,
                            MedicoCreador = infoPaciente.Paciente.MedicoCreadorNavigation?.UserLogin ?? string.Empty,
                            MedicoUltMod = infoPaciente.Paciente.MedicoUltModNavigation?.UserLogin ?? string.Empty,
                            MedicosPacientes = infoPaciente.MedicosPacientes.ToDictionary(x => x.IdMedico, x => x.UserLogin)
                        };
                        listaPacientes.Add(nuevoPaciente);
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

        // Obtener todas las farmacos disponibles 
        public List<FarmacosDto> ObtenerFarmacos() {
            try {
                return _farmacosDal.GetFarmacos();
            } catch (Exception) { throw; }
        }
    }
}

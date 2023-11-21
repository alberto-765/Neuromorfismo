using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Dal;
using WebMedicina.BackEnd.ServicesDependencies;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Service {
    public class PacientesService : IPacientesService {
        private readonly PacientesDal pacientesDal;
        private readonly EpilepsiasDal _epilepsiasDal;
        private readonly FarmacosDal _farmacosDal;
        private readonly MutacionesDal _mutacionesDal;
        public PacientesService(PacientesDal pacientesDal, EpilepsiasDal epilepsiasDal, FarmacosDal farmacosDal, MutacionesDal mutacionesDal) {
            this.pacientesDal = pacientesDal;
            _epilepsiasDal = epilepsiasDal;
            _farmacosDal = farmacosDal;
            _mutacionesDal = mutacionesDal;
        }

        public async Task<IEnumerable<int>> GetAllMed() {
            try {
                // Obtenemos todos los medicos y sus pacientes
                IEnumerable<MedicosPacientesDto> listaMedicosPac =  await pacientesDal.ObtenerMedicoPacientes();

                // Obtenemos los id de los medicos de la lista
                if(listaMedicosPac != null && listaMedicosPac.Any()) {
                    return listaMedicosPac.Select(q => q.Medico).ToList();
                }

                return Enumerable.Empty<int>();
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

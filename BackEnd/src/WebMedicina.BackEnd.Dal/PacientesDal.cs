using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Dal {
    public class PacientesDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;

        public PacientesDal(WebmedicinaContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        // Obtener todos los medicos con pacientes asignados
        public async Task<List<MedicosPacientesDto>>  ObtenerAllMedicoPacientes() {
            try {
                return _mapper.Map<List<MedicosPacientesDto>>(await _context.Medicospacientes.ToListAsync());
            } catch (Exception) {
                throw;
            }
        }

        // Validar numero de historia de paciente
        public bool ValidarNumHistoria(string numHistoria) {
            try {
                return _context.Pacientes.Any(paciente => paciente.NumHistoria == numHistoria);
            } catch (Exception) {
                throw;
            }
        }

        // Crear nuevo paciente
        public async Task<bool> CrearPaciente(PacientesModel nuevoPaciente) {
            try {
                await _context.Pacientes.AddAsync(nuevoPaciente);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // Obtener todos los pacientes
        public IEnumerable<PacientesModel> GetAllPacientes() {
            try {
                return _context.Pacientes.ToList();
            } catch (Exception) {
                throw;
            }
        }

        // Obtener nombres medicos relacionado con cada paciente
        public Dictionary<int, string> ObtenerNombresMed(HashSet<int> idMedicos) {
            try {
                Dictionary<int, string> listaNombresMed = _context.Medicos.Where(q => idMedicos.Contains(q.IdMedico)).ToDictionary(medico => medico.IdMedico, medico => medico.Nombre);
                return listaNombresMed;
            } catch (Exception) {
                throw;
            }
        }
    }
}

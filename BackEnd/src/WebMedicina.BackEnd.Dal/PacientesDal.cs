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

        public async Task<List<MedicosPacientesDto>>  ObtenerMedicoPacientes() {
            try {
                return _mapper.Map<List<MedicosPacientesDto>>(await _context.Medicospacientes.ToListAsync());
            } catch (Exception) {
                throw;
            }
        }
    }
}

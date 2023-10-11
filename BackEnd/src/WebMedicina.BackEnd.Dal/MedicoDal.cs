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
    public class MedicoDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;
        private readonly ExcepcionDto _excepcionDto;

        // Contructor con inyeccion de dependencias
        public MedicoDal(WebmedicinaContext context, IMapper mapper, ExcepcionDto excepcionDto) {
            _context = context;
            _mapper = mapper;
            _excepcionDto = excepcionDto;

        }

        // Obtenemos los datos de un medico
        public UserInfoDto ObtenerInfoUser(string numHistoria) {
            try {
                MedicosModel modeloMedico = new();
                modeloMedico = _context.Medicos.FirstOrDefault(u => u.NumHistoria == numHistoria);
                return _mapper.Map<UserInfoDto>(modeloMedico);
            } catch (Exception ex) {
                throw;
            }
        }
    }
}

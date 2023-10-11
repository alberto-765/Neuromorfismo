using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Dal {
    public  class AdminDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;
        private readonly ExcepcionDto _excepcionDto;


        public AdminDal(WebmedicinaContext context, IMapper mapper, ExcepcionDto excepcionDto) {
            _context = context;
            _mapper = mapper;
            _excepcionDto = excepcionDto;

        }


        public bool CrearNuevoMedico (UserRegistroDto nuevoMedico, String idUsuario) {
            try {
                MedicosModel medicosModel = _mapper.Map<MedicosModel>(nuevoMedico);
                medicosModel.NetuserId = idUsuario;

                _context.Medicos.Add(medicosModel);
                _context.SaveChanges();
                  return true; // La operación se realizó correctamente
            } catch (Exception ex) {
                _excepcionDto.ConstruirPintarExcepcion(ex);
                return false;   
            }
        }

    }
}

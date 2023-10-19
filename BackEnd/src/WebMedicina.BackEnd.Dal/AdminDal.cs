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

        public AdminDal(WebmedicinaContext context, IMapper mapper, ExcepcionDto excepcionDto) {
            _context = context;
            _mapper = mapper;
        }


        public bool CrearNuevoMedico (UserRegistroDto nuevoMedico, String idUsuario) {
            try {
                MedicosModel medicosModel = _mapper.Map<MedicosModel>(nuevoMedico);
                medicosModel.NetuserId = idUsuario;

                _context.Medicos.Add(medicosModel);
                  return true; // La operación se realizó correctamente
            } catch (Exception) {
                return false;   
            }
        }

        public async Task<List<UserInfoDto>> ObtenerMedicos(Dictionary<string, string> filtros) {

            List<UserInfoDto> listaMedicos = new();
            // Obtenemos los usuarios con los filtros seleccionados
            listaMedicos = _context.Medicos.Where(u =>
                (string.IsNullOrEmpty(filtros["busqueda"]) || (u.NumHistoria == filtros["busqueda"] || u.Nombre.StartsWith(filtros["busqueda"]) || u.Apellidos.Contains(filtros["busqueda"]) || u.Apellidos.StartsWith(filtros["busqueda"]))
                || (string.IsNullOrEmpty(filtros["rol"]) || u.Rol == filtros["rol"])))
                .Select(_mapper.Map<UserInfoDto>)
                .ToList();

            return listaMedicos;
        }

    }
}

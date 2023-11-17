using AutoMapper;
using WebMedicina.BackEnd.Model;

namespace WebMedicina.BackEnd.Dal {
    public class MedicoDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;

        // Contructor con inyeccion de dependencias
        public MedicoDal(WebmedicinaContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        // Obtenemos los datos de un medico
        public MedicosModel ObtenerInfoUser(string numHistoria) {
            try {
                return _context.Medicos.Find(numHistoria);
            } catch (Exception ex) {
                throw;
            }
        }
    }
}

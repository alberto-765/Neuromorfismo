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

        // Obtenemos los datos de un medico filtrando por id
        public MedicosModel ObtenerInfoUser(int idMedico) {
            try {
                return _context.Medicos.Find(idMedico);
            } catch (Exception) {
                throw;
            }
        }
        
        // Obtenemos los datos de un medico filtrando por username
        public MedicosModel ObtenerInfoUserLogin(string userName) {
            try {
                return _context.Medicos.First(q => q.UserLogin == userName);
            } catch (Exception) {
                throw;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Model;

namespace WebMedicina.BackEnd.Dal {
    public class MedicoDal {
        private readonly WebmedicinaContext _context;

        // Contructor con inyeccion de dependencias
        public MedicoDal(WebmedicinaContext context) {
            _context = context;
        }

        // Obtenemos los datos de un medico filtrando por id
        public MedicosModel? ObtenerInfoUser(int idMedico) {
            return _context.Medicos.Find(idMedico);
        }
        
        // Obtenemos los datos de un medico filtrando por username
        public MedicosModel ObtenerInfoUserLogin(string userName) {
            var medico =  _context.Medicos.AsNoTracking().First(q => q.UserLogin == userName);
            return medico;
        }
    }
}

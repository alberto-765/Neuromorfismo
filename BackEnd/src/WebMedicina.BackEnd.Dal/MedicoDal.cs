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
            try {
                return _context.Medicos.Find(idMedico);
            } catch (Exception) {
                throw;
            }
        }
        
        // Obtenemos los datos de un medico filtrando por username
        public MedicosModel ObtenerInfoUserLogin(string userName) {
            try {
                var medico =  _context.Medicos.First(q => q.UserLogin == userName);
                return medico;
            } catch (Exception) {
                throw;
            }
        }
    }
}

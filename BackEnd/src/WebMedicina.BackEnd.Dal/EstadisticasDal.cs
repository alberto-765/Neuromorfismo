using WebMedicina.BackEnd.Model;

namespace WebMedicina.BackEnd.Dal {
    public class EstadisticasDal {
        private readonly WebmedicinaContext _context;


        public EstadisticasDal(WebmedicinaContext context) {
            _context = context;
        }


        public async Task<Dictionary<string, uint>> GetTotalPaciente() {
            var hola = _context.Pacientes.GroupBy(q => q.FechaCreac);
            await Task.Delay(1000);
            return new();

        }
    }
}

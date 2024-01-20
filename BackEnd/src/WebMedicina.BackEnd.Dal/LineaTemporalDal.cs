using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Security.Cryptography;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.LineaTemporal;

namespace WebMedicina.BackEnd.Dal
{
    public class LineaTemporalDal {
        private readonly WebmedicinaContext _context;

        public LineaTemporalDal(WebmedicinaContext context) {
            _context = context;
        }

        // Obtener diccionario ordenado de las etapas, si no hay devolvemos uno vacio
        public ImmutableSortedDictionary<int, EtapaLTDto> GetEtapas() {
            return _context.EtapaLTModel.ToImmutableSortedDictionary(q => q.Id, q=> q.ToDto()) ;
        }

        // Obtener las evoluciones de un paciente
        public async Task<List<EvolucionLTDto>> GetEvoluciones(int idPaciente) {
            return await (from q in _context.EvolucionLTModels where q.IdPaciente == idPaciente select q.ToDto()).ToListAsync();
        }

        // Obtener una evolucion de un paciente
        public async Task<EvolucionLTModel?> GetEvolucion(int idEvolucion, int idPaciente) {
            return await _context.EvolucionLTModels.SingleOrDefaultAsync(q => q.Id == idEvolucion && q.IdPaciente == idPaciente);
        }

        // Obtener la evolucion de un paciente
        public async Task<bool> ActualizarEvolucion(EvolucionLTModel evolucion) {
            _context.EvolucionLTModels.Update(evolucion);
            return await _context.SaveChangesAsync() > 0;
        }


        // Insetar evolucion 
        public async Task<bool> InsertarEvolucion(EvolucionLTModel evolucion) {
            await _context.AddAsync(evolucion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}

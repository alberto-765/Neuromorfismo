using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;
using System.Diagnostics;
using WebMedicina.BackEnd.Model;

namespace WebMedicina.BackEnd.Dal {
    public class EstadisticasDal {
        private readonly WebmedicinaContext _context;


        public EstadisticasDal(WebmedicinaContext context) {
            _context = context;
        }

        /// <summary>
        /// Get diccionario con fecha-cantidad
        /// </summary>
        /// <returns></returns>
        public ImmutableSortedDictionary<DateTime, uint> GetTotalPacientes() {
            return _context.Pacientes.AsNoTracking()
                .GroupBy(q => q.FechaCreac).Select(q => new { Fecha = new DateTime(q.Key.Year, q.Key.Month, 1), Cantidad = (uint)q.Count()})
                .ToImmutableSortedDictionary(q => q.Fecha, q=> q.Cantidad);
        }

        /// <summary>
        /// Get diccionario con fecha-cantidad
        /// </summary>
        /// <returns></returns>
        public ImmutableSortedDictionary<DateTime, uint> GetTotalMedicos() {
            return _context.Medicos.AsNoTracking()
                .GroupBy(q => q.FechaCreac).Select(q => new { Fecha = new DateTime(q.Key.Year, q.Key.Month, 1), Cantidad = (uint)q.Count() })
                .ToImmutableSortedDictionary(q => q.Fecha, q => q.Cantidad);
        }

        /// <summary>
        /// Obtenemos el numero de pacientes que tiene cada etapa
        /// </summary>
        /// <returns></returns>
        public ImmutableDictionary<string, uint> GetTotalEtapas() {

            // Agrupamos por paciente, obtenemos la etapa mas alta y agrupamos por etapa, y por ultimo creamos diccionario con el nombre de la etapa
            // y la cantidad
            return _context.EvolucionLT.AsNoTracking()
                .GroupBy(q => q.IdPaciente).Select(q => q.Max(e => e.IdEtapa))
                .GroupBy(q => q).Select(q => new {
                    _context.EtapaLT.Single(e => e.Id == q.Key).Titulo,
                    Cantidad = (uint)q.Count()
                })
                .ToImmutableDictionary(q => q.Titulo, q => q.Cantidad);
        }

    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
using Neuromorfismo.Shared.Dto.Tipos;

namespace Neuromorfismo.BackEnd.Dal {
    public class MutacionesDal {

        private readonly NeuromorfismoContext _context;

        public MutacionesDal(NeuromorfismoContext context) {
            _context = context;
        }

        //  Get MUTACION
        public List<MutacionesDto> GetMutaciones() {
            List<MutacionesDto> listaMutaciones = _context.Mutaciones.AsNoTracking().Select(q => q.ToDto()).ToList();
            if(listaMutaciones.Count > 0) {
                for (int i = 0; i < listaMutaciones.Count; i++){
                    listaMutaciones[i].Indice = i+1;
                }
            }
            return listaMutaciones;
        }

        //  Create MUTACION
        public async Task<bool> CrearMutacion(string nombre) {
            MutacionesModel nuevaMutacion = new() {
                Nombre = nombre,
                FechaUltMod = DateTime.Today,
                FechaCreac = DateTime.Today
            };

            await _context.Mutaciones.AddAsync(nuevaMutacion);
            return await _context.SaveChangesAsync() > 0;
        }

        //  Delete MUTACION
        public async Task<bool> DeleteMutacion(int idMutacion) {
            MutacionesModel? mutacionBorrada = await _context.Mutaciones.FindAsync(idMutacion);
            if (mutacionBorrada != null) {
                _context.Mutaciones.Remove(mutacionBorrada);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        //  Update MUTACION
        public async Task<(bool validacionEntry, bool filasModif)> UpdateMutacion(MutacionesDto mutacion) {
            MutacionesModel? mut = await _context.Mutaciones.FindAsync(mutacion.IdMutacion);
            bool validacionEntry = false, filasModif = false;
            if (mut != null) {

                // Asignamos nuevo nombre
                mut.Nombre = mutacion.Nombre;

                // Verificamos que el objeto haya sido modicado
                if (_context.Entry(mut).State == EntityState.Modified) {
                    validacionEntry = true;
                    filasModif = await _context.SaveChangesAsync() > 0;
                }

            }
            return (validacionEntry, filasModif);
        }
    }
}

using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebMedicina.BackEnd.Model;
using WebMedicina.BackEnd.ServicesDependencies.Mappers;
using WebMedicina.Shared.Dto.Tipos;

namespace WebMedicina.BackEnd.Dal {
    public class MutacionesDal {

        private readonly WebmedicinaContext _context;

        public MutacionesDal(WebmedicinaContext context) {
            _context = context;
        }

        //  Get MUTACION
        public List<MutacionesDto> GetMutaciones() {
            try {
                List<MutacionesDto> listaMutaciones = _context.Mutaciones.Select(q => q.ToDto()).ToList();
                if(listaMutaciones.Count > 0) {
                    for (int i = 0; i < listaMutaciones.Count; i++){
                        listaMutaciones[i].Indice = i+1;
                    }
                }
                return listaMutaciones;
            } catch (Exception) { throw; }
        }

        //  Create MUTACION
        public async Task<bool> CrearMutacion(string nombre) {
            try {
                MutacionesModel nuevaMutacion = new() {
                    Nombre = nombre
                };

                await _context.Mutaciones.AddAsync(nuevaMutacion);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        //  Delete MUTACION
        public async Task<bool> DeleteMutacion(int idMutacion) {
            try {
                MutacionesModel? mutacionBorrada = await _context.Mutaciones.FindAsync(idMutacion);
                if (mutacionBorrada != null) {
                    _context.Mutaciones.Remove(mutacionBorrada);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        //  Update MUTACION
        public async Task<(bool validacionEntry, bool filasModif)> UpdateMutacion(MutacionesDto mutacion) {
            try {
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
            } catch (Exception) {
                throw;
            }
        }
    }
}

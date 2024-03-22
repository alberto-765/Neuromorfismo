using Microsoft.EntityFrameworkCore;
using Neuromorfismo.BackEnd.Model;
using Neuromorfismo.BackEnd.ServicesDependencies.Mappers;
using Neuromorfismo.Shared.Dto.Tipos;

namespace Neuromorfismo.BackEnd.Dal {
    public class FarmacosDal {
        private readonly WebmedicinaContext _context;

        public FarmacosDal(WebmedicinaContext context) {
            _context = context;
        }
        //  Get FARMACOS
        public List<FarmacosDto> GetFarmacos() {
            return _context.Farmacos.AsNoTracking().Select(q => q.ToDto()).ToList();
        }

        //  Create FARMACOS
        public async Task<bool> CrearFarmaco(string nombre) {
            FarmacosModel nuevoFarmaco = new() {
                Nombre = nombre,
                FechaUltMod = DateTime.Today,
                FechaCreac = DateTime.Today
            };

            await _context.Farmacos.AddAsync(nuevoFarmaco);
            return await _context.SaveChangesAsync() > 0;
        }

        //  Delete FARMACOS
        public async Task<bool> DeleteFarmaco(int idFarmaco) {
            FarmacosModel? farmacoBorrado = await _context.Farmacos.FindAsync(idFarmaco);
            if (farmacoBorrado != null) {
                _context.Farmacos.Remove(farmacoBorrado);
            }
            return await _context.SaveChangesAsync() > 0;
        }

        //  Update FARMACOS
        public async Task<(bool validacionEntry, bool filasModif)> UpdateFarnaco(FarmacosDto farmaco) {
            FarmacosModel? far = await _context.Farmacos.FindAsync(farmaco.IdFarmaco);
            bool validacionEntry = false, filasModif = false;
            if (far != null) {

                // Asignamos nuevo nombre
                far.Nombre = farmaco.Nombre;

                // Verificamos que el objeto haya sido modicado
                if (_context.Entry(far).State == EntityState.Modified) {
                    validacionEntry = true;
                    filasModif = await _context.SaveChangesAsync() > 0;
                }

            }
            return (validacionEntry, filasModif);
        }

    }
}

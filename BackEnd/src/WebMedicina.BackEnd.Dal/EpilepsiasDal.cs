using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Dal {
    public class EpilepsiasDal {
        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;

        public EpilepsiasDal(WebmedicinaContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        // Get EPILEPSIAS
        public List<EpilepsiasDto> GetEpilepsias() {
            try {
                return _mapper.Map<List<EpilepsiasDto>>(_context.Epilepsias.ToList());
            } catch (Exception) { throw; }
        }

        // [WEB V1] - Create EPILEPSIAS
        public async Task<bool> CrearEpilepsia(string nombre) {
            try {
                EpilepsiaModel nuevaEpilepsia = new() {
                    Nombre = nombre
                };

                await _context.Epilepsias.AddAsync(nuevaEpilepsia);
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // Delete EPILEPSIAS
        public async Task<bool> DeleteEpilepsia(int idEpilepsia) {
            try {
                EpilepsiaModel? epilepsiaBorrada = await _context.Epilepsias.FindAsync(idEpilepsia);
                if (epilepsiaBorrada != null) {
                    _context.Epilepsias.Remove(epilepsiaBorrada);
                }
                return await _context.SaveChangesAsync() > 0;
            } catch (Exception) {
                throw;
            }
        }

        // Update EPILEPSIAS
        public async Task<(bool validacionEntry, bool filasModif)> UpdateEpilepsia(EpilepsiasDto epilepsia) {
            try {
                EpilepsiaModel ep = _context.Epilepsias.Find(epilepsia.IdEpilepsia);
                bool validacionEntry = false, filasModif = false;
                if (ep != null) {

                    // Asignamos nuevo nombre
                    ep.Nombre = epilepsia.Nombre;

                    // Verificamos que el objeto haya sido modicado
                    if (_context.Entry(ep).State == EntityState.Modified) {
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

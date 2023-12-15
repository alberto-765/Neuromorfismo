using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebMedicina.BackEnd.Model;
using WebMedicina.Shared.Dto;

namespace WebMedicina.BackEnd.Dal {
    public class MutacionesDal {

        private readonly WebmedicinaContext _context;
        private readonly IMapper _mapper;

        public MutacionesDal(WebmedicinaContext context, IMapper mapper) {
            _context = context;
            _mapper = mapper;
        }

        //  Get MUTACION
        public List<MutacionesDto> GetMutaciones() {
            try {
                return _mapper.Map<List<MutacionesDto>>(_context.Mutaciones.ToList());
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
                MutacionesModel mut = _context.Mutaciones.Find(mutacion.IdMutacion);
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

using Microsoft.EntityFrameworkCore;
using Neuromorfismo.BackEnd.Model;

namespace Neuromorfismo.BackEnd.Dal {
    public class TokensDal {
        private readonly WebmedicinaContext _context;

        public TokensDal(WebmedicinaContext context) {
            _context = context;
        }

        public bool AddRefreshToken(UserRefreshTokens user) {
            _context.UserRefreshToken.Add(user);
            return _context.SaveChanges() > 0;
        }

        // Eliminamos todos los tokens que puedan existir de un usuario
        public bool DeleteRefreshTokens(int idMedico, string refreshToken = "") {

            // Si refreshToken no es un string vacio añadir el firstordefault que valide x.RefreshToken == refreshToken
            IQueryable<UserRefreshTokens> query = _context.UserRefreshToken;

            if (!string.IsNullOrWhiteSpace(refreshToken)) {
                query.Where(x => x.RefreshToken == refreshToken);
            }

            // Obtener todos los tokens que cumplan la vali
            List<UserRefreshTokens> tokens = query.Where(x => x.IdMedico == idMedico).ToList();
            if (tokens.Any()) {
                _context.UserRefreshToken.RemoveRange(tokens);
            }
            return _context.SaveChanges() > 0;
        }

        public UserRefreshTokens? GetRefreshToken(int idMedico, string refreshToken) {
            return _context.UserRefreshToken.AsNoTracking().FirstOrDefault(x => x.IdMedico == idMedico && x.RefreshToken == refreshToken);
        }
    }
}

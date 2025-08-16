

using Microsoft.EntityFrameworkCore;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext;

namespace Sistema_Citas.Intrafestructura.Sistema_Citas.Repository
{
    public class UsuarioRepository : GeneryRepository<Usuario>, IUsuarioRepository
    {
        private readonly DBContext _context;
        readonly DbSet<Usuario> _dbSet;
        public UsuarioRepository(DBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = context.Set<Usuario>();
        }

        public async Task<Usuario> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                {
                    throw new ArgumentException("El correo electrónico no puede ser nulo o vacío.", nameof(email));
                }
                var usuario = await _context.Set<Usuario>()
                                            .AsNoTracking()
                                            .FirstOrDefaultAsync(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
                if (usuario == null)
                {
                    throw new KeyNotFoundException($"No se encontró un usuario con el correo electrónico: {email}");
                }
                return usuario;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the user by email.", ex);
            }

        }

        public async Task<Usuario?> GetByEmailyPasswordAsync(string email, string contrasenaHash)
        {
            try
            {

                var emailLower = email.ToLowerInvariant();

                return await _context.Set<Usuario>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Email.ToLower() == emailLower
                                              && u.ContrasenaHash == contrasenaHash);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por email y contraseña", ex);
            }

        }
    }
}

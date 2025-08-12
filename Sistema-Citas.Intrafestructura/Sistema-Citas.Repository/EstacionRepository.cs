

using Microsoft.EntityFrameworkCore;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext;

namespace Sistema_Citas.Intrafestructura.Sistema_Citas.Repository
{
    public class EstacionRepository : GeneryRepository<Estacion>, IEstacionRepository
    {
        private readonly DBContext _context;
        public EstacionRepository(DBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Estacion>> GetEstacionesByNombreAsync(string nombre)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new ArgumentException("El nombre de la estación no puede ser nulo o vacío.", nameof(nombre));
                }
                return await _context.Set<Estacion>()
                    .Where(e => e.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving estaciones by nombre.", ex);

            }
        }
    }
}

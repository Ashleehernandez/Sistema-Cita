

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
            if(string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre de la estación no puede ser nulo o vacío.", nameof(nombre));
            }
            try
            {
               var estacion = await _context.Estacion.ToListAsync();

                return estacion.Where(estacion => estacion.Nombre != null && estacion.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase)).ToList();


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving estaciones by nombre.", ex);

            }
        }
    }
}

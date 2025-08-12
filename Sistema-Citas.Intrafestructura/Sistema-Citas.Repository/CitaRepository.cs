using Microsoft.EntityFrameworkCore;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;
using Sistema_Citas.Intrafestructura.Sistema_Citas_DBContext;

namespace Sistema_Citas.Intrafestructura.Sistema_Citas.Repository
{
    public class CitaRepository : GeneryRepository<Cita>, ICitasRepository
    {
        private readonly DBContext _context;
        public CitaRepository(DBContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Cita>> GetCitasByEstacionIdAsync(int estacionId)
        {
            try
            {
                return await _context.Set<Cita>()
                    .Where(c => c.EstacionId == estacionId)
                    .ToListAsync();


            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving citas by estacion ID.", ex);
            }
        }

        public async Task<IEnumerable<Cita>> GetCitasByEstadoAsync(Estado estado)
        {
            try
            {
                return await _context.Set<Cita>()
                    .Where(c => c.Estado == estado)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving citas by estado.", ex);
            }

        }

        public async Task<IEnumerable<Cita>> GetCitasByFechaAsync(DateTime fecha)
        {
            try
            {
                return await _context.Set<Cita>()
                .Where(c => c.FechaInicio.Date == fecha.Date)
                .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving citas by fecha.", ex);

            }
        }

        public async Task<IEnumerable<Cita>> GetCitasByUsuarioIdAsync(int usuarioId)
        {
            try
            {
                return await _context.Set<Cita>()
                    .Where(c => c.UsuarioId == usuarioId)
                    .ToListAsync();

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving citas by usuario ID.", ex);
            }
        }
    }
  
}

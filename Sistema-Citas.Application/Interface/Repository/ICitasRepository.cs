using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;

namespace Sistema_Citas.Application.Interface.Repository
{
    public interface ICitasRepository : IGeneryRepository<Cita>
    {
        Task<IEnumerable<Cita>> GetCitasByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Cita>> GetCitasByEstacionIdAsync(int estacionId);
        Task<IEnumerable<Cita>> GetCitasByFechaAsync(DateTime fecha);
        Task<IEnumerable<Cita>> GetCitasByEstadoAsync(Estado estado);
        
    }
}

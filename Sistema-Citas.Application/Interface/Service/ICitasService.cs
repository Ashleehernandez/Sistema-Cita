

using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;

namespace Sistema_Citas.Application.Interface.Service
{
    public interface ICitasService : IGeneryService<Cita>
    {
        Task<IEnumerable<Cita>> GetCitasByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Cita>> GetCitasByEstacionIdAsync(int estacionId);
        Task<IEnumerable<Cita>> GetCitasByFechaAsync(DateTime fecha);
        Task<IEnumerable<Cita>> GetCitasByEstadoAsync(Estado estado);
        Task<Cita> ConfirmarCitaAsync(int citaId);
        Task<Cita> RechazarCitaAsync(int citaId);
        Task<Cita> CancelarCitaAsync(int citaId);
    }
}

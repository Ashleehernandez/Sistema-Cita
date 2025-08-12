using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;

namespace Sistema_Citas.Application.Service
{
    public class CitasService : GeneryService<Cita>, ICitasService
    {
       
        private readonly ICitasRepository citasRepository;

        public CitasService(ICitasRepository CitasRepositor) : base(CitasRepositor)
        {
            citasRepository = CitasRepositor ?? throw new ArgumentNullException(nameof(CitasRepositor));

        }

        public async Task<Cita> CancelarCitaAsync(int citaId)
        {
            try
            {
                var cita = await citasRepository.GetByIdAsync(citaId);
                if (cita == null)
                {
                    throw new Exception("La cita no existe en la base de datos.");
                }
                // Cambiar el estado de la cita a Cancelada
                cita.Estado = Estado.Rechazado;
                // Actualizar la cita en la base de datos
                await citasRepository.UpdateAsyncc(cita);
                return cita;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while canceling the appointment.", ex);

            }
        }

        public async Task<Cita> ConfirmarCitaAsync(int citaId)
        {
            try
            {
                var cita = await citasRepository.GetByIdAsync(citaId);
                if (cita == null)
                {
                    throw new Exception("La cita no existe en la base de datos.");
                }
                // Cambiar el estado de la cita a Confirmada
                cita.Estado = Estado.Confirmado;
                // Actualizar la cita en la base de datos
                await citasRepository.UpdateAsyncc(cita);
                return cita;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while confirming the appointment.", ex);
            }
        }

        public async Task<IEnumerable<Cita>> GetCitasByEstacionIdAsync(int estacionId)
        {
            try
            {
                return await citasRepository.GetCitasByEstacionIdAsync(estacionId);

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
                return await citasRepository.GetCitasByEstadoAsync(estado);
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
                return await citasRepository.GetCitasByFechaAsync(fecha);
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
                return await citasRepository.GetCitasByUsuarioIdAsync(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving citas by usuario ID.", ex);
            }

        }

        public async Task<Cita> RechazarCitaAsync(int citaId)
        {
            try
            {
                var cita = await citasRepository.GetByIdAsync(citaId);
                if (cita == null)
                {
                    throw new Exception("La cita no existe en la base de datos.");
                }
                // Cambiar el estado de la cita a Rechazada
                cita.Estado = Estado.Rechazado;
                // Actualizar la cita en la base de datos
                await citasRepository.UpdateAsyncc(cita);
                return cita;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while rejecting the appointment.", ex);
            }
        }
    }
}

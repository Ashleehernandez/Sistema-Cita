using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.Application.Service
{
    public class EstacionService : GeneryService<Estacion>, IEstacionService
    {

        private readonly IEstacionRepository _estacionRepository;

        // CORREGIDO: Constructor simplificado
        public EstacionService(IEstacionRepository estacionRepository) : base(estacionRepository)
        {
            _estacionRepository = estacionRepository ?? throw new ArgumentNullException(nameof(estacionRepository));
        }


        public async Task<IEnumerable<Estacion>> GetEstacionesByNombreAsync(string nombre)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre))
                {
                    throw new ArgumentException("El nombre de la estación no puede ser nulo o vacío.", nameof(nombre));
                }
               return await _estacionRepository.GetEstacionesByNombreAsync(nombre);

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving estaciones by nombre.", ex);

            }
        }
    }
}

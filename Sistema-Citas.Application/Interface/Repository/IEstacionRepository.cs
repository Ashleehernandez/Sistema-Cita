

using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.Application.Interface.Repository
{
    public interface IEstacionRepository : IGeneryRepository<Estacion>
    {
        Task<IEnumerable<Estacion>> GetEstacionesByNombreAsync(string nombre);
      
    }
}

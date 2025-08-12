

using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Domain.Entity;
using Sistema_Citas.Domain.Enum;

namespace Sistema_Citas.Application.Interface.Service
{
    public interface IEstacionService : IGeneryService<Estacion>
    {
        Task<IEnumerable<Estacion>> GetEstacionesByNombreAsync(string nombre);
    }
}

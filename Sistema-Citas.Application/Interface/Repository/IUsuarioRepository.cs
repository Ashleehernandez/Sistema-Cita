using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.Application.Interface.Repository
{
    public interface IUsuarioRepository : IGeneryRepository<Usuario>
    {
        Task<Usuario> GetByEmailAsync(string email);
    }
}

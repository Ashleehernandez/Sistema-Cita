

using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.Application.Interface.Service
{
    public  interface IUsuarioService : IGeneryService<Usuario>
    {
        Task<Usuario> GetByEmailAsync(string email);
    }
}

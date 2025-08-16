

using Sistema_Citas.Application.Interface.Genery;
using Sistema_Citas.Application.Interface.Repository;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.Application.Service
{
    public class UsuarioService : GeneryService<Usuario> , IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        
        public UsuarioService(IUsuarioRepository usuarioRepositor) : base(usuarioRepositor)
        {
            _usuarioRepository = usuarioRepositor ?? throw new ArgumentNullException(nameof(usuarioRepositor));

        }
        public async Task<Usuario> GetByEmailAsync(string email)
        {
            try
            {
                return await _usuarioRepository.GetByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the user by email.", ex);
            }
        }

        public  async Task<Usuario?> GetByEmailyPasswordAsync(string Email, string ContrasenaHash)
        {
            try
            {
                return await _usuarioRepository.GetByEmailyPasswordAsync(Email, ContrasenaHash);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario por email y contraseña", ex);
            }

        }
    }
  
    
}

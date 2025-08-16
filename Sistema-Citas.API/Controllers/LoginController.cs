using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Domain.DTo.UsuarioDto;

namespace Sistema_Citas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public LoginController(IUsuarioService usuarioService, IMapper mapper)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper), "The service must implement IMapper.");
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            try
            {
                var usuario = await _usuarioService.GetByEmailyPasswordAsync(loginDto.Email, loginDto.ContrasenaHash);
                if (usuario == null)
                {
                    return Unauthorized("Credenciales inválidas.");
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}

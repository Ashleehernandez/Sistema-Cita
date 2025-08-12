using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Domain.DTo.UsuarioDto;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IMapper _mapper;
        public UsuarioController(IUsuarioService usuarioService , IMapper mapper)
        {
            _usuarioService = usuarioService ?? throw new ArgumentNullException(nameof(usuarioService));
            _mapper = mapper as IMapper 
                      ?? throw new ArgumentNullException(nameof(mapper), "The service must implement IMapper.");
        }

        [HttpGet("Usuario")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync();
                return Ok(_mapper.Map<IEnumerable<GetUsuarioDto>>(usuarios));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Usuario/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound($"Usuario with ID {id} not found.");
                }
                return Ok(_mapper.Map<GetUsuarioDto>(usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost("Usuario")]
        public async Task<IActionResult> Create([FromBody] CreateUsuarioDto createUsuarioDto)
        {
            if (createUsuarioDto == null)
            {
                return BadRequest("Usuario data is null.");
            }
            try
            {
                var usuario = _mapper.Map<Usuario>(createUsuarioDto);
                await _usuarioService.AddAsync(usuario);
                return CreatedAtAction(nameof(GetById), new { id = usuario.UsuarioId }, _mapper.Map<GetUsuarioDto>(usuario));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("Usuario/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateUsuarioDto updateUsuarioDto)
        {
            if (updateUsuarioDto == null)
            {
                return BadRequest("Usuario data is null.");
            }
            try
            {
                var existingUsuario = await _usuarioService.GetByIdAsync(id);
                if (existingUsuario == null)
                {
                    return NotFound($"Usuario with ID {id} not found.");
                }
                var usuario = _mapper.Map<Usuario>(updateUsuarioDto);
                usuario.UsuarioId = id; // Ensure the ID is set correctly
                await _usuarioService.UpdateAsync(usuario);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("Usuario/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var usuario = await _usuarioService.GetByIdAsync(id);
                if (usuario == null)
                {
                    return NotFound($"Usuario with ID {id} not found.");
                }
                await _usuarioService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}
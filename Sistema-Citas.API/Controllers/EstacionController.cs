using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Sistema_Citas.Application.Interface.Service;
using Sistema_Citas.Domain.DTo.EstacionDto;
using Sistema_Citas.Domain.Entity;

namespace Sistema_Citas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionController : ControllerBase // Cambiar a ControllerBase para APIs
    {
        private readonly IEstacionService _estacionService; // Usar _ como en UsuarioController
        private readonly IMapper _mapper;

        public EstacionController(IEstacionService estacionService, IMapper mapper)
        {
            // CORREGIDO: Igual que en UsuarioController
            _estacionService = estacionService ?? throw new ArgumentNullException(nameof(estacionService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); // Quitar el "as IMapper"
        }

        [HttpGet("GetByNombre/{nombre}")]
        public async Task<IActionResult> GetByNombre(string nombre)
        {
            try
            {
                var estaciones = await _estacionService.GetEstacionesByNombreAsync(nombre);
                if (estaciones == null || !estaciones.Any())
                {
                    return NotFound($"No se encontraron estaciones con el nombre: {nombre}");
                }
                return Ok(estaciones);
            }
            catch (ArgumentException argEx)
            {
                return BadRequest(argEx.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateEstacionDto createEstacionDto)
        {
            if (createEstacionDto == null)
            {
                return BadRequest("Estacion data is null.");
            }
            try
            {
                var estacionEntity = _mapper.Map<Estacion>(createEstacionDto);
                await _estacionService.AddAsync(estacionEntity);
                var estacionToReturn = _mapper.Map<GetEstacionDto>(estacionEntity);
                return CreatedAtAction(nameof(GetByNombre), new { nombre = estacionToReturn.Nombre }, estacionToReturn);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Estacion")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var estaciones = await _estacionService.GetAllAsync(); // Usar _estacionService
                return Ok(_mapper.Map<IEnumerable<GetEstacionDto>>(estaciones));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Estacion/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var estacion = await _estacionService.GetByIdAsync(id);
                if (estacion == null)
                {
                    return NotFound($"Estacion with ID {id} not found.");
                }
                return Ok(_mapper.Map<GetEstacionDto>(estacion));
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateEstacionDto updateEstacionDto)
        {
            if(updateEstacionDto == null)
            {
                return BadRequest("Los datos de la estación están vacíos");
            }
            
           
            try
            {
                var ExistingEstacion = await _estacionService.GetByIdAsync(id);
                if(ExistingEstacion == null)
                {
                    return NotFound($"No se encontró la estación con ID {id}");
                }
                _mapper.Map(updateEstacionDto, ExistingEstacion);
                ExistingEstacion.UpdateTime ??= DateTime.Now;
                await _estacionService.UpdateAsync(id, ExistingEstacion);
                return NoContent();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var estacion = await _estacionService.GetByIdAsync(id);
                if (estacion == null)
                {
                    return NotFound($"Estacion with ID {id} not found.");
                }
                await _estacionService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
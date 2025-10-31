using Microsoft.AspNetCore.Mvc;
using UniverseBuilder.Api.DTOs;
using UniverseBuilder.Core.Models;
using UniverseBuilder.Core.Services;

namespace UniverseBuilder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniverseController : ControllerBase
    {
        private readonly UniverseService _service;

        public UniverseController(UniverseService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<UniverseDto>>> GetAll()
        {
            var universes = await _service.GetAllUniversesAsync();
            var dtos = universes.Select(u => new UniverseDto
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                CreatedDate = u.CreatedDate,
                ModifiedDate = u.ModifiedDate
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UniverseDto>> GetById(Guid id)
        {
            var universe = await _service.GetUniverseByIdAsync(id);
            if (universe == null)
            {
                return NotFound(new { message = $"Universe with ID {id} not found." });
            }

            var dto = new UniverseDto
            {
                Id = universe.Id,
                Name = universe.Name,
                Description = universe.Description,
                CreatedDate = universe.CreatedDate,
                ModifiedDate = universe.ModifiedDate
            };

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult<UniverseDto>> Create([FromBody] CreateUniverseDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var universe = new Universe
            {
                Name = createDto.Name,
                Description = createDto.Description
            };

            try
            {
                await _service.CreateUniverseAsync(universe);

                var dto = new UniverseDto
                {
                    Id = universe.Id,
                    Name = universe.Name,
                    Description = universe.Description,
                    CreatedDate = universe.CreatedDate,
                    ModifiedDate = universe.ModifiedDate
                };

                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateUniverseDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUniverse = await _service.GetUniverseByIdAsync(id);
            if (existingUniverse == null)
            {
                return NotFound(new { message = $"Universe with ID {id} not found." });
            }

            existingUniverse.Name = updateDto.Name;
            existingUniverse.Description = updateDto.Description;

            try
            {
                await _service.UpdateUniverseAsync(existingUniverse);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var existingUniverse = await _service.GetUniverseByIdAsync(id);
            if (existingUniverse == null)
            {
                return NotFound(new { message = $"Universe with ID {id} not found." });
            }

            await _service.DeleteUniverseAsync(id);
            return NoContent();
        }
    }
}
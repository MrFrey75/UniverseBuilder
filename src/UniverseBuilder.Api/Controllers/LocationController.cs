using Microsoft.AspNetCore.Mvc;
using UniverseBuilder.Api.DTOs;
using UniverseBuilder.Core.Models;
using UniverseBuilder.Core.Services;

namespace UniverseBuilder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : ControllerBase
    {
        private readonly LocationService _service;

        public LocationController(LocationService service)
        {
            _service = service;
        }

        [HttpGet("universe/{universeId}")]
        public async Task<ActionResult<List<LocationDto>>> GetAllByUniverse(Guid universeId)
        {
            try
            {
                var locations = await _service.GetAllLocationsAsync(universeId);
                var dtos = locations.Select(MapToDto).ToList();
                return Ok(dtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("universe/{universeId}/roots")]
        public async Task<ActionResult<List<LocationDto>>> GetRoots(Guid universeId)
        {
            try
            {
                var locations = await _service.GetRootLocationsAsync(universeId);
                var dtos = locations.Select(MapToDto).ToList();
                return Ok(dtos);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetById(Guid id)
        {
            var location = await _service.GetLocationByIdAsync(id);
            if (location == null)
            {
                return NotFound(new { message = $"Location with ID {id} not found." });
            }

            return Ok(MapToDto(location));
        }

        [HttpGet("{id}/children")]
        public async Task<ActionResult<List<LocationDto>>> GetChildren(Guid id)
        {
            var children = await _service.GetChildrenAsync(id);
            var dtos = children.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}/ancestors")]
        public async Task<ActionResult<List<LocationDto>>> GetAncestors(Guid id)
        {
            var ancestors = await _service.GetAncestorsAsync(id);
            var dtos = ancestors.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}/descendants")]
        public async Task<ActionResult<List<LocationDto>>> GetDescendants(Guid id)
        {
            var descendants = await _service.GetDescendantsAsync(id);
            var dtos = descendants.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}/siblings")]
        public async Task<ActionResult<List<LocationDto>>> GetSiblings(Guid id)
        {
            var siblings = await _service.GetSiblingsAsync(id);
            var dtos = siblings.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}/path")]
        public async Task<ActionResult<List<LocationDto>>> GetPath(Guid id)
        {
            var path = await _service.GetLocationPathAsync(id);
            var dtos = path.Select(MapToDto).ToList();
            return Ok(dtos);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDto>> Create([FromBody] CreateLocationDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var location = MapToLocation(createDto);
                await _service.CreateLocationAsync(location);

                var dto = MapToDto(location);
                return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateLocationDto updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingLocation = await _service.GetLocationByIdAsync(id);
            if (existingLocation == null)
            {
                return NotFound(new { message = $"Location with ID {id} not found." });
            }

            try
            {
                UpdateLocationFromDto(existingLocation, updateDto);
                await _service.UpdateLocationAsync(existingLocation);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}/move")]
        public async Task<ActionResult> Move(Guid id, [FromBody] MoveLocationDto moveDto)
        {
            try
            {
                await _service.MoveLocationAsync(id, moveDto.NewParentId);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                await _service.DeleteLocationAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        private static LocationDto MapToDto(Location location)
        {
            return new LocationDto
            {
                Id = location.Id,
                UniverseId = location.UniverseId,
                ParentLocationId = location.ParentLocationId,
                Name = location.Name,
                Type = location.Type,
                Description = location.Description,
                Coordinates = location.Coordinates != null ? new LocationCoordinatesDto
                {
                    Latitude = location.Coordinates.Latitude,
                    Longitude = location.Coordinates.Longitude,
                    Altitude = location.Coordinates.Altitude
                } : null,
                Climate = location.Climate,
                Population = location.Population,
                Area = location.Area,
                CustomProperties = location.CustomProperties,
                Tags = location.Tags,
                CreatedDate = location.CreatedDate,
                ModifiedDate = location.ModifiedDate
            };
        }

        private static Location MapToLocation(CreateLocationDto dto)
        {
            return new Location
            {
                UniverseId = dto.UniverseId,
                ParentLocationId = dto.ParentLocationId,
                Name = dto.Name,
                Type = dto.Type,
                Description = dto.Description,
                Coordinates = dto.Coordinates != null ? new LocationCoordinates
                {
                    Latitude = dto.Coordinates.Latitude,
                    Longitude = dto.Coordinates.Longitude,
                    Altitude = dto.Coordinates.Altitude
                } : null,
                Climate = dto.Climate,
                Population = dto.Population,
                Area = dto.Area,
                CustomProperties = dto.CustomProperties,
                Tags = dto.Tags
            };
        }

        private static void UpdateLocationFromDto(Location location, UpdateLocationDto dto)
        {
            location.ParentLocationId = dto.ParentLocationId;
            location.Name = dto.Name;
            location.Type = dto.Type;
            location.Description = dto.Description;
            location.Coordinates = dto.Coordinates != null ? new LocationCoordinates
            {
                Latitude = dto.Coordinates.Latitude,
                Longitude = dto.Coordinates.Longitude,
                Altitude = dto.Coordinates.Altitude
            } : null;
            location.Climate = dto.Climate;
            location.Population = dto.Population;
            location.Area = dto.Area;
            location.CustomProperties = dto.CustomProperties;
            location.Tags = dto.Tags;
        }
    }

    public class MoveLocationDto
    {
        public Guid? NewParentId { get; set; }
    }
}

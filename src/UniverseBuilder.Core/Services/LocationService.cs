using UniverseBuilder.Core.Models;
using UniverseBuilder.Core.Repositories;

namespace UniverseBuilder.Core.Services
{
    public class LocationService
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IUniverseRepository _universeRepository;

        public LocationService(ILocationRepository locationRepository, IUniverseRepository universeRepository)
        {
            _locationRepository = locationRepository;
            _universeRepository = universeRepository;
        }

        public async Task<List<Location>> GetAllLocationsAsync(Guid universeId)
        {
            await ValidateUniverseExistsAsync(universeId);
            return await _locationRepository.GetAllAsync(universeId);
        }

        public async Task<Location?> GetLocationByIdAsync(Guid id)
        {
            return await _locationRepository.GetByIdAsync(id);
        }

        public async Task<List<Location>> GetRootLocationsAsync(Guid universeId)
        {
            await ValidateUniverseExistsAsync(universeId);
            return await _locationRepository.GetRootLocationsAsync(universeId);
        }

        public async Task<List<Location>> GetChildrenAsync(Guid parentId)
        {
            return await _locationRepository.GetChildrenAsync(parentId);
        }

        public async Task<List<Location>> GetAncestorsAsync(Guid locationId)
        {
            return await _locationRepository.GetAncestorsAsync(locationId);
        }

        public async Task<List<Location>> GetDescendantsAsync(Guid locationId)
        {
            return await _locationRepository.GetDescendantsAsync(locationId);
        }

        public async Task<List<Location>> GetSiblingsAsync(Guid locationId)
        {
            return await _locationRepository.GetSiblingsAsync(locationId);
        }

        public async Task<List<Location>> GetLocationPathAsync(Guid locationId)
        {
            var path = new List<Location>();
            var location = await _locationRepository.GetByIdAsync(locationId);
            
            if (location == null)
            {
                return path;
            }

            path.Add(location);
            var ancestors = await _locationRepository.GetAncestorsAsync(locationId);
            path.InsertRange(0, ancestors);

            return path;
        }

        public async Task CreateLocationAsync(Location location)
        {
            ValidateLocationAsync(location);
            await ValidateUniverseExistsAsync(location.UniverseId);

            if (location.ParentLocationId.HasValue)
            {
                await ValidateParentExistsAsync(location.ParentLocationId.Value);
            }

            location.CreatedDate = DateTime.UtcNow;
            location.ModifiedDate = DateTime.UtcNow;
            await _locationRepository.CreateAsync(location);
        }

        public async Task UpdateLocationAsync(Location location)
        {
            ValidateLocationAsync(location);
            
            var existing = await _locationRepository.GetByIdAsync(location.Id);
            if (existing == null)
            {
                throw new ArgumentException($"Location with ID {location.Id} not found.");
            }

            if (location.ParentLocationId.HasValue)
            {
                await ValidateParentExistsAsync(location.ParentLocationId.Value);
                
                if (await _locationRepository.HasCircularReferenceAsync(location.Id, location.ParentLocationId.Value))
                {
                    throw new InvalidOperationException("Cannot set parent: would create circular reference.");
                }
            }

            location.ModifiedDate = DateTime.UtcNow;
            await _locationRepository.UpdateAsync(location);
        }

        public async Task DeleteLocationAsync(Guid id)
        {
            var location = await _locationRepository.GetByIdAsync(id);
            if (location == null)
            {
                throw new ArgumentException($"Location with ID {id} not found.");
            }

            var children = await _locationRepository.GetChildrenAsync(id);
            if (children.Any())
            {
                throw new InvalidOperationException("Cannot delete location with children. Delete or reassign children first.");
            }

            await _locationRepository.DeleteAsync(id);
        }

        public async Task MoveLocationAsync(Guid locationId, Guid? newParentId)
        {
            var location = await _locationRepository.GetByIdAsync(locationId);
            if (location == null)
            {
                throw new ArgumentException($"Location with ID {locationId} not found.");
            }

            if (newParentId.HasValue)
            {
                await ValidateParentExistsAsync(newParentId.Value);
                
                if (await _locationRepository.HasCircularReferenceAsync(locationId, newParentId.Value))
                {
                    throw new InvalidOperationException("Cannot move location: would create circular reference.");
                }
            }

            location.ParentLocationId = newParentId;
            location.ModifiedDate = DateTime.UtcNow;
            await _locationRepository.UpdateAsync(location);
        }

        private void ValidateLocationAsync(Location location)
        {
            if (string.IsNullOrWhiteSpace(location.Name))
            {
                throw new ArgumentException("Location name cannot be empty.");
            }

            if (location.Name.Length > 200)
            {
                throw new ArgumentException("Location name cannot exceed 200 characters.");
            }

            if (string.IsNullOrWhiteSpace(location.Type))
            {
                throw new ArgumentException("Location type cannot be empty.");
            }

            if (!string.IsNullOrEmpty(location.Description) && location.Description.Length > 5000)
            {
                throw new ArgumentException("Location description cannot exceed 5000 characters.");
            }

            if (location.Population.HasValue && location.Population.Value < 0)
            {
                throw new ArgumentException("Population cannot be negative.");
            }

            if (location.Area.HasValue && location.Area.Value < 0)
            {
                throw new ArgumentException("Area cannot be negative.");
            }
        }

        private async Task ValidateUniverseExistsAsync(Guid universeId)
        {
            var universe = await _universeRepository.GetByIdAsync(universeId);
            if (universe == null)
            {
                throw new ArgumentException($"Universe with ID {universeId} not found.");
            }
        }

        private async Task ValidateParentExistsAsync(Guid parentId)
        {
            var parent = await _locationRepository.GetByIdAsync(parentId);
            if (parent == null)
            {
                throw new ArgumentException($"Parent location with ID {parentId} not found.");
            }
        }
    }
}

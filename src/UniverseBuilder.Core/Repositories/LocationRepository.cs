using MongoDB.Driver;
using UniverseBuilder.Core.Models;

namespace UniverseBuilder.Core.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly IMongoCollection<Location> _collection;

        public LocationRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Location>("Locations");
        }

        public async Task<List<Location>> GetAllAsync(Guid universeId)
        {
            return await _collection
                .Find(l => l.UniverseId == universeId)
                .ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(Guid id)
        {
            return await _collection
                .Find(l => l.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Location>> GetByUniverseIdAsync(Guid universeId)
        {
            return await _collection
                .Find(l => l.UniverseId == universeId)
                .ToListAsync();
        }

        public async Task<List<Location>> GetChildrenAsync(Guid parentId)
        {
            return await _collection
                .Find(l => l.ParentLocationId == parentId)
                .ToListAsync();
        }

        public async Task<List<Location>> GetRootLocationsAsync(Guid universeId)
        {
            return await _collection
                .Find(l => l.UniverseId == universeId && l.ParentLocationId == null)
                .ToListAsync();
        }

        public async Task<List<Location>> GetAncestorsAsync(Guid locationId)
        {
            var ancestors = new List<Location>();
            var current = await GetByIdAsync(locationId);

            while (current?.ParentLocationId != null)
            {
                current = await GetByIdAsync(current.ParentLocationId.Value);
                if (current != null)
                {
                    ancestors.Insert(0, current);
                }
            }

            return ancestors;
        }

        public async Task<List<Location>> GetDescendantsAsync(Guid locationId)
        {
            var descendants = new List<Location>();
            await CollectDescendantsRecursive(locationId, descendants);
            return descendants;
        }

        private async Task CollectDescendantsRecursive(Guid parentId, List<Location> descendants)
        {
            var children = await GetChildrenAsync(parentId);
            descendants.AddRange(children);

            foreach (var child in children)
            {
                await CollectDescendantsRecursive(child.Id, descendants);
            }
        }

        public async Task<List<Location>> GetSiblingsAsync(Guid locationId)
        {
            var location = await GetByIdAsync(locationId);
            if (location == null)
            {
                return new List<Location>();
            }

            return await _collection
                .Find(l => l.ParentLocationId == location.ParentLocationId && l.Id != locationId)
                .ToListAsync();
        }

        public async Task CreateAsync(Location location)
        {
            await _collection.InsertOneAsync(location);
        }

        public async Task UpdateAsync(Location location)
        {
            await _collection.ReplaceOneAsync(l => l.Id == location.Id, location);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(l => l.Id == id);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _collection
                .Find(l => l.Id == id)
                .AnyAsync();
        }

        public async Task<bool> HasCircularReferenceAsync(Guid locationId, Guid? newParentId)
        {
            if (newParentId == null)
            {
                return false;
            }

            if (locationId == newParentId)
            {
                return true;
            }

            var descendants = await GetDescendantsAsync(locationId);
            return descendants.Any(d => d.Id == newParentId);
        }
    }
}

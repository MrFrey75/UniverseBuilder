using UniverseBuilder.Core.Models;

namespace UniverseBuilder.Core.Repositories
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllAsync(Guid universeId);
        Task<Location?> GetByIdAsync(Guid id);
        Task<List<Location>> GetByUniverseIdAsync(Guid universeId);
        Task<List<Location>> GetChildrenAsync(Guid parentId);
        Task<List<Location>> GetRootLocationsAsync(Guid universeId);
        Task<List<Location>> GetAncestorsAsync(Guid locationId);
        Task<List<Location>> GetDescendantsAsync(Guid locationId);
        Task<List<Location>> GetSiblingsAsync(Guid locationId);
        Task CreateAsync(Location location);
        Task UpdateAsync(Location location);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> HasCircularReferenceAsync(Guid locationId, Guid? newParentId);
    }
}

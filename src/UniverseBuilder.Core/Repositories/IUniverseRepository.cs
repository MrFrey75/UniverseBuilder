using UniverseBuilder.Core.Models;

namespace UniverseBuilder.Core.Repositories
{
    public interface IUniverseRepository
    {
        Task<List<Universe>> GetAllAsync();
        Task<Universe> GetByIdAsync(Guid id);
        Task CreateAsync(Universe universe);
        Task UpdateAsync(Universe universe);
        Task DeleteAsync(Guid id);
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniverseBuilder.Core.Models;
using UniverseBuilder.Core.Repositories;

namespace UniverseBuilder.Core.Services
{
    public class UniverseService
    {
        private readonly IUniverseRepository _repository;

        public UniverseService(IUniverseRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Universe>> GetAllUniversesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Universe> GetUniverseByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task CreateUniverseAsync(Universe universe)
        {
            ValidateUniverse(universe);
            universe.CreatedDate = DateTime.UtcNow;
            universe.ModifiedDate = DateTime.UtcNow;
            await _repository.CreateAsync(universe);
        }

        public async Task UpdateUniverseAsync(Universe universe)
        {
            ValidateUniverse(universe);
            universe.ModifiedDate = DateTime.UtcNow;
            await _repository.UpdateAsync(universe);
        }

        public async Task DeleteUniverseAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }

        private void ValidateUniverse(Universe universe)
        {
            if (string.IsNullOrWhiteSpace(universe.Name))
            {
                throw new ArgumentException("Universe name cannot be empty.");
            }

            if (universe.CreatedDate > DateTime.UtcNow)
            {
                throw new ArgumentException("CreatedDate cannot be in the future.");
            }
        }
    }
}
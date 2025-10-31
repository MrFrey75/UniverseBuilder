using MongoDB.Driver;
using UniverseBuilder.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UniverseBuilder.Core.Repositories
{
    public class UniverseRepository : IUniverseRepository
    {
        private readonly IMongoCollection<Universe> _collection;

        public UniverseRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Universe>("Universes");
        }

        public async Task<List<Universe>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<Universe> GetByIdAsync(Guid id)
        {
            return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Universe universe)
        {
            await _collection.InsertOneAsync(universe);
        }

        public async Task UpdateAsync(Universe universe)
        {
            await _collection.ReplaceOneAsync(u => u.Id == universe.Id, universe);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _collection.DeleteOneAsync(u => u.Id == id);
        }
    }
}
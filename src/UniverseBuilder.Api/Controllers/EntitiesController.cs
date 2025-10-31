using Microsoft.AspNetCore.Mvc;
using UniverseBuilder.Core.Database;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace UniverseBuilder.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntitiesController : ControllerBase
    {
        private readonly DatabaseContext _dbContext;

        public EntitiesController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEntities()
        {
            var collection = _dbContext.GetCollection<BsonDocument>("Entities");
            var entities = await collection.Find(new BsonDocument()).ToListAsync();
            return Ok(entities);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntityById(string id)
        {
            var collection = _dbContext.GetCollection<BsonDocument>("Entities");
            var entity = await collection.Find(new BsonDocument { { "_id", new ObjectId(id) } }).FirstOrDefaultAsync();
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntity([FromBody] BsonDocument entity)
        {
            var collection = _dbContext.GetCollection<BsonDocument>("Entities");
            await collection.InsertOneAsync(entity);
            return Created("", entity);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntity(string id, [FromBody] BsonDocument updatedEntity)
        {
            var collection = _dbContext.GetCollection<BsonDocument>("Entities");
            var result = await collection.ReplaceOneAsync(
                new BsonDocument { { "_id", new ObjectId(id) } },
                updatedEntity);

            if (result.MatchedCount == 0)
            {
                return NotFound();
            }

            return Ok(updatedEntity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntity(string id)
        {
            var collection = _dbContext.GetCollection<BsonDocument>("Entities");
            var result = await collection.DeleteOneAsync(new BsonDocument { { "_id", new ObjectId(id) } });

            if (result.DeletedCount == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;

namespace UniverseBuilder.Core.Database
{
    public class DatabaseMigration
    {
        private readonly IMongoDatabase _database;

        public DatabaseMigration(DatabaseContext context)
        {
            _database = context.GetCollection<BsonDocument>("_migrations").Database;
        }

        public void ApplyMigrations(IEnumerable<Action<IMongoDatabase>> migrations)
        {
            foreach (var migration in migrations)
            {
                try
                {
                    migration(_database);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Migration failed: {ex.Message}");
                }
            }
        }
    }
}
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace UniverseBuilder.Core.Models
{
    public class Location
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [BsonRepresentation(BsonType.String)]
        public Guid UniverseId { get; set; }

        [BsonRepresentation(BsonType.String)]
        public Guid? ParentLocationId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public LocationCoordinates? Coordinates { get; set; }
        public string Climate { get; set; } = string.Empty;
        public long? Population { get; set; }
        public double? Area { get; set; }
        
        public Dictionary<string, string> CustomProperties { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }

    public class LocationCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
    }
}

namespace UniverseBuilder.Api.DTOs
{
    public class LocationDto
    {
        public Guid Id { get; set; }
        public Guid UniverseId { get; set; }
        public Guid? ParentLocationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public LocationCoordinatesDto? Coordinates { get; set; }
        public string Climate { get; set; } = string.Empty;
        public long? Population { get; set; }
        public double? Area { get; set; }
        public Dictionary<string, string> CustomProperties { get; set; } = new();
        public List<string> Tags { get; set; } = new();
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }

    public class LocationCoordinatesDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double? Altitude { get; set; }
    }
}

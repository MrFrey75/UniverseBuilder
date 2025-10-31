using System.ComponentModel.DataAnnotations;

namespace UniverseBuilder.Api.DTOs
{
    public class CreateLocationDto
    {
        [Required(ErrorMessage = "Universe ID is required")]
        public Guid UniverseId { get; set; }

        public Guid? ParentLocationId { get; set; }

        [Required(ErrorMessage = "Location name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 200 characters")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Location type is required")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Type must be between 1 and 100 characters")]
        public string Type { get; set; } = string.Empty;

        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters")]
        public string Description { get; set; } = string.Empty;

        public LocationCoordinatesDto? Coordinates { get; set; }

        [StringLength(200, ErrorMessage = "Climate cannot exceed 200 characters")]
        public string Climate { get; set; } = string.Empty;

        [Range(0, long.MaxValue, ErrorMessage = "Population must be non-negative")]
        public long? Population { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Area must be non-negative")]
        public double? Area { get; set; }

        public Dictionary<string, string> CustomProperties { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }
}

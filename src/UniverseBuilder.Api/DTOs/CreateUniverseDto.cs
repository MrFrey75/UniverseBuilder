using System.ComponentModel.DataAnnotations;

namespace UniverseBuilder.Api.DTOs
{
    public class CreateUniverseDto
    {
        [Required(ErrorMessage = "Universe name is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 200 characters")]
        public string Name { get; set; } = string.Empty;

        [StringLength(5000, ErrorMessage = "Description cannot exceed 5000 characters")]
        public string Description { get; set; } = string.Empty;
    }
}

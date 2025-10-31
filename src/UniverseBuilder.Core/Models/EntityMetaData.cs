namespace UniverseBuilder.Core.Models
{
    public class EntityMetaData
    {
        public string Key { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<Tag> Tags { get; set; } = [];
    }

    public class Tag
    {
        public string Name { get; set; } = string.Empty;
        public string Color { get; set; } = string.Empty;
    }
}
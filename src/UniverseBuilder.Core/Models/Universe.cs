using System;

namespace UniverseBuilder.Core.Models
{
    public class Universe
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
        public List<EntityMetaData> MetaDataItems { get; set; } = [];
    }
}
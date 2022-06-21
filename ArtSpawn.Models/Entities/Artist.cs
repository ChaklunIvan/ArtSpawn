using System.Collections.Generic;

namespace ArtSpawn.Models.Entities
{
    public class Artist : BaseEntity 
    {
        public string Name { get; set; }
        public string About { get; set; }
        public byte[] Image { get; set; }
        public IList<Product> Products { get; set; }
    }
}

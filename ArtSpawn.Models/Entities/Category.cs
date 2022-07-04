using System.Collections.Generic;

namespace ArtSpawn.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Type { get; set; }
        public IList<Product> Products { get; set; }
    }
}

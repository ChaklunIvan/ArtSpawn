using System.Collections.Generic;

namespace ArtSpawn.Models.Entities
{
    public class Product : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[] File { get; set; }
        public Artist Artist { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
    }
}

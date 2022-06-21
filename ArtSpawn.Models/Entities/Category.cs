using System.Collections.Generic;

namespace ArtSpawn.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
    }
}

using ArtSpawn.Models.Enums;
using System.Collections.Generic;

namespace ArtSpawn.Models.Entities
{
    public class Category : BaseEntity
    {
        public CategoryType Type { get; set; }
        public IList<Product> Products { get; set; }
    }
}

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtSpawn.Models.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        public IList<ProductCategory> ProductCategories { get; set; }
    }
}

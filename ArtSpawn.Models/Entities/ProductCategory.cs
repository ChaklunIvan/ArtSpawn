using System;
using System.Threading.Tasks;

namespace ArtSpawn.Models.Entities
{
    public class ProductCategory : BaseEntity
    {
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

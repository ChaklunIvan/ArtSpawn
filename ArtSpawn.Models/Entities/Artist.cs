using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ArtSpawn.Models.Entities
{
    public class Artist : BaseEntity 
    {
        [Required]
        [MaxLength(32)]
        public string Name { get; set; }
        public string About { get; set; }
        public byte[] Image { get; set; }
        public IList<Product> Products { get; set; }
    }
}

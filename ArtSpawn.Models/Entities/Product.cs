using ArtSpawn.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSpawn.Models.Entities
{
    public class Product : BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        [Required]
        public byte[] File { get; set; }
        public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

    }
}

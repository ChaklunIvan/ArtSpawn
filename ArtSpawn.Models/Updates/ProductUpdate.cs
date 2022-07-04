using System;

namespace ArtSpawn.Models.Updates
{
    public class ProductUpdate
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public byte[]? File { get; set; }
    }
}

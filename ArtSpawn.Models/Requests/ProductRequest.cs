using System;

namespace ArtSpawn.Models.Requests
{
    public class ProductRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        public Guid? ArtistId { get; set; }
    }
}

using System;

namespace ArtSpawn.Models.Responses
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public Guid ArtistId { get; set; }
        public Guid CategoryId { get; set; }
    }
}

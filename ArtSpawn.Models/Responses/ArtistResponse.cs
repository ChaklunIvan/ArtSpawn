using System;

namespace ArtSpawn.Models.Responses
{
    public class ArtistResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public byte[] Image { get; set; }
    }
}

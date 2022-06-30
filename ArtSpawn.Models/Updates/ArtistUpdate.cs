using System;

namespace ArtSpawn.Models.Updates
{
    public class ArtistUpdate
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string About { get; set; }
        public byte[]? Image { get; set; }
    }
}

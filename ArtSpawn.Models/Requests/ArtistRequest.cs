using System.ComponentModel.DataAnnotations;

namespace ArtSpawn.Models.Requests
{
    public class ArtistRequest
    {
        [Required]
        public string Name { get; set; }
        public string About { get; set; }
        public byte[]? Image { get; set; }
    }
}

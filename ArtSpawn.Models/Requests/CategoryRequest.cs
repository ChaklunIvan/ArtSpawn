using System.ComponentModel.DataAnnotations;

namespace ArtSpawn.Models.Requests
{
    public class CategoryRequest
    {
        [Required]
        public string Type { get; set; }
    }
}

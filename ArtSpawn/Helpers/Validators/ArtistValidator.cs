using ArtSpawn.Models.Entities;
using FluentValidation;
using System.Linq;

namespace ArtSpawn.Helpers.Validators
{
    public class ArtistValidator : AbstractValidator<Artist>
    {
        public ArtistValidator()
        {
            
        }
    }
}

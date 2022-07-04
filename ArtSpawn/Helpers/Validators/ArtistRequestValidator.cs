using ArtSpawn.Models.Entities;
using FluentValidation;

namespace ArtSpawn.Helpers.Validators
{
    public class ArtistRequestValidator : AbstractValidator<Artist>
    {
        public ArtistRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(3, 20);
            RuleFor(x => x.About).Length(10, 200);
        }
    }
}

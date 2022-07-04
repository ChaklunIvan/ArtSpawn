using ArtSpawn.Models.Updates;
using FluentValidation;

namespace ArtSpawn.Helpers.Validators
{
    public class ArtistUpdateValidator : AbstractValidator<ArtistUpdate>
    {
        public ArtistUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).Length(2, 20);
            RuleFor(x => x.About).Length(10, 200);
        }
    }
}

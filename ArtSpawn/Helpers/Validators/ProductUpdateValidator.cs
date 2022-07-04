using ArtSpawn.Models.Updates;
using FluentValidation;

namespace ArtSpawn.Helpers.Validators
{
    public class ProductUpdateValidator : AbstractValidator<ProductUpdate>
    {
        public ProductUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().Length(2, 32);
            RuleFor(x => x.Description).Length(15, 200);
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}

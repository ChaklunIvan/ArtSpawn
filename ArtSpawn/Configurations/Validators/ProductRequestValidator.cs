using ArtSpawn.Models.Entities;
using FluentValidation;

namespace ArtSpawn.Configurations.Validators
{
    public class ProductRequestValidator : AbstractValidator<Product>
    {
        public ProductRequestValidator()
        {
            RuleFor(x => x.Title).NotEmpty().Length(2, 32);
            RuleFor(x => x.Description).Length(15, 200);
            RuleFor(x => x.Price).NotEmpty();
        }
    }
}

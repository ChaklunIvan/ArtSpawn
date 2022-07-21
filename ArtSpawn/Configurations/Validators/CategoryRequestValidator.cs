using ArtSpawn.Models.Entities;
using ArtSpawn.Models.Exceptions;
using ArtSpawn.Models.Requests;
using FluentValidation;

namespace ArtSpawn.Configurations.Validators
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>
    {
        public CategoryRequestValidator()
        {
            RuleFor(x => x.Type).NotEmpty().Length(1, 20);
        }
    }
}

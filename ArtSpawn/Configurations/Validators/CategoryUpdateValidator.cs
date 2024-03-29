﻿using ArtSpawn.Models.Updates;
using FluentValidation;

namespace ArtSpawn.Configurations.Validators
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdate>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Type).Length(1, 20);
        }
    }
}

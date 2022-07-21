using ArtSpawn.Models.Requests;
using FluentValidation;

namespace ArtSpawn.Configurations.Validators
{
    public class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password).MinimumLength(8).NotEmpty();
        }
    }
}

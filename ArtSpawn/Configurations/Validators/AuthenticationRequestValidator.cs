using ArtSpawn.Models.Requests;
using FluentValidation;

namespace ArtSpawn.Configurations.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}

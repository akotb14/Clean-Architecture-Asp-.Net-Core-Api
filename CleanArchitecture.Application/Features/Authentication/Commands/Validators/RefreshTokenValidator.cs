using CleanArchitecture.Application.Features.Authentication.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Validators
{
    public class RefreshTokenValidator : AbstractValidator<RefreshTokenCommand>
    {
        public RefreshTokenValidator() { ApplyValidationsRules(); }
        public void ApplyValidationsRules()
        {
            RuleFor(e => e.AccessToken).NotEmpty().WithMessage("AccessToken Not Empty")
               .NotNull().WithMessage("AccessToken Not Null");

            RuleFor(e => e.RefreshToken).NotEmpty().WithMessage("RefreshToken Not Empty")
                .NotNull().WithMessage("RefreshToken Not Null");
        }
    }
}

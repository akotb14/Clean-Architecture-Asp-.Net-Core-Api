using CleanArchitecture.Application.Features.Authentication.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Validators
{
    public class SignInValidator : AbstractValidator<SignInCommand>
    {
        public SignInValidator() { ApplyValidationsRules(); }
        public void ApplyValidationsRules()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email Not Empty")
               .NotNull().WithMessage("Email Not Null").EmailAddress();

            RuleFor(e => e.Password).NotEmpty().WithMessage("Pasword Not Empty")
                .NotNull().WithMessage("Password Not Null");
        }
    }
}

using CleanArchitecture.Application.Features.Authentication.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Validators
{
    public class SendResetPasswordValidator : AbstractValidator<SendResetPasswordCommand>
    {
        public SendResetPasswordValidator() { ApplyValidationsRules(); }
        public void ApplyValidationsRules()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email Not Empty")
               .NotNull().WithMessage("Email Not Null").EmailAddress();
        }
    }
}

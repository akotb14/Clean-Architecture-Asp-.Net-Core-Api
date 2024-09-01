using CleanArchitecture.Application.Features.Authentication.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authentication.Commands.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            ApplyValidationsRules();
        }
        private void ApplyValidationsRules()
        {
            RuleFor(e => e.Email).NotEmpty().WithMessage("UserId Not Empty")
                .NotNull().WithMessage("UserId Not Empty").EmailAddress();

            RuleFor(e => e.Password).NotEmpty().WithMessage("CurrentPassword Not Empty")
                .NotNull().WithMessage("CurrentPassword Not Null");


            RuleFor(e => e.ConfirmPassword).Equal(e => e.Password).WithMessage("Password and Comfirm are not match");

        }
    }
}

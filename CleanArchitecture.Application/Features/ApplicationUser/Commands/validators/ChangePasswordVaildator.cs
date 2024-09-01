using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.ApplicationUser.Commands.validators
{
    public class ChangePasswordVaildator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordVaildator() { ApplyValidationsRules(); }
        private void ApplyValidationsRules()
        {
            RuleFor(e => e.UserId).NotEmpty().WithMessage("UserId Not Empty")
                .NotNull().WithMessage("UserId Not Empty");

            RuleFor(e => e.CurrentPassword).NotEmpty().WithMessage("CurrentPassword Not Empty")
                .NotNull().WithMessage("CurrentPassword Not Null");
            RuleFor(e => e.NewPassword).NotEmpty().WithMessage("NewPassword Not Empty")
                .NotNull().WithMessage("NewPassword Not Null");

            RuleFor(e => e.ConfirmPassword).Equal(e => e.NewPassword).WithMessage("Password and Comfirm are not match");

        }
    }
}

using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.ApplicationUser.Commands.validators
{
    public class AddUserValidator : AbstractValidator<AddUserCommand>
    {
        public AddUserValidator() { ApplyValidationsRules(); }
        private void ApplyValidationsRules()
        {
            RuleFor(e => e.UserName).NotEmpty().WithMessage("UserName Not Empty")
                .NotNull().WithMessage("UserName Not Empty");
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email Not Empty")
                .NotNull().WithMessage("Email Not Null").EmailAddress();
            RuleFor(e => e.FullName).NotEmpty().WithMessage("FullName Not Empty")
                .NotNull().WithMessage("FullName Not Null");
            RuleFor(e => e.Password).NotEmpty().WithMessage("Pasword Not Empty")
                .NotNull().WithMessage("Password Not Null");
            RuleFor(e => e.ConfirmPassword).Equal(e => e.Password).WithMessage("Password and Comfirm are not match");

        }
    }
}

using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.ApplicationUser.Commands.validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserValidator() { ApplyValidationsRules(); }
        private void ApplyValidationsRules()
        {
            RuleFor(e => e.Id).NotEmpty().WithMessage("UserId Not Empty")
                .NotNull().WithMessage("UserId Not Empty"); ;
            RuleFor(e => e.UserName).NotEmpty().WithMessage("UserName Not Empty")
                .NotNull().WithMessage("UserName Not Empty");
            RuleFor(e => e.Email).NotEmpty().WithMessage("Email Not Empty")
                .NotNull().WithMessage("Email Not Null").EmailAddress();
            RuleFor(e => e.FullName).NotEmpty().WithMessage("FullName Not Empty")
                .NotNull().WithMessage("FullName Not Null");

        }
    }
}

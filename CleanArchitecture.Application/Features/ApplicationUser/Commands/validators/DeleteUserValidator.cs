using CleanArchitecture.Application.Features.ApplicationUser.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.ApplicationUser.Commands.validators
{
    public class DeleteUserValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserValidator()
        {
            ApplyValidationsRules();
        }
        private void ApplyValidationsRules()
        {
            RuleFor(e => e.UserId).NotEmpty().WithMessage("UserId Not Empty")
                .NotNull().WithMessage("UserId Not Empty"); ;

        }
    }
}

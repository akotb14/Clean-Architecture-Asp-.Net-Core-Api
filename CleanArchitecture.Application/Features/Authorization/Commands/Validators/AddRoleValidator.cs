using CleanArchitecture.Application.Features.Authorization.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Validators
{
    public class AddRoleValidator : AbstractValidator<AddRoleCommand>
    {
        public AddRoleValidator() { ApplyValidatorRules(); }
        public void ApplyValidatorRules()
        {
            RuleFor(e => e.RoleName).NotEmpty().WithMessage("RoleName Not Empty")
               .NotNull().WithMessage("RoleName Not Null");
        }
    }
}

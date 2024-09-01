using CleanArchitecture.Application.Features.Authorization.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Validators
{
    public class EditRoleValidator : AbstractValidator<EditRoleCommand>
    {
        public EditRoleValidator() { ApplyValidatorRules(); }
        public void ApplyValidatorRules()
        {

            RuleFor(e => e.RoleId).NotEmpty().WithMessage("RoleId Not Empty")
               .NotNull().WithMessage("RoleId Not Null");
            RuleFor(e => e.RoleName).NotEmpty().WithMessage("RoleName Not Empty")
                   .NotNull().WithMessage("RoleName Not Null");
        }
    }
}

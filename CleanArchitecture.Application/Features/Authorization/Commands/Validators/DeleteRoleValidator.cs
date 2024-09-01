using CleanArchitecture.Application.Features.Authorization.Commands.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authorization.Commands.Validators
{
    public class DeleteRoleValidator : AbstractValidator<DeleteRoleCommand>
    {
        public DeleteRoleValidator() { ApplyValidatorRules(); }
        public void ApplyValidatorRules()
        {
            RuleFor(e => e.RoleId).NotEmpty().WithMessage("RoleId Not Empty")
               .NotNull().WithMessage("RoleId Not Null");
        }
    }
}

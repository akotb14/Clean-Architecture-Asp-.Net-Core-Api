using CleanArchitecture.Application.Features.Authentication.Queries.Requests;
using FluentValidation;

namespace CleanArchitecture.Application.Features.Authentication.Queries.Validators
{
    public class ConfirmEmailValidator : AbstractValidator<ConfirmEmailQuery>
    {
        public ConfirmEmailValidator() { ApplyValidationsRules(); }
        private void ApplyValidationsRules()
        {
            RuleFor(e => e.UserId).NotEmpty().WithMessage("UserId Not Empty")
                .NotNull().WithMessage("UserId Not Empty");
            RuleFor(e => e.Code).NotEmpty().WithMessage("Code Not Empty")
                .NotNull().WithMessage("Code Not Null");


        }
    }
}

using FluentValidation;
using PetStore.Models.Request;

namespace PetStore.Validations
{
    public class OwnerRequestValidator : AbstractValidator<OwnerRequest>
    {
        public OwnerRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required.")
                .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("Phone Number must be in a valid format (E.164).");
        }
    }
}

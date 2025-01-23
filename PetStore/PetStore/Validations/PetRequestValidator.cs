using FluentValidation;
using PetStore.Models.Request;

namespace PetStore.Validations
{
    public class PetRequestValidator : AbstractValidator<PetRequest>
    {
        public PetRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Pet Name is required.")
                .MaximumLength(100).WithMessage("Pet Name cannot exceed 100 characters.");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Pet Type is required.")
                .MaximumLength(50).WithMessage("Pet Type cannot exceed 50 characters.");

            RuleFor(x => x.Age)
                .InclusiveBetween(0, 30).WithMessage("Pet Age must be between 0 and 30.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Pet Price must be greater than 0.")
                .LessThanOrEqualTo(10000).WithMessage("Pet Price cannot exceed 10,000.");

            RuleFor(x => x.OwnerId)
                .NotEmpty().WithMessage("OwnerId is required.");
        }
    }
}

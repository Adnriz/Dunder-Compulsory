using FluentValidation;
using Service.TransferModels.Requests;

namespace Service.Validators
{
    public class CreatePaperDtoValidator : AbstractValidator<CreatePaperDto>
    {
        public CreatePaperDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
            RuleFor(x => x.Discontinued).NotNull().WithMessage("Discontinued status is required.");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stock cannot be negative.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}
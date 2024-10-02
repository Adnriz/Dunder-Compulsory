using FluentValidation;
using Service.TransferModels.Requests;

namespace Service.Validators
{
    public class CreateOrderEntryDtoValidator : AbstractValidator<CreateOrderEntryDto>
    {
        public CreateOrderEntryDtoValidator()
        {
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
            RuleFor(x => x.ProductId).GreaterThan(0).WithMessage("Product ID must be greater than zero.");
        }
    }
}
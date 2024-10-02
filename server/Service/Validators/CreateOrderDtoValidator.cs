using FluentValidation;
using Service.TransferModels.Requests;

namespace Service.Validators
{
    public class CreateOrderDtoValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderDtoValidator()
        {
            RuleFor(x => x.OrderDate).NotEmpty().WithMessage("Order date is required.");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required.");
            RuleFor(x => x.TotalAmount).GreaterThan(0).WithMessage("Total amount must be greater than zero.");
            RuleFor(x => x.CustomerId).GreaterThan(0).WithMessage("Customer ID must be greater than zero.");
            RuleForEach(x => x.OrderEntries).SetValidator(new CreateOrderEntryDtoValidator());
        }
    }
}
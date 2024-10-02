using FluentValidation;
using Service.TransferModels.Requests;

namespace Service.Validators
{
    public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.PropertyName).NotEmpty().WithMessage("Property name is required.");
        }
    }
}
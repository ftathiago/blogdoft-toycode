using FluentValidation;

namespace Producer.Business.Entities.Validations
{
    public class SaleItemEntityValidator : AbstractValidator<SaleItemEntity>
    {
        public SaleItemEntityValidator()
        {
            RuleFor(item => item.Quantity)
                .Must(quantity => quantity > 0)
                .WithMessage("Quantity must be greather than zero.");
            RuleFor(item => item.Value)
                .Must(value => value > 0)
                .WithMessage("Value must be greather than zero.");
        }
    }
}

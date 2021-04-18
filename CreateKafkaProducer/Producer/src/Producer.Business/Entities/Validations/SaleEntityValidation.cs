using FluentValidation;
using System.Linq;

namespace Producer.Business.Entities.Validations
{
    public class SaleEntityValidation : AbstractValidator<SaleEntity>
    {
        public SaleEntityValidation()
        {
            RuleFor(s => s.Number)
                .Matches("^\\d{7}$")
                .WithMessage("Sale number should have seven digits.");
            RuleFor(s => s.CustomerIdentity)
                .IsValidCPF()
                .WithMessage("Customer identity should be a valid CPF.");
            RuleFor(s => s.Items)
                .Must(items => items.Any())
                .WithMessage("A sale should have one item at last.");
            RuleForEach(sale => sale.Items)
                .SetValidator(new SaleItemEntityValidator());
        }
    }
}

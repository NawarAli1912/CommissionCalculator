using FCamara.CommissionCalculator.Models;
using FluentValidation;

namespace FCamara.CommissionCalculator.Validators;

public class CommissionCalculationRequestValidator : AbstractValidator<CommissionCalculationRequest>
{
    public CommissionCalculationRequestValidator()
    {

        RuleFor(x => x.LocalSalesCount)
            .GreaterThanOrEqualTo(0).WithMessage("Local sales count must be zero or greater.");

        RuleFor(x => x.ForeignSalesCount)
            .GreaterThanOrEqualTo(0).WithMessage("Foreign sales count must be zero or greater.");

        RuleFor(x => x.AverageSaleAmount)
            .GreaterThan(0).WithMessage("Average sale amount must be greater than zero.");
    }
}

using FCamara.CommissionCalculator.Models;
using FCamara.CommissionCalculator.Validators;
using FluentValidation.TestHelper;

namespace FCamara.CommissionCalculator.Tests.Validators;
public class CommissionCalculationRequestValidatorTests
{
    private readonly CommissionCalculationRequestValidator _validator;

    public CommissionCalculationRequestValidatorTests()
    {
        _validator = new CommissionCalculationRequestValidator();
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenLocalSalesCountIsNegative()
    {
        var model = new CommissionCalculationRequest { LocalSalesCount = -1 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.LocalSalesCount);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenForeignSalesCountIsNegative()
    {
        var model = new CommissionCalculationRequest { ForeignSalesCount = -1 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.ForeignSalesCount);
    }

    [Fact]
    public void Validator_ShouldHaveError_WhenAverageSaleAmountIsZeroOrNegative()
    {
        var model = new CommissionCalculationRequest { AverageSaleAmount = 0 };
        var result = _validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(x => x.AverageSaleAmount);
    }

    [Fact]
    public void Validator_ShouldNotHaveError_ForValidInput()
    {
        var model = new CommissionCalculationRequest
        {
            LocalSalesCount = 10,
            ForeignSalesCount = 5,
            AverageSaleAmount = 100m
        };
        var result = _validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
using FCamara.CommissionCalculator.Models;

namespace FCamara.CommissionCalculator.Services.Interfaces;

public interface ICommissionCalculatorService
{
    CommissionCalculationResponse CalculateCommission(CommissionCalculationRequest request);
}

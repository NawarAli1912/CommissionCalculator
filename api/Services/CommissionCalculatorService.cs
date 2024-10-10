using FCamara.CommissionCalculator.Models;
using FCamara.CommissionCalculator.Services.Interfaces;

namespace FCamara.CommissionCalculator.Services;

public class CommissionCalculatorService : ICommissionCalculatorService
{
    public CommissionCalculationResponse CalculateCommission(CommissionCalculationRequest request)
    {
        // FCamara commission rates
        const decimal fcLocalRate = 0.20m;
        const decimal fcForeignRate = 0.35m;

        // Competitor commission rates
        const decimal competitorLocalRate = 0.02m;
        const decimal competitorForeignRate = 0.0755m;

        var averageSaleAmount = request.AverageSaleAmount;
        var localSalesCount = request.LocalSalesCount;
        var foreignSalesCount = request.ForeignSalesCount;

        // Calculate FCamara commissions
        var fcLocalCommission = fcLocalRate * localSalesCount * averageSaleAmount;
        var fcForeignCommission = fcForeignRate * foreignSalesCount * averageSaleAmount;
        var fcTotalCommission = fcLocalCommission + fcForeignCommission;

        // Calculate competitor commissions
        var competitorLocalCommission = competitorLocalRate * localSalesCount * averageSaleAmount;
        var competitorForeignCommission = competitorForeignRate * foreignSalesCount * averageSaleAmount;
        var competitorTotalCommission = competitorLocalCommission + competitorForeignCommission;

        return new CommissionCalculationResponse
        {
            FCamaraCommissionAmount = fcTotalCommission,
            CompetitorCommissionAmount = competitorTotalCommission
        };
    }
}

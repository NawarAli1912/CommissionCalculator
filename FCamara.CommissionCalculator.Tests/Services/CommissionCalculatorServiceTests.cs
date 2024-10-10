using FCamara.CommissionCalculator.Models;
using FCamara.CommissionCalculator.Services;

namespace FCamara.CommissionCalculator.Tests.Services;

public class CommissionCalculatorServiceTests
{
    private readonly CommissionCalculatorService _service;

    public CommissionCalculatorServiceTests()
    {
        _service = new CommissionCalculatorService();
    }

    [Fact]
    public void CalculateCommission_ReturnsCorrectValues_ForValidInput()
    {
        // Arrange
        var request = new CommissionCalculationRequest
        {
            LocalSalesCount = 10,
            ForeignSalesCount = 10,
            AverageSaleAmount = 100m
        };

        // Act
        var response = _service.CalculateCommission(request);

        // Assert
        Assert.Equal(550m, response.FCamaraCommissionAmount);
        Assert.Equal(95.5m, response.CompetitorCommissionAmount);
    }
    [Fact]
    public void CalculateCommission_ReturnsZeroCommission_WhenNoSales()
    {
        // Arrange
        var request = new CommissionCalculationRequest
        {
            LocalSalesCount = 0,
            ForeignSalesCount = 0,
            AverageSaleAmount = 100m
        };

        // Act
        var response = _service.CalculateCommission(request);

        // Assert
        Assert.Equal(0m, response.FCamaraCommissionAmount);
        Assert.Equal(0m, response.CompetitorCommissionAmount);
    }

    [Fact]
    public void CalculateCommission_HandlesLargeNumbersCorrectly()
    {
        // Arrange
        var request = new CommissionCalculationRequest
        {
            LocalSalesCount = 10000,
            ForeignSalesCount = 5000,
            AverageSaleAmount = 1000m
        };

        // Act
        var response = _service.CalculateCommission(request);

        // Expected calculations:
        // FCamara Local: 20% * 10000 * 1000 = 2,000,000
        // FCamara Foreign: 35% * 5000 * 1000 = 1,750,000
        // FCamara Total: 2,000,000 + 1,750,000 = 3,750,000

        // Competitor Local: 2% * 10000 * 1000 = 200,000
        // Competitor Foreign: 7.55% * 5000 * 1000 = 377,500
        // Competitor Total: 200,000 + 377,500 = 577,500

        // Assert
        Assert.Equal(3750000m, response.FCamaraCommissionAmount);
        Assert.Equal(577500m, response.CompetitorCommissionAmount);
    }

    [Fact]
    public void CalculateCommission_ReturnsZero_WhenAverageSaleAmountIsZero()
    {
        // Arrange
        var request = new CommissionCalculationRequest
        {
            LocalSalesCount = 10,
            ForeignSalesCount = 5,
            AverageSaleAmount = 0m
        };

        // Act
        var response = _service.CalculateCommission(request);

        // Assert
        Assert.Equal(0m, response.FCamaraCommissionAmount);
        Assert.Equal(0m, response.CompetitorCommissionAmount);
    }
}
using FCamara.CommissionCalculator.Models;
using FCamara.CommissionCalculator.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FCamara.CommissionCalculator.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CommissionController : ControllerBase
    {
        private readonly ICommissionCalculatorService _commissionCalculatorService;

        public CommissionController(ICommissionCalculatorService commissionCalculatorService)
        {
            _commissionCalculatorService = commissionCalculatorService;
        }


        [ProducesResponseType(typeof(CommissionCalculationResponse), 200)]
        [HttpPost]
        public IActionResult Calculate([FromBody] CommissionCalculationRequest calculationRequest)
        {
            var result = _commissionCalculatorService.CalculateCommission(calculationRequest);
            return Ok(result);
        }
    }

}

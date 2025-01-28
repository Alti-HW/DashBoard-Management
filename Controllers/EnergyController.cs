using Dashboard_Management.Constants;
using Dashboard_Management.DTOs;
using Dashboard_Management.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_Management.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnergyController : ControllerBase
    {
        private readonly IEnergyService _energyService;

        public EnergyController(IEnergyService energyService)
        {
            _energyService = energyService;
        }

        [HttpPost("energy-consumption")]
        public async Task<IActionResult> GetEnergyConsumption([FromBody] EnergyConsumptionRequestDto request)
        {
            var result = await _energyService.GetEnergyConsumptionAsync(request.StartDate, request.EndDate);

            return Ok(new ApiResponse<IEnumerable<BuildingEnergyConsumptionDto>>
            {
                Success = true,
                Message = ResponseMessages.DataRetrieved,
                Data = result
            });
        }

        [HttpPost("GetMetrics")]
        public async Task<IActionResult> GetMetrics([FromBody] MetricRequestDto request)
        {
            var response = await _energyService.GetMetricsAsync(request);

            if (response == null || !response.Any())
            {
                return NotFound(new ApiResponse<IEnumerable<EnergyMetricResponseDto>>
                {
                    Success = false,
                    Message = ResponseMessages.NoDataFound,
                    Data = null
                });
            }

            return Ok(new ApiResponse<IEnumerable<EnergyMetricResponseDto>>
            {
                Success = true,
                Message = ResponseMessages.DataRetrieved,
                Data = response
            });
        }
    }
}

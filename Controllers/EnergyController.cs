using Dashboard_Management.DTOs;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Services;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse<string>
                {
                    Success = false,
                    Message = "Invalid request data."
                });
            }

            var result = await _energyService.GetEnergyConsumptionAsync(request.StartDate, request.EndDate);

            return Ok(new ApiResponse<IEnumerable<BuildingEnergyConsumptionDto>>
            {
                Success = true,
                Message = "Data retrieved successfully.",
                Data = result
            });
        }
        [HttpPost("GetMetrics")]
        public async Task<IActionResult> GetMetrics([FromBody] MetricRequestDto request)
        {
            if (request == null || string.IsNullOrEmpty(request.MetricType) || request.StartDate == null || request.EndDate == null)
            {
                return BadRequest("Invalid input data.");
            }

            var response = await _energyService.GetMetricsAsync(request);

            if (response == null || !response.Any())
            {
                return NotFound("No data found for the provided criteria.");
            }

            return Ok(response);
        }
    }

}

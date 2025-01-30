using Dashboard_Management.DTOs;
using Dashboard_Management.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_Management.Controllers
{
    [Authorize(Policy = "DashboardAdminPolicy")]
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
            Console.WriteLine($"Dashboard Admin: {User.IsInRole("Dashboard-Admin")}");
            Console.WriteLine($"default-roles-alti-ems: {User.IsInRole("default-roles-alti-ems")}");
            Console.WriteLine($"offline_access: {User.IsInRole("offline_access")}");
            Console.WriteLine($"uma_authorization: {User.IsInRole("uma_authorization")}");
            Console.WriteLine($"manage-account: {User.IsInRole("manage-account")}");
            Console.WriteLine($"manage-account-links: {User.IsInRole("manage-account-links")}");

            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();

            foreach (var claim in claims)
            {
                Console.WriteLine($"{claim.Type} {claim.Value}");
            }

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

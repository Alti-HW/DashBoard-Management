using System.Collections.Generic;
using System.Threading.Tasks;
using Dashboard_Management.Constants;
using Dashboard_Management.DTOs;
using Dashboard_Management.DTOs.Building;
using Dashboard_Management.Interfaces;
using Dashboard_Management.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dashboard_Management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        [HttpGet("GetAllBuildingsWithFloors")]
        public async Task<IActionResult> GetAllBuildingsWithFloors()
        {
            var result = await _buildingService.GetAllBuildingsWithFloorsAsync();

            return Ok(new ApiResponse<IEnumerable<BuildingMinimalDto>>
            {
                Success = true,
                Message = ResponseMessages.DataRetrieved,
                Data = result
            });
        }
    }
}

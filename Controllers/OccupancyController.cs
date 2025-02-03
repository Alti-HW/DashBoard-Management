using Dashboard_Management.Constants;
using Dashboard_Management.DTOs.Occupancy;
using Dashboard_Management.DTOs;
using Dashboard_Management.Interfaces.Occupancy;
using Microsoft.AspNetCore.Mvc;

[Route("api/occupancy")]
[ApiController]
public class OccupancyController : ControllerBase
{
    private readonly IOccupancyService _occupancyService;

    public OccupancyController(IOccupancyService occupancyService)
    {
        _occupancyService = occupancyService;
    }

    [HttpPost("get")]
    public async Task<IActionResult> GetOccupancyData([FromBody] OccupancyRequestDto request)
    {
        var response = await _occupancyService.GetOccupancyDataAsync(request);

        if (response == null || !response.Any())
        {
            return NotFound(new ApiResponse<IEnumerable<BuildingOccupancyDto>>
            {
                Success = false,
                Message = ResponseMessages.NoDataFound,
                Data = null
            });
        }

        return Ok(new ApiResponse<IEnumerable<BuildingOccupancyDto>>
        {
            Success = true,
            Message = ResponseMessages.DataRetrieved,
            Data = response
        });
    }
}

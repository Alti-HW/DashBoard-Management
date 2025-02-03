using Dashboard_Management.DTOs.Occupancy;

namespace Dashboard_Management.Interfaces.Occupancy
{
    public interface IOccupancyService
    {
        Task<IEnumerable<BuildingOccupancyDto>> GetOccupancyDataAsync(OccupancyRequestDto request);
    }

}

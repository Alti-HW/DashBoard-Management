using Dashboard_Management.DTOs.Occupancy;

namespace Dashboard_Management.Interfaces.Occupancy
{
    public interface IOccupancyRepository
    {
        Task<IEnumerable<BuildingOccupancyDto>> GetOccupancyDataAsync(OccupancyRequestDto request);
    }

}

using Dashboard_Management.DTOs.Occupancy;
using Dashboard_Management.Interfaces.Occupancy;
using Dashboard_Management.Repositories;

namespace Dashboard_Management.Services
{
    public class OccupancyService : IOccupancyService
    {
        private readonly IOccupancyRepository _occupancyRepository;

        public OccupancyService(IOccupancyRepository occupancyRepository)
        {
            _occupancyRepository = occupancyRepository;
        }

        public async Task<IEnumerable<BuildingOccupancyDto>> GetOccupancyDataAsync(OccupancyRequestDto request)
        {
            return await _occupancyRepository.GetOccupancyDataAsync(request);
        }
    }

}
